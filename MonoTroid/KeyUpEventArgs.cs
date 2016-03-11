using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using Microsoft.Xna.Framework.Input;

namespace MonoTroid
{
    class KeyUpEventArgs : EventArgs
    {
        public Keys Key { get; private set; }

        public KeyUpEventArgs(Keys key)
        {
            Key = key;
        }
    }
}
