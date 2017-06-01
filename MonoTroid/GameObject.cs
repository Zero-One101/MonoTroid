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
            MoveSpeed = MoveSpeed.Y + Gravity < terminalVelocity
                ? new Vector2(MoveSpeed.X, MoveSpeed.Y + Gravity)
                : new Vector2(MoveSpeed.X, terminalVelocity);
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
            if (collisionType == Tile.ECollisionType.ESolid)
            {
                var oldPos = Position - (MoveSpeed * (float) EntityManager.gameTime.ElapsedGameTime.TotalSeconds);
                var points = new List<Vector2>()
                {
                    new Vector2(oldPos.X, Position.Y),
                    new Vector2(oldPos.X + frameSize.X, Position.Y),
                    new Vector2(oldPos.X + frameSize.X, Position.Y + frameSize.Y),
                    new Vector2(oldPos.X, Position.Y + frameSize.Y)
                };
                var oldYPoly = new Polygon(points);

                var hitResult = otherPoly.CheckCollision(oldYPoly, Vector2.Zero);
                if (hitResult.WillIntersect)
                {
                    Position = new Vector2(Position.X, oldPos.Y);
                    MoveSpeed = new Vector2(MoveSpeed.X, 0);

                    if (Hit.Points[0].Y < otherPoly.Points[0].Y)
                    {
                        hasJumped = false;
                    }
                }

                points = new List<Vector2>()
                {
                    new Vector2(Position.X, oldPos.Y),
                    new Vector2(Position.X + frameSize.X, oldPos.Y),
                    new Vector2(Position.X + frameSize.X, oldPos.Y + frameSize.Y),
                    new Vector2(Position.X, oldPos.Y + frameSize.Y)
                };
                var oldXPoly = new Polygon(points);

                hitResult = otherPoly.CheckCollision(oldXPoly, Vector2.Zero);
                if (hitResult.WillIntersect)
                {
                    Position = new Vector2(oldPos.X, Position.Y);
                    MoveSpeed = new Vector2(0, MoveSpeed.Y);
                }

                GenerateHitBox();
            }
            else
            {
                // Assume slope
                if (MoveSpeed.X != 0)
                {
                    var direction = MoveSpeed.X > 0 ? 1 : -1;
                    Position = new Vector2(Position.X + direction, Position.Y - 1);
                    GenerateHitBox();
                }
                else
                {
                    var oldPos = Position - (MoveSpeed * (float) EntityManager.gameTime.ElapsedGameTime.TotalSeconds);
                    Position = oldPos;
                    MoveSpeed = Vector2.Zero;
                    GenerateHitBox();
                }
            }
        }
    }
}