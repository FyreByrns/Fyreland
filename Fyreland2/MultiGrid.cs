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
                    Tile tile = new Tile(i, j);
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

    public class Tile : Panel {
        Grid iconGrid;
        Image[] icons;

        public event EventHandler<TileEventArgs> TileClickEvent;

        public Point Location { get; private set; }

        public Tile(int x, int y) {
            iconGrid = new Grid();
            icons = new Image[8];
            Location = new Point(x, y);

            Background = Brushes.Transparent;

            MouseEnter += TileMouseEnter;
            MouseLeave += TileMouseLeave;
        }

        private void TileMouseEnter(object sender, System.Windows.Input.MouseEventArgs e) {
            Background = Brushes.Teal;
        }

        private void TileMouseLeave(object sender, System.Windows.Input.MouseEventArgs e) {
            Background = Brushes.Transparent;
        }

        public struct TileInfo {
            public bool isTile;
            public bool isSlope;
            public bool isPole;
            public bool isSoftPlatform;
            public bool isGlass;
            public bool isWormGrass;
            public bool hasShortcutEntrance;
            public bool hasShortcutPip;
            public bool hasHorizontalPole;
            public bool hasVerticalPole;
        }

        public class TileEventArgs : EventArgs {
            public Tile tile;

            public TileEventArgs(Tile t) { tile = t; }
        }
    }
}
