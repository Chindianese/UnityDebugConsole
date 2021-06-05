using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace Chindianese.DebugConsole.Example
{
    /// <author>Tay Hao Cheng</author>
    /// <summary>
    /// Uses new input system for console controls. Check for value.performed as new input system is dumb and is called 3 times, down, performed and released or something like that.
    /// </summary>
    public class NewInputOpenConsole : MonoBehaviour
    {
        [SerializeField]
        private DebugConsole debugConsole = null;

        public void OnToggleConsole(UnityEngine.InputSystem.InputAction.CallbackContext value)
        {
            if (value.performed)
                debugConsole.OnToggleDebugConsole();
        }
        public void OnReturn(UnityEngine.InputSystem.InputAction.CallbackContext value)
        {
            if (value.performed)
                debugConsole.OnReturn();
        }
    }
}