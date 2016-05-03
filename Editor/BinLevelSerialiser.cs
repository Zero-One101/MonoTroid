using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using Microsoft.Xna.Framework;

namespace Editor
{
    public class BinLevelSerialiser : ILevelSerialiser
    {
        private Point levelSize = new Point(16, 14);

        public Level LoadLevel(EntityManager entityManager, string levelName)
        {
            var levelFile = entityManager.ResourceManager.LoadLevelFile(levelName);
            var tileData = new Tile[levelSize.X, levelSize.Y];

            for (var y = 0; y < levelSize.Y; y++)
            {
                for (var x = 0; x < levelSize.X; x++)
                {
                    if (levelFile[x + y * 16] != 0x0)
                    {
                        var tile = new Tile();
                        tile.Initialise(entityManager, new Vector2(x * 16, y * 16));
                        tileData[x, y] = tile;
                    }
                }
            }

            var level = new Level(tileData);
            return level;
        }

        public bool SaveLevel(string levelName)
        {
            throw new NotImplementedException();
        }
    }
}
