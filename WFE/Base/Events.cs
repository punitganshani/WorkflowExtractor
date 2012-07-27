using System;
using WFE.Lib;

namespace WFE.Base
{
    public class Events
    {
        public event EventHandler<MessageEventArgs> OnMessageReceived;
        public event EventHandler OnProcessingComplete;

        public void SendMessage(string message, EventType type)
        {
            if (OnMessageReceived == null) return;

            Log.Debug(type + " " + message);
            OnMessageReceived(this, new MessageEventArgs(message, type));
        }

        protected virtual void OnComplete()
        {
            if (OnProcessingComplete != null)
                OnProcessingComplete(this, EventArgs.Empty);
        }
    }

    public enum EventType
    {
        Error,
        Output,
        Warning,
        Complete,
        Started
    }
}