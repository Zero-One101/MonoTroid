using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Input;

namespace MonoTroid.States.Player
{
    class Walking : OnGround
    {
        public override void Begin(Samus context)
        {
            context.Animation = context.Facing == GameObject.EFacing.ELeft
                ? new Animation(context.EntityManager, "Samus/RunL", true, 10, 50f, 0)
                : new Animation(context.EntityManager, "Samus/RunR", true, 10, 50f, 0);
        }

        protected override void HandleInput(Samus context, GameTime gameTime)
        {
            if (context.downKeys.Contains(Keys.Left))
            {
                if (context.Facing == GameObject.EFacing.ERight)
                {
                    context.Animation = new Animation(context.EntityManager, "Samus/RunL", true, 10, 50f, 0);
                }

                context.Facing = GameObject.EFacing.ELeft;
                context.MoveSpeed = new Vector2(-context.maxMoveSpeed, context.MoveSpeed.Y);
            }

            if (context.downKeys.Contains(Keys.Right))
            {
                if (context.Facing == GameObject.EFacing.ELeft)
                {
                    context.Animation = new Animation(context.EntityManager, "Samus/RunR", true, 10, 50f, 0);
                }

                context.Facing = GameObject.EFacing.ERight;
                context.MoveSpeed = new Vector2(context.maxMoveSpeed, context.MoveSpeed.Y);
            }

            // If both movement keys are held, cancel movement and go to a Standing state
            if (context.downKeys.Contains(Keys.Left) && context.downKeys.Contains(Keys.Right))
            {
                context.MoveSpeed = new Vector2(0, context.MoveSpeed.Y);
                context.State = new Standing();
                context.State.Begin(context);
            }

            if (context.upKeys.Contains(Keys.Left) || context.upKeys.Contains(Keys.Right))
            {
                context.MoveSpeed = new Vector2(0, context.MoveSpeed.Y);
            }

            if (context.upKeys.Contains(Keys.Left) && context.Facing == GameObject.EFacing.ELeft ||
                context.upKeys.Contains(Keys.Right) && context.Facing == GameObject.EFacing.ERight)
            {
                context.State = new Standing();
                context.State.Begin(context);
            }

            base.HandleInput(context, gameTime);
        }
    }
}
