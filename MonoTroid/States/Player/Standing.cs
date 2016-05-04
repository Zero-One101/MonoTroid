using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;
using Microsoft.Xna.Framework.Input;

namespace MonoTroid.States.Player
{
    class Standing : SamusState
    {
        public override void Begin(Samus context)
        {
            context.Animation = new Animation(context.EntityManager, "SamusStand", true, 1, 1.0f, 0);
        }

        public override void Update(Samus context, GameTime gameTime)
        {
            if (context.downKeys.Contains(Keys.Left))
            {
                context.State = new WalkL();
                context.State.Begin(context);
            }

            base.Update(context, gameTime);
        }
    }
}
