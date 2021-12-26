using System;
using System.Collections.Generic;
using System.Configuration;
using System.Linq;
using Task_3.ATS.States;
using Task_3.DateBase;

namespace Task_3.ATS
{
    public class Station
    {
        private readonly List<Port> _listPorts;
        private readonly int _totalNumberOfPorts = Convert.ToInt32
            (ConfigurationManager.AppSettings.Get("totalNumberOfPorts"));
        private readonly PortController _portController;

        private readonly List<CallEventArgs> _callsInInitializationState = new();
        private readonly List<CallEventArgs> _callsInDialingState = new();
        private readonly List<Call> _callsInConnectedState = new();

        public event EventHandler<Call> CallRecordCreate;

        public Station(PortController portController)
        {
            _portController = portController;
            _listPorts = new List<Port>(_totalNumberOfPorts);
            this.SetListPorts();
            this.BindPorts();
        }


        public Port GetFreePort {
            get {
                var port = _listPorts.FirstOrDefault(x => x.State == PortState.Free && !x.IsUsed);
                if (port != null) port.IsUsed = true;
                return port;
            }
        }


        public void OnPortOutgoingCall(object sender, CallEventArgs args)
        {
            args.CallState = CallState.Initiation;
            _callsInInitializationState.Add(args);

            var targetPort = _portController.GetPort(args.TargetPhoneNumber);
            if (targetPort is not {State: PortState.Free}) return;
            args.CallState = CallState.Dialing;
            var index = _callsInInitializationState.FindIndex(0, _callsInInitializationState.Count,
                i => i.Id == args.Id);
            _callsInDialingState.Add(_callsInInitializationState[index]);
            _callsInInitializationState.RemoveAt(index);

            targetPort.OnIncomingCall(sender, args);
        }

        public void OnPortAcceptCall(object sender, CallEventArgs args)
        {
            args.CallState = CallState.Connection;
            this.MoveCallToConnectedList(args);
        }


        public void OnPortRejectCall(object sender, CallEventArgs args)
        {
            //this.MoveCallToConnectedList(args);
            var index = _callsInDialingState.FindIndex(0, _callsInDialingState.Count, i => i.Id == args.Id);
            if (index == -1) return;
            this.SendCallToBillingSystem(new Call() {
                Id = _callsInDialingState[index].Id,
                StartCall = DateTime.Now,
                EndCall = DateTime.Now,
                SourceNumber = _callsInDialingState[index].SourcePhoneNumber,
                TargetNumber = _callsInDialingState[index].TargetPhoneNumber
            });
            _callsInDialingState.RemoveAt(index);
        }
        public void OnPortEndOfCall(object sender, CallEventArgs args)
        {
            var index = _callsInConnectedState.FindIndex(0, _callsInConnectedState.Count, i => i.Id == args.Id);
            if (index == -1) return;
            _callsInConnectedState[index].EndCall = DateTime.Now;
            this.SendCallToBillingSystem(_callsInConnectedState[index]);
            _callsInConnectedState.RemoveAt(index);

        }

        public void SendCallToBillingSystem(Call call)
        {
            this.CreateCallRecord(call);
        }

        protected virtual void CreateCallRecord(Call call)
        {
            CallRecordCreate?.Invoke(this, call);

        }

        private void SetListPorts()
        {
            for (var i = 0; i < _listPorts.Capacity; i++) _listPorts.Add(new Port());
        }

        private void BindPorts()
        {
            foreach (var port in _listPorts)
            {
                port.OutgoingCall += this.OnPortOutgoingCall;
                port.AcceptCall += this.OnPortAcceptCall;
                port.RejectCall += this.OnPortRejectCall;
                port.EndOfCall += this.OnPortEndOfCall;
            }
        }

        private void MoveCallToConnectedList(CallEventArgs args)
        {
            var index = _callsInDialingState.FindIndex(0, _callsInDialingState.Count,
                i => i.Id == args.Id);
            if (index == -1) return;
            _callsInConnectedState.Add(new Call() {
                Id = args.Id,
                StartCall = DateTime.Now,
                EndCall = DateTime.Now,
                SourceNumber = args.SourcePhoneNumber,
                TargetNumber = args.TargetPhoneNumber
            });
            _callsInDialingState.RemoveAt(index);
        }
    }
}