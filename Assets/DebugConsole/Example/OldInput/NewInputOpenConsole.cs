using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace Chindianese.DebugConsole.Example
{
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