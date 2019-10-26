using System;
using System.Collections.Generic;
using System.Drawing;
using System.Drawing.Imaging;
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
            if (level == null) level = new Bitmap(tiles[0].GetLength(0) * 20, tiles[0].GetLength(1) * 20);

            for (int i = 0; i < tiles[0].GetLength(0); i++) for (int j = 0; j < tiles[0].GetLength(1); j++) {
                    Color col = Color.FromArgb(255, GetBaseRed(1, tiles[0][i, j].info.Empty ? 0 : 1, false), 0, 0);

                    for (int x = 0; x < 20; x++) for (int y = 0; y < 20; y++) {
                            level.SetPixel(i * 20 + x, j * 20 + y, col);
                        }
                }

            //unsafe {
            //    BitmapData bitmapData = level.LockBits(new Rectangle(0, 0, level.Width, level.Height), ImageLockMode.ReadWrite, level.PixelFormat);

            //    int bytesPerPixel = Bitmap.GetPixelFormatSize(level.PixelFormat) / 8;
            //    int heightInPixels = bitmapData.Height;
            //    int widthInBytes = bitmapData.Width * bytesPerPixel;
            //    byte* PtrFirstPixel = (byte*)bitmapData.Scan0;

            //    Parallel.For(0, heightInPixels, y => {
            //        byte* currentLine = PtrFirstPixel + (y * bitmapData.Stride);
            //        for (int x = 0; x < widthInBytes; x = x + bytesPerPixel) {
            //            int oldBlue = currentLine[x];
            //            int oldGreen = currentLine[x + 1];
            //            int oldRed = currentLine[x + 2];

            //            currentLine[x] = (byte)oldBlue;
            //            currentLine[x + 1] = (byte)oldGreen;
            //            currentLine[x + 2] = (byte)GetBaseRed(1, tiles[0][x / bytesPerPixel, 0].info.Empty ? 0 : 1, false);
            //        }
            //    });
            //    level.UnlockBits(bitmapData);
            //}

            level.Save("testlevel.png", ImageFormat.Png);
        }

        static int GetBaseRed(int layer, float blackToWhite, bool lit) {
            double oneToThirty = Min(30, Round(blackToWhite * 30, MidpointRounding.AwayFromZero));
            return (int)((oneToThirty % 10) + ((oneToThirty % 10) == 0).AsInt() * 10 + (blackToWhite > 1 / 3).AsInt() * 30 + (blackToWhite > 2 / 3).AsInt() * 30) + (layer - 1) * 10 + (lit ? 90 : 0);
        }
    }
}
