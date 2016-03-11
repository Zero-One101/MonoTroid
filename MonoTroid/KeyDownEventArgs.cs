using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using Microsoft.Xna.Framework.Input;

namespace MonoTroid
{
    class KeyDownEventArgs : EventArgs
    {
        public Keys Key { get; private set; }

        public KeyDownEventArgs(Keys key)
        {
            Key = key;
        }
    }
}
