using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;


namespace Chindianese.DebugConsole
{
    public class DebugCommandBase
    {
        public string commandID { get; private set; }
        public string commandDescription { get; private set; }
        public string commandFormat { get; private set; }

        public Type Type = null;
        public DebugCommandBase(string commandID, string commandDescription, string commandFormat)
        {
            this.commandID = commandID;
            this.commandDescription = commandDescription;
            this.commandFormat = commandFormat;
        }
    }

    public class DebugCommand : DebugCommandBase
    {
        private System.Action command;

        public DebugCommand(string commandID, string commandDescription, string commandFormat, 
            Action command) : base (commandID, commandDescription, commandFormat)
        {
            this.command = command;

        }

        public void Invoke()
        {
            command.Invoke();
        }
    }
    public class DebugCommand<T1> : DebugCommandBase
    {
        private System.Action<T1> command;

        public DebugCommand(string commandID, string commandDescription, string commandFormat,
            Action<T1> command) : base(commandID, commandDescription, commandFormat)
        {
            this.command = command;
            this.Type = typeof(T1);
        }

        public void Invoke(T1 value)
        {
            command.Invoke(value);
        }
    }
}
