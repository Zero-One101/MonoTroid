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
        /// <summary>
        /// The possible directions a GameObject can face
        /// </summary>
        public enum EFacing
        {
            ELeft,
            ERight
        }

        /// <summary>
        /// The direction the GameObject is currently facing
        /// </summary>
        public EFacing Facing { get; set; }

        /// <summary>
        /// The EntityManager the GameObject is held in
        /// </summary>
        public EntityManager EntityManager { get; private set; }

        /// <summary>
        /// The bounding hitbox for the GameObject
        /// </summary>
        public Rectangle HitRect { get; protected set; }

        /// <summary>
        /// The position of the GameObject
        /// </summary>
        public Vector2 Position { get; set; }

        /// <summary>
        /// The possible position of the GameObject in the next frame
        /// </summary>
        protected Vector2 nextPosition;

        /// <summary>
        /// How fast the GameObject is moving
        /// </summary>
        public Vector2 MoveSpeed { get; set; }

        /// <summary>
        /// The size of the GameObject
        /// </summary>
        protected Vector2 frameSize;

        /// <summary>
        /// The maximum speed the GameObject can move
        /// </summary>
        public float maxMoveSpeed;

        /// <summary>
        /// How fast the GameObject will fall due to gravity
        /// </summary>
        protected float Gravity { get; } = 0.1f;

        /// <summary>
        /// The maximum speed the GameObject can achieve when falling
        /// </summary>
        protected float terminalVelocity;

        /// <summary>
        /// How strongly the GameObject can jump
        /// </summary>
        public float jumpStrength;

        /// <summary>
        /// Whether or not the GameObject has performed a jump. Resets on landing.
        /// </summary>
        public bool hasJumped;

        /// <summary>
        /// Whether or not the GameObject is dead and should be removed from the list of entities.
        /// </summary>
        public bool IsDead { get; private set; } = false;

        /// <summary>
        /// The texture of the GameObject.
        /// </summary>
        protected Texture2D texture;

        public virtual void Initialise(EntityManager entityManager, Vector2 spawnPosition)
        {
            EntityManager = entityManager;
            Position = spawnPosition;
            nextPosition = Position;
        }

        /// <summary>
        /// Updates the GameObject. This is where movement, gravity, state changing and input handling occur.
        /// </summary>
        /// <param name="gameTime"></param>
        public abstract void Update(GameTime gameTime);

        /// <summary>
        /// Draws the GameObject to the screen.
        /// </summary>
        /// <param name="spriteBatch"></param>
        public abstract void Draw(SpriteBatch spriteBatch);

        /// <summary>
        /// Called in order to resolve a collision between two GameObjects.
        /// </summary>
        /// <param name="other"></param>
        public abstract void Collide(GameObject other);

        /// <summary>
        /// Makes the GameObject fall in midair. May be overriden/uncalled with flying enemies
        /// </summary>
        protected virtual void ApplyGravity()
        {
            MoveSpeed = MoveSpeed.Y + Gravity < terminalVelocity
                ? new Vector2(MoveSpeed.X, MoveSpeed.Y + Gravity)
                : new Vector2(MoveSpeed.X, terminalVelocity);
        }

        /// <summary>
        /// On collision with a tile, attempts to move the GameObject out of the tile
        /// </summary>
        /// <param name="otherRect"></param>
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