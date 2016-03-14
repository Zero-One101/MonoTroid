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
        private Point levelSize = new Point(16, 14);

        public LevelManager(EntityManager entityManager)
        {
            this.entityManager = entityManager;
        }

        public bool LoadLevel(string levelName)
        {
            var levelFile = entityManager.ResourceManager.LoadLevelFile(levelName);
            var tileData = new Tile[levelSize.X, levelSize.Y];

            for (var y = 0; y < levelSize.Y; y++)
            {
                for (var x = 0; x < levelSize.X; x++)
                {
                    Debug.Print((x + y * 16).ToString());
                    if (levelFile[x + y * 16] != 0x0)
                    {
                        var tile = new Tile();
                        tile.Initialise(entityManager, new Vector2(x * 16, y * 16));
                        tileData[x, y] = tile;
                    }
                }
            }

            level = new Level(tileData);
            return false;
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
