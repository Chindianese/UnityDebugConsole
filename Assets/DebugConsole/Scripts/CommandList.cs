using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace Chindianese.DebugConsole
{
    public class CommandList : MonoBehaviour
    {
        [Header("References")]
        [SerializeField]
        private DebugConsole console = null;
        //
        public List<DebugCommandBase> commandList;

        public void Awake()
        {
            commandList = new List<DebugCommandBase>();
            //------------
            var HELP = new DebugCommand("help", "Print all commands", "help", () =>
            {
                foreach (var command in commandList)
                {
                    console.PrintToConsole($"{command.commandFormat} - {command.commandDescription} ");
                }
            });
            commandList.Add(HELP);
            var LOG = new DebugCommand<string>("log", "Prints string to debug and unity console", "log <val>", (val) =>
            {
                Debug.Log(val);
                console.PrintToConsole(val);              
            });
            commandList.Add(LOG);



        }
        public DebugCommandBase GetCommand(string commandID)
        {
            return commandList.Find(x => x.commandID == commandID);
        }
    }
}