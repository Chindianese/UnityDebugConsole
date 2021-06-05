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
            var LOG = new DebugCommand<string>("log", "Logs string to unity console", "log", (val) =>
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