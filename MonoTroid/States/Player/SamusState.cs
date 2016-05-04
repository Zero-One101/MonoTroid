using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace MonoTroid.States.Player
{
    /// <summary>
    /// A State object to encapsulate some of Samus' functionality
    /// </summary>
    abstract class SamusState
    {
        /// <summary>
        /// Called when entering a new state
        /// </summary>
        /// <param name="context"></param>
        public abstract void Begin(Samus context);

        /// <summary>
        /// Updates the state
        /// </summary>
        /// <param name="context"></param>
        /// <param name="gameTime"></param>
        public virtual void Update(Samus context, GameTime gameTime)
        {
            HandleInput(context, gameTime);
            context.Animation.Update(gameTime);
        }

        /// <summary>
        /// Handles the input sent to Samus by the player
        /// </summary>
        /// <param name="context"></param>
        /// <param name="gameTime"></param>
        protected abstract void HandleInput(Samus context, GameTime gameTime);
    }
}
