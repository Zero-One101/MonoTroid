using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;
using Microsoft.Xna.Framework.Input;
using MonoTroid.States.Player;

namespace MonoTroid
{
    class Samus : GameObject
    {
        public readonly List<Keys> downKeys = new List<Keys>();
        public readonly List<Keys> upKeys = new List<Keys>();
        public Animation Animation { get; set; }
        public SamusState state;

        public override void Initialise(EntityManager entityManager, Vector2 spawnPosition)
        {
            base.Initialise(entityManager, spawnPosition);
            entityManager.KeyDown += EntityManager_KeyDown;
            entityManager.KeyUp += EntityManager_KeyUp;
            maxMoveSpeed = 5f;
            terminalVelocity = 5f;
            frameSize = new Vector2(16, 43);
            HitRect = new Rectangle((int)Position.X, (int)Position.Y, (int)frameSize.X, (int)frameSize.Y);
            jumpStrength = -3f;
            state = new Standing();
            state.Begin(this);
            //texture = entityManager.ResourceManager.LoadTexture("SamusStand");
            //Animation = new Animation(entityManager, "SamusRunL", true, 10, 50f, 0);
        }

        private void EntityManager_KeyUp(object sender, KeyUpEventArgs e)
        {
            upKeys.Add(e.Key);
        }

        private void EntityManager_KeyDown(object sender, KeyDownEventArgs e)
        {
            downKeys.Add(e.Key);
        }

        public override void Update(GameTime gameTime)
        {
            ApplyGravity();
            HandleInput();
            HitRect = new Rectangle((int)Position.X, (int)Position.Y, (int)frameSize.X, (int)frameSize.Y);
            state.Update(this, gameTime);
            ClearKeys();
        }

        private void ApplyGravity()
        {
            moveSpeed.Y += Gravity;
            moveSpeed.Y = moveSpeed.Y > terminalVelocity ? terminalVelocity : moveSpeed.Y;
        }

        private void HandleInput()
        {
            if (downKeys.Contains(Keys.Left))
            {
                moveSpeed.X = -maxMoveSpeed;
            }
            if (downKeys.Contains(Keys.Right))
            {
                moveSpeed.X = maxMoveSpeed;
            }
            if (downKeys.Contains(Keys.X))
            {
                if (!hasJumped)
                {
                    moveSpeed.Y += jumpStrength;
                    hasJumped = true;
                }
            }

            if (upKeys.Contains(Keys.Left) || upKeys.Contains(Keys.Right))
            {
                moveSpeed.X = 0;
            }

            Position += moveSpeed;
        }

        private void ClearKeys()
        {
            downKeys.Clear();
            upKeys.Clear();
        }

        public override void Draw(SpriteBatch spriteBatch)
        {
            //spriteBatch.DrawFilledRectangle(new Rectangle((int)position.X, (int)position.Y, (int)frameSize.X, (int)frameSize.Y), Color.Red);
            //spriteBatch.Draw(texture, position, Color.White);
            state.Draw(this, spriteBatch);
            spriteBatch.DrawRectangle(HitRect, Color.Green);
        }

        public override void Collide(GameObject other)
        {
            
        }
    }
}
