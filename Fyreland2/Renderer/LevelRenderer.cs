using System;
using System.Collections.Generic;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Fyreland2.Renderer {
    public class LevelRenderer {
        public Tile[][,] tiles;
        public Bitmap level;

        public LevelRenderer(int width, int height) {
            tiles = new Tile[3][,];
            tiles[0] = new Tile[width, height];
        }

        public void Render() {
            if (level == null) level = new Bitmap(tiles.GetLength(0) * 20, tiles.GetLength(1) * 20);


        }

        static int GetBaseRed(int layer, float blackToWhite, bool lit) {
            int tret = 1;
            tret += layer * 10;



            return tret + (lit ? 90 : 0);
        }
    }
}
