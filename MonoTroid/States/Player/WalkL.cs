using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Input;

namespace MonoTroid.States.Player
{
    class WalkL : SamusState
    {
        public override void Begin(Samus context)
        {
            context.Animation = new Animation(context.EntityManager, "SamusRunL", true, 10, 50f, 0);
        }

        public override void Update(Samus context, GameTime gameTime)
        {
            if (context.upKeys.Contains(Keys.Left))
            {
                context.State = new Standing();
                context.State.Begin(context);
            }

            base.Update(context, gameTime);
        }
    }
}
