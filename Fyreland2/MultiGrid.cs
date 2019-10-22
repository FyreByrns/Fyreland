using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows;
using System.Windows.Media;
using System.Windows.Controls;

namespace Fyreland2 {
    public class MultiGrid : Grid {
        public int Rows { get; private set; }
        public int Columns { get; private set; }
        public int Size { get; private set; }

        public Tile[,] tiles;

        public MultiGrid(int rows, int columns, int size) {
            tiles = new Tile[rows, columns];

            for (int i = 0; i < rows; i++) RowDefinitions.Add(new RowDefinition() { Height = new GridLength(size, GridUnitType.Pixel) });
            for (int i = 0; i < columns; i++) ColumnDefinitions.Add(new ColumnDefinition() { Width = new GridLength(size, GridUnitType.Pixel) });
            for (int i = 0; i < rows; i++) for (int j = 0; j < columns; j++) {
                    Tile tile = new Tile(j, i);
                    tiles[i, j] = tile;

                    Children.Add(tile);
                    SetRow(tile, i);
                    SetColumn(tile, j);
                }

            Height = size * rows;
            Width = size * columns;

            Rows = rows;
            Columns = columns;
            Size = size;
        }
    }
}
