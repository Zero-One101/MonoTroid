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
        public enum EFacing
        {
            ELeft,
            ERight
        }

        public EFacing Facing { get; set; }
        public EntityManager EntityManager { get; private set; }
        public Rectangle HitRect { get; protected set; }
        public Vector2 Position { get; set; }
        protected Vector2 nextPosition;
        public Vector2 MoveSpeed { get; set; }
        protected Vector2 frameSize;
        public float maxMoveSpeed;
        protected float Gravity { get; } = 0.1f;
        protected float terminalVelocity;
        public float jumpStrength;
        public bool hasJumped;
        public bool IsDead { get; private set; } = false;
        protected Texture2D texture;

        public virtual void Initialise(EntityManager entityManager, Vector2 spawnPosition)
        {
            EntityManager = entityManager;
            Position = spawnPosition;
            nextPosition = Position;
        }

        public abstract void Update(GameTime gameTime);
        public abstract void Draw(SpriteBatch spriteBatch);
        public abstract void Collide(GameObject other);

        protected virtual void ApplyGravity()
        {
            MoveSpeed = MoveSpeed.Y + Gravity < terminalVelocity
                ? new Vector2(MoveSpeed.X, MoveSpeed.Y + Gravity)
                : new Vector2(MoveSpeed.X, terminalVelocity);
        }

        public virtual void ResolveTileCollision(Rectangle otherRect)
        {
            var oldPos = Position - MoveSpeed;
            var oldYHitRect = new Rectangle((int)oldPos.X, (int)Position.Y, (int)frameSize.X, (int)frameSize.Y);

            if (oldYHitRect.Intersects(otherRect))
            {
                //Position.Y -= moveSpeed.Y;
                Position = new Vector2(Position.X, oldPos.Y);
                MoveSpeed = new Vector2(MoveSpeed.X, 0);

                // If we hit our head off something, we shouldn't be able to jump again
                if (HitRect.Top < otherRect.Top)
                {
                    hasJumped = false;
                }
            }

            var oldXHitRect = new Rectangle((int)Position.X, (int)oldPos.Y, (int)frameSize.X, (int)frameSize.Y);

            if (oldXHitRect.Intersects(otherRect))
            {
                //Position.X -= moveSpeed.X;
                Position = new Vector2(oldPos.X, Position.Y);
                MoveSpeed = new Vector2(0, MoveSpeed.Y);
            }

            HitRect = new Rectangle((int)Position.X, (int)Position.Y, (int)frameSize.X, (int)frameSize.Y);
        }
    }
}