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
            
        }

        protected override void HandleInput(Samus context, GameTime gameTime)
        {
            if (context.downKeys.Contains(Keys.X))
            {
                if (!context.hasJumped)
                {
                    context.MoveSpeed = new Vector2(context.MoveSpeed.X, context.MoveSpeed.Y + context.jumpStrength);
                    context.hasJumped = true;
                }
            }
        }
    }
}
