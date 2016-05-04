using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Microsoft.Xna.Framework;

namespace MonoTroid.States.Player
{
    class Jumping : InAir
    {
        public override void Begin(Samus context)
        {
            
        }

        public override void Update(Samus context, GameTime gameTime)
        {
            if (context.MoveSpeed.Y > 0)
            {
                context.State = new Falling();
                context.State.Begin(context);
            }

            base.Update(context, gameTime);
        }
    }
}
