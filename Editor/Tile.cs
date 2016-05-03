﻿using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;

namespace MonoTroid
{
    public class Tile : GameObject
    {
        public override void Initialise(EntityManager entityManager, Vector2 spawnPosition)
        {
            base.Initialise(entityManager, spawnPosition);
            frameSize = new Vector2(16, 16);
            HitRect = new Rectangle((int)position.X, (int)position.Y, (int)frameSize.X, (int)frameSize.Y);
        }

        public override void Update(GameTime gameTime)
        {
            
        }

        public override void Draw(SpriteBatch spriteBatch)
        {
            spriteBatch.DrawFilledRectangle(HitRect, Color.Purple);
            spriteBatch.DrawRectangle(HitRect, Color.Green);
        }

        public override void Collide(GameObject other)
        {
            
        }
    }
}