using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;

namespace MonoTroid
{
    public class Level
    {
        public LevelHeader Header { get; private set; }

        /// <summary>
        /// The 2D tile array that defines the structure of the level
        /// </summary>
        public Tile[,] Tiles { get; private set; }

        public Level(Tile[,] tiles)
        {
            Tiles = tiles;
        }

        public void Draw(SpriteBatch spriteBatch)
        {
            foreach (var tile in Tiles)
            {
                tile?.Draw(spriteBatch);
            }
        }

        public void SetHeader(LevelHeader header)
        {
            Header = header;
        }
    }

    public struct LevelHeader
    {
        public Point ScreenXY { get; set; }
    }
}
