using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;
using MonoTroid.Managers;

namespace MonoTroid
{
    public class Tile : GameObject
    {
        public enum ECollisionType
        {
            ESolid,
            ESlope
        }

        public ECollisionType Collision { get; set; }

        public override void Initialise(EntityManager entityManager, Vector2 spawnPosition)
        {
            base.Initialise(entityManager, spawnPosition);
            frameSize = new Vector2(16, 16);
            var points = new List<Vector2>()
            {
                new Vector2(Position.X, Position.Y),
                new Vector2(Position.X + frameSize.X, Position.Y),
                new Vector2(Position.X + frameSize.X, Position.Y + frameSize.Y),
                new Vector2(Position.X,
                    Position.Y + frameSize.Y)
            };
            Hit = new Polygon(points);
            Collision = ECollisionType.ESolid;
            texture = entityManager.ResourceManager.LoadTexture("RBTile");
        }

        public override void Update(GameTime gameTime)
        {
            
        }

        public void SetCollisionType(ECollisionType collision)
        {
            Collision = collision;

            if (collision == ECollisionType.ESlope)
            {
                texture = EntityManager.ResourceManager.LoadTexture("RBSlope");
            }
        }

        public override void Draw(SpriteBatch spriteBatch)
        {
            spriteBatch.Draw(texture, Position, Color.White);
            spriteBatch.DrawRectangle(new Rectangle(Position.ToPoint(), frameSize.ToPoint()), Color.Red);
        }

        public override void Collide(GameObject other)
        {
            
        }
    }
}
