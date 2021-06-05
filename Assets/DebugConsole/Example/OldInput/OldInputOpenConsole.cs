using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace Chindianese.DebugConsole.Example
{
    /// <Author>Tay Hao Cheng</Author>
    /// <summary>
    /// Uses old input system to open console. Untested.
    /// </summary> 
    public class OldInputOpenConsole : MonoBehaviour
    {
        [SerializeField]
        private DebugConsole debugConsole = null;


        private void Update()
        {
            if (Input.GetKeyUp(KeyCode.Tilde))
                debugConsole.OnToggleDebugConsole();
            if (Input.GetKeyUp(KeyCode.Return))
                debugConsole.OnReturn();
        }

    }
}