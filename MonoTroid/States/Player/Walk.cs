using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Input;

namespace MonoTroid.States.Player
{
    class Walk : SamusState
    {
        public override void Begin(Samus context)
        {
            context.Animation = context.Facing == GameObject.EFacing.ELeft
                ? new Animation(context.EntityManager, "Samus/SamusRunL", true, 10, 50f, 0)
                : new Animation(context.EntityManager, "Samus/SamusRunR", true, 10, 50f, 0);
        }

        public override void Update(Samus context, GameTime gameTime)
        {
            if (context.upKeys.Contains(Keys.Left) && context.Facing == GameObject.EFacing.ELeft ||
                context.upKeys.Contains(Keys.Right) && context.Facing == GameObject.EFacing.ERight)
            {
                context.State = new Standing();
                context.State.Begin(context);
            }

            base.Update(context, gameTime);
        }
    }
}
