using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;

namespace MonoTroid
{
    abstract class GameObject
    {
        private EntityManager entityManager;
        public bool IsDead { get; private set; } = false;

        public virtual void Initialise(EntityManager entityManager)
        {
            this.entityManager = entityManager;
        }

        public abstract void Update(GameTime gameTime);

        public abstract void Draw(SpriteBatch spriteBatch);
    }
}
