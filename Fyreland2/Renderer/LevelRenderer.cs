using System;
using System.Collections.Generic;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using static System.Math;

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
            double oneToThirty = Min(30, Round(blackToWhite * 30, MidpointRounding.AwayFromZero));
            return (int)((oneToThirty % 10) + ((oneToThirty % 10) == 0).AsInt() * 10 + (blackToWhite > 1 / 3).AsInt() * 30 + (blackToWhite > 2 / 3).AsInt() * 30) + (layer-1) * 10 + (lit ? 90 : 0);
        }
    }
}
