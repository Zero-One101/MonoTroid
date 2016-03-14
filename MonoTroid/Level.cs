using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace MonoTroid
{
    public class Level
    {
        private Tile[,] tiles;

        public Level(Tile[,] tiles)
        {
            this.tiles = tiles;
        }
    }
}
