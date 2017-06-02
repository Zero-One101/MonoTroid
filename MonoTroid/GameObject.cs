using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;
using MonoTroid.Managers;

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
        public Polygon Hit { get; protected set; }

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
        /// Scaled by frame time
        /// </summary>
        public float maxMoveSpeed;

        /// <summary>
        /// How fast the GameObject will fall due to gravity
        /// Scaled by frame time
        /// </summary>
        public float Gravity { get; } = 6f;

        /// <summary>
        /// The maximum speed the GameObject can achieve when falling
        /// Scaled by frame time
        /// </summary>
        protected float terminalVelocity;

        /// <summary>
        /// How strongly the GameObject can jump
        /// Scaled by frame time
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
            MoveSpeed = new Vector2(MoveSpeed.X, Math.Min(MoveSpeed.Y + Gravity, terminalVelocity));
        }

        protected virtual void ApplyMovement(GameTime gameTime)
        {
            Position += MoveSpeed * (float)gameTime.ElapsedGameTime.TotalSeconds;
        }

        protected virtual void GenerateHitBox()
        {
            var points = new List<Vector2>()
            {
                new Vector2(Position.X, Position.Y),
                new Vector2(Position.X + frameSize.X, Position.Y),
                new Vector2(Position.X + frameSize.X, Position.Y + frameSize.Y),
                new Vector2(Position.X, Position.Y + frameSize.Y)
            };
            Hit = new Polygon(points);
        }

        /// <summary>
        /// On collision with a tile, attempts to move the GameObject out of the tile
        /// </summary>
        /// <param name="otherPoly"></param>
        /// <param name="mtv">The minimum translation vector required to resolve the collision</param>
        public virtual void ResolveTileCollision(Polygon otherPoly, Vector2 mtv, Tile.ECollisionType collisionType)
        {
            var translation = mtv;
            if (mtv.Y > 0)
            {
                if (Math.Abs(mtv.X) == Math.Abs(mtv.Y))
                {
                    // Prevents sliding on a 45 degree slope
                    // Position = new Vector2(Position.X + mtv.X, Position.Y);
                    translation = new Vector2(0, mtv.Y);
                }
                MoveSpeed = new Vector2(MoveSpeed.X, 0);
                hasJumped = false;
            }
            Position -= translation;
            GenerateHitBox();
        }
    }
}