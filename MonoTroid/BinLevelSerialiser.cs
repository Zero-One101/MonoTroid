using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using Microsoft.Xna.Framework;
using MonoTroid.Managers;

namespace MonoTroid
{
    public class BinLevelSerialiser : ILevelSerialiser
    {
        private Point ScreenSize = new Point(16, 14);
        private int position = 0;
        private byte[] levelFile;

        public Level LoadLevel(EntityManager entityManager, string levelName)
        {
            var levelHeader = new LevelHeader();
            levelFile = entityManager.ResourceManager.LoadLevelFile(levelName);
            GetScreenCount(ref levelHeader);

            var levelSize = new Point(ScreenSize.X * levelHeader.ScreenXY.X, ScreenSize.Y * levelHeader.ScreenXY.Y);

            var tileData = new Tile[levelSize.X, levelSize.Y];

            for (var y = 0; y < levelSize.Y; y++)
            {
                for (var x = 0; x < levelSize.X; x++)
                {
                    if (ReadByte() != 0x0)
                    {
                        var tile = new Tile();
                        tile.Initialise(entityManager, new Vector2(x * 16, y * 16));
                        tileData[x, y] = tile;
                    }
                }
            }

            var level = new Level(tileData);
            level.SetHeader(levelHeader);
            return level;
        }

        private void GetScreenCount(ref LevelHeader levelHeader)
        {
            var screenX = ReadByte();
            var screenY = ReadByte();
            levelHeader.ScreenXY = new Point(screenX, screenY);
        }

        public bool SaveLevel(string levelName)
        {
            throw new NotImplementedException();
        }

        private byte ReadByte()
        {
            var data = levelFile[position];
            position++;
            return data;
        }
    }
}
