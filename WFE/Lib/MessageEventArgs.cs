using System;
using System.Collections.Generic;
using WFE.Base;
using System.Diagnostics;

namespace WFE.Lib
{
    public class MessageEventArgs : EventArgs
    {
        public string Message { get; set; }

        public EventType Type { get; set; }

        public List<string> CallStack { get; private set; }

        public MessageEventArgs(string message, EventType type)
        {
            this.Message = message;
            this.Type = type;
            this.CallStack = new List<string>();

            var stackTrace = new StackTrace();           // get call stack
            StackFrame[] stackFrames = stackTrace.GetFrames();  // get method calls (frames)

            CallStack = new List<string>();

            // write call stack method names
            if (stackFrames == null) return;

            foreach (StackFrame stackFrame in stackFrames)
            {
                CallStack.Add(stackFrame.GetMethod().Name);   // write method name
            }
        }
    }
}
