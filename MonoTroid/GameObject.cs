using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;

namespace MonoTroid
{
    public abstract class GameObject
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
        protected bool hasJumped;
        public bool IsDead { get; private set; } = false;
        protected Texture2D texture;

        public virtual void Initialise(EntityManager entityManager, Vector2 spawnPosition)
        {
            this.entityManager = entityManager;
            position = spawnPosition;
            nextPosition = position;
        }

        public abstract void Update(GameTime gameTime);
        public abstract void Draw(SpriteBatch spriteBatch);
        public abstract void Collide(GameObject other);

        public virtual void ResolveTileCollision(Rectangle otherRect)
        {
            var oldPos = position - moveSpeed;
            var oldYHitRect = new Rectangle((int)oldPos.X, (int)position.Y, (int)frameSize.X, (int)frameSize.Y);

            if (oldYHitRect.Intersects(otherRect))
            {
                position.Y -= moveSpeed.Y;
                moveSpeed.Y = 0;

                // If we hit our head off something, we shouldn't be able to jump again
                if (HitRect.Top < otherRect.Top)
                {
                    hasJumped = false;
                }
            }

            var oldXHitRect = new Rectangle((int)position.X, (int)oldPos.Y, (int)frameSize.X, (int)frameSize.Y);

            if (oldXHitRect.Intersects(otherRect))
            {
                position.X -= moveSpeed.X;
                moveSpeed.X = 0;
            }

            HitRect = new Rectangle((int)position.X, (int)position.Y, (int)frameSize.X, (int)frameSize.Y);
        }
    }
}
