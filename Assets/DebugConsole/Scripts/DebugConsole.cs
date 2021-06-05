using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace Chindianese.DebugConsole
{
    [RequireComponent(typeof(CommandList))]
    public class DebugConsole : MonoBehaviour
    {
        //
        [Header("Console Values")]
        [SerializeField]
        private float logViewHeightMax = 60;
        [SerializeField]
        [Min(1)]
        private int fontSize = 15;
        // GUI Variables
        private bool consoleVisible = false;
        string input = "";
        private bool scrollToBottom = false;
        private Vector2 scroll;
        private List<string> logs = new List<string>();
        // commands
        private CommandList commandList = null;
        private void Awake()
        {
            commandList = GetComponent<CommandList>(); // required component
        }
        #region UserInput
        public void OnToggleDebugConsole()
        {
            Debug.Log("Toggle console");
          
            consoleVisible = !consoleVisible;
            if (consoleVisible)
                ShowConsole();
            else
                HideConsole();
        }
        private void ShowConsole()
        {
            GUI.FocusControl("debugTextField");
        }
        private void HideConsole()
        {

        }
        public void OnReturn()
        {
            Debug.Log("Handle Input");
            if (consoleVisible)
            {
                HandleInput();
                input = "";
            }
        }
        #endregion
        private void OnGUI()
        {
            if (!consoleVisible) return;
          
            float logHeight = fontSize * 1.5f;
            float logViewBuffer = 5f;
            float logViewHeight = Mathf.Min(logHeight * logs.Count + logViewBuffer, logViewHeightMax);
            float y = 0f;
            {
                if (logs.Count > 0)
                {
                    GUI.Box(new Rect(0, y, Screen.width, logViewHeight), "");
                    Rect viewport = new Rect(0, 0, Screen.width - 30, logHeight * logs.Count);
                    scroll = GUI.BeginScrollView(new Rect(0, y, Screen.width, logViewHeight), scroll, viewport);
                    int index = 0;
                    foreach (var log in logs)
                    {
                        GUI.skin.label.fontSize = fontSize;
                        Rect labelRect = new Rect(5, logHeight * index, viewport.width - 100, logHeight);
                        GUI.Label(labelRect, logs[index]);
                        index++;
                    }
                    if (scrollToBottom)
                    {
                        GUI.ScrollTo(new Rect(0, logHeight * logs.Count, 0, 0));
                        scrollToBottom = false;
                    }
                    GUI.EndScrollView();
                    y += logViewHeight;
                }
            }
            GUI.Box(new Rect(0, y, Screen.width, 30), "");
            GUI.backgroundColor = new Color(0, 0, 0, 0);
            GUI.SetNextControlName("debugTextField");
            input = GUI.TextField(new Rect(10f, y + 5f, Screen.width - 20f, 20f), input);
            GUI.FocusControl("debugTextField");
        }
        private void HandleInput()
        {
            List<string> properties = new List<string>(input.Split(' '));
            properties.RemoveAll(x => x == "");
            DebugCommandBase command = commandList.GetCommand(properties[0]); // case sensitive
            if (command == null)
                return;
            System.Type paramType = command.Type;
            if (paramType != null)
            {
                if (properties.Count < 2)
                {
                    Error_NullParameter();
                    return;
                }
                switch (paramType.Name)
                {
                    case "String":
                        (command as DebugCommand<string>).Invoke(properties[1]);
                        break;
                    case "Int32":
                        {
                            int result;
                            if (int.TryParse(properties[1], out result))
                                (command as DebugCommand<int>).Invoke(int.Parse(properties[1]));
                            else
                                Error_InvalidParameter();
                        }
                            break;
                    case "Single":
                        {
                            properties[1] = properties[1].Replace("f", string.Empty);
                            float result;
                            if (float.TryParse(properties[1], out result))
                                (command as DebugCommand<float>).Invoke(result);
                            else
                                Error_InvalidParameter();
                        }
                        break;
                }
            }
            else
            {
                (command as DebugCommand).Invoke();
            }
        }
        public void PrintToConsole(string value)
        {
            logs.Add(value);
            scrollToBottom = true;
        }
        #region ERROR_HANDLING
        private void Error_NullParameter()
        {
            PrintToConsole("No parameter provided.");
        }
        private void Error_InvalidParameter()
        {
            PrintToConsole("Invalid parameter.");
        }
        #endregion
    }
}