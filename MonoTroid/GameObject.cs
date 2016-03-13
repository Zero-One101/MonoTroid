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
        public Rectangle HitRect { get; protected set; }
        protected Vector2 position;
        protected Vector2 nextPosition;
        protected Vector2 moveSpeed;
        protected Vector2 frameSize;
        protected float maxMoveSpeed;
        protected float Gravity { get; } = 0.1f;
        protected float terminalVelocity;
        protected float jumpStrength;
        public bool IsDead { get; private set; } = false;

        public virtual void Initialise(EntityManager entityManager, Vector2 spawnPosition)
        {
            this.entityManager = entityManager;
            position = spawnPosition;
            nextPosition = position;
        }

        public abstract void Update(GameTime gameTime);
        public abstract void Draw(SpriteBatch spriteBatch);
        public abstract void Collide(GameObject other);
    }
}
