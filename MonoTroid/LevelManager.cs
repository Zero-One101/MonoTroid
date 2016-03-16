using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.Linq;
using System.Text;
using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;

namespace MonoTroid
{
    public class LevelManager
    {
        private Level level;
        private readonly EntityManager entityManager;

        public LevelManager(EntityManager entityManager)
        {
            this.entityManager = entityManager;
        }

        public bool LoadLevel(string levelName)
        {
            ILevelSerialiser levelSerialiser = new BinLevelSerialiser();
            try
            {
                level = levelSerialiser.LoadLevel(entityManager, levelName);
                return true;
            }
            catch
            {
                return false;
            }
        }

        public void Draw(SpriteBatch spriteBatch)
        {
            level.Draw(spriteBatch);
        }

        public void CheckCollisions(GameObject entity)
        {
            foreach (var tile in level.Tiles)
            {
                if (tile != null)
                {
                    if (tile.HitRect.Intersects(entity.HitRect))
                    {
                        entity.ResolveTileCollision(tile.HitRect);
                    }
                }
            }
        }
    }
}
