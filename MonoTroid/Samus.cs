using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;
using Microsoft.Xna.Framework.Input;

namespace MonoTroid
{
    class Samus : GameObject
    {
        private readonly List<Keys> downKeys = new List<Keys>();
        private readonly List<Keys> upKeys = new List<Keys>();
        private Animation animation;

        public override void Initialise(EntityManager entityManager, Vector2 spawnPosition)
        {
            base.Initialise(entityManager, spawnPosition);
            entityManager.KeyDown += EntityManager_KeyDown;
            entityManager.KeyUp += EntityManager_KeyUp;
            maxMoveSpeed = 5f;
            terminalVelocity = 5f;
            frameSize = new Vector2(16, 43);
            HitRect = new Rectangle((int)position.X, (int)position.Y, (int)frameSize.X, (int)frameSize.Y);
            jumpStrength = -3f;
            texture = entityManager.ResourceManager.LoadTexture("SamusStand");
            animation = new Animation(entityManager, "SamusRunL", true, 10, 50f, 0);
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
            HitRect = new Rectangle((int)position.X, (int)position.Y, (int)frameSize.X, (int)frameSize.Y);
            animation.Update(gameTime);
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
            downKeys.Clear();

            if (upKeys.Contains(Keys.Left) || upKeys.Contains(Keys.Right))
            {
                moveSpeed.X = 0;
            }
            upKeys.Clear();

            position += moveSpeed;
        }

        public override void Draw(SpriteBatch spriteBatch)
        {
            //spriteBatch.DrawFilledRectangle(new Rectangle((int)position.X, (int)position.Y, (int)frameSize.X, (int)frameSize.Y), Color.Red);
            //spriteBatch.Draw(texture, position, Color.White);
            animation.Draw(spriteBatch, position);
            spriteBatch.DrawRectangle(HitRect, Color.Green);
        }

        public override void Collide(GameObject other)
        {
            
        }
    }
}
