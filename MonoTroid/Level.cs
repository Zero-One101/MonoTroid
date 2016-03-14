using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using Microsoft.Xna.Framework.Graphics;

namespace MonoTroid
{
    public class Level
    {
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
    }
}
