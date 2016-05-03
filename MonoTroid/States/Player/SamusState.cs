using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace MonoTroid.States.Player
{
    abstract class SamusState
    {
        public abstract void Begin();
        public abstract void Update(Samus context, GameTime gameTime);
        public abstract void Draw(Samus context, SpriteBatch spriteBatch);
    }
}
