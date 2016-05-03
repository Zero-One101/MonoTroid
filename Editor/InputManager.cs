using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using Microsoft.Xna.Framework.Input;

namespace Editor
{
    /// <summary>
    /// Handles capturing and firing input events
    /// </summary>
    public class InputManager
    {
        public event KeyDownHandler KeyDown;
        public event KeyUpHandler KeyUp;
        private KeyboardState keyboardState;
        private readonly List<Keys> downKeys = new List<Keys>();
        private readonly List<Keys> upKeys = new List<Keys>();
        private readonly List<Keys> prevDownKeys = new List<Keys>();

        public void Update()
        {
            keyboardState = Keyboard.GetState();

            prevDownKeys.Clear();
            prevDownKeys.AddRange(downKeys);
            downKeys.Clear();
            upKeys.Clear();

            downKeys.AddRange(keyboardState.GetPressedKeys());

            foreach (var downKey in downKeys)
            {
                FireKeyDown(downKey);
            }

            foreach (var key in prevDownKeys.Where(key => !downKeys.Contains(key)))
            {
                upKeys.Add(key);
            }

            foreach (var upKey in upKeys)
            {
                FireKeyUp(upKey);
            }
        }

        private void FireKeyDown(Keys downKey)
        {
            KeyDown?.Invoke(this, new KeyDownEventArgs(downKey));
        }

        private void FireKeyUp(Keys upKey)
        {
            KeyUp?.Invoke(this, new KeyUpEventArgs(upKey));
        }
    }
}
