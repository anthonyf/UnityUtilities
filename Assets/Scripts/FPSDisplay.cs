using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using UnityEngine;

namespace AF.UnityUtilities
{
    public class FPSDisplay : MonoBehaviour
    {
        float deltaTime = 0.0f;

        Dictionary<string, string> _properties;

        static FPSDisplay _instance;

        private void Start()
        {
            if (_instance == null)
            {
                _properties = new Dictionary<string, string>();
                _instance = this;
            }
            else
            {
                throw new InvalidOperationException();
            }
        }

        void Update()
        {
            deltaTime += (Time.deltaTime - deltaTime) * 0.1f;
        }

        void OnGUI()
        {
            int w = Screen.width, h = Screen.height;

            GUIStyle style = new GUIStyle();

            Rect rect = new Rect(0, 0, w, h * 2 / 100);
            style.alignment = TextAnchor.UpperLeft;
            style.fontSize = h * 2 / 100;
            style.normal.textColor = new Color(0.0f, 0.0f, 0.5f, 1.0f);
            float msec = deltaTime * 1000.0f;
            float fps = 1.0f / deltaTime;
            string text = string.Format("{0:0.0} ms ({1:0.} fps)", msec, fps);
            SetProperty("fps", text);

            var sb = new StringBuilder();
            foreach (var name in _properties.Keys)
            {
                sb.Append(name + ": " + _properties[name] + "\n");
            }
            GUI.Label(rect, sb.ToString(), style);
        }

        public static void SetProperty(string name, string value)
        {
            if (_instance != null)
            {
                _instance._properties[name] = value;
            }
        }
    }
}