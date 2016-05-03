using System;
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
            HitRect = new Rectangle((int)Position.X, (int)Position.Y, (int)frameSize.X, (int)frameSize.Y);
            texture = entityManager.ResourceManager.LoadTexture("RBTile");
        }

        public override void Update(GameTime gameTime)
        {
            
        }

        public override void Draw(SpriteBatch spriteBatch)
        {
            spriteBatch.Draw(texture, Position, Color.White);
            //spriteBatch.DrawRectangle(HitRect, Color.Green);
        }

        public override void Collide(GameObject other)
        {
            
        }
    }
}
