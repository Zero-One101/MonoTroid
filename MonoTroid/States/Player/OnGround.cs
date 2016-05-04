using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Input;

namespace MonoTroid.States.Player
{
    class OnGround : SamusState
    {
        public override void Begin(Samus context)
        {
            if (context.MoveSpeed.X == 0)
            {
                context.State = new Landing();
                context.State.Begin(context);
            }
            else
            {
                context.State = new Walking();
                context.State.Begin(context);
            }
        }

        protected override void HandleInput(Samus context, GameTime gameTime)
        {
            // Prevent jumping if we're already falling
            if (context.MoveSpeed.Y > context.Gravity)
            {
                context.hasJumped = true;

                context.State = new InAir();
                context.State.Begin(context);
            }

            if (context.downKeys.Contains(Keys.X))
            {
                if (!context.hasJumped)
                {
                    context.MoveSpeed = new Vector2(context.MoveSpeed.X, context.MoveSpeed.Y + context.jumpStrength);
                    context.hasJumped = true;

                    context.State = new InAir();
                    context.State.Begin(context);
                }
            }
        }
    }
}
