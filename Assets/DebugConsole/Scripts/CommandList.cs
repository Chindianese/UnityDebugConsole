using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace Chindianese.DebugConsole
{
    [RequireComponent(typeof(DebugConsole))]
    public class CommandList : MonoBehaviour
    {
        private DebugConsole console = null;
        //
        public List<DebugCommandBase> commandList;

        public void Awake()
        {
            console = GetComponent<DebugConsole>();
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

            var intTest = new DebugCommand<int>("intTest", "Prints int to console", "intTest <int>", (val) =>
            {
                console.PrintToConsole("int: " + val);
            });
            commandList.Add(intTest);
            var floatTest = new DebugCommand<float>("floatTest", "Prints float to console", "log <float>", (val) =>
            {
                console.PrintToConsole("float: " + val);
            });
            commandList.Add(floatTest);

        }
        public DebugCommandBase GetCommand(string commandID)
        {
            return commandList.Find(x => x.commandID == commandID);
        }
    }
}