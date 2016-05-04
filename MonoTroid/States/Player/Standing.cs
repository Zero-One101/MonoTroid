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
    class Standing : OnGround
    {
        public override void Begin(Samus context)
        {
            context.Animation = context.Facing == GameObject.EFacing.ELeft
                ? new Animation(context.EntityManager, "Samus/StandL", true, 1, 1f, 0)
                : new Animation(context.EntityManager, "Samus/StandR", true, 1, 1f, 0);
        }

        protected override void HandleInput(Samus context, GameTime gameTime)
        {
            if ((context.downKeys.Contains(Keys.Left) && !context.downKeys.Contains(Keys.Right)) ||
                (context.downKeys.Contains(Keys.Right) && !context.downKeys.Contains(Keys.Left)))
            {
                context.Facing = context.downKeys.Contains(Keys.Right)
                    ? GameObject.EFacing.ERight
                    : GameObject.EFacing.ELeft;

                context.State = new Walking();
                context.State.Begin(context);
            }

            base.HandleInput(context, gameTime);
        }
    }
}
