using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Input;

namespace MonoTroid.States.Player
{
    class InAir : SamusState
    {
        public override void Begin(Samus context)
        {
            if (context.MoveSpeed.Y < 0)
            {
                context.State = new Jumping();
                context.State.Begin(context);
            }
            else
            {
                context.State = new Falling();
                context.State.Begin(context);
            }
        }

        public override void Update(Samus context, GameTime gameTime)
        {
            // Switch back to OnGround if we've landed
            if (!context.hasJumped)
            {
                context.State = new OnGround();
                context.State.Begin(context);
            }

            base.Update(context, gameTime);
        }

        protected override void HandleInput(Samus context, GameTime gameTime)
        {
            if (context.downKeys.Contains(Keys.Left))
            {
                if (context.Facing == GameObject.EFacing.ERight)
                {
                    // TODO: Switch to opposite animation
                }

                context.Facing = GameObject.EFacing.ELeft;
                context.MoveSpeed = new Vector2(-context.maxMoveSpeed, context.MoveSpeed.Y);
            }

            if (context.downKeys.Contains(Keys.Right))
            {
                if (context.Facing == GameObject.EFacing.ELeft)
                {
                    // TODO: Switch to opposite animation
                }

                context.Facing = GameObject.EFacing.ERight;
                context.MoveSpeed = new Vector2(context.maxMoveSpeed, context.MoveSpeed.Y);
            }

            if (context.downKeys.Contains(Keys.Left) && context.downKeys.Contains(Keys.Right))
            {
                context.MoveSpeed = new Vector2(0, context.MoveSpeed.Y);
            }

            if (context.upKeys.Contains(Keys.Left) || context.upKeys.Contains(Keys.Right))
            {
                context.MoveSpeed = new Vector2(0, context.MoveSpeed.Y);
            }
        }
    }
}
