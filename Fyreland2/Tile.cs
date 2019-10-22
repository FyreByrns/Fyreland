using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows;
using System.Windows.Media;
using System.Windows.Controls;
using System.Windows.Shapes;
using System.Windows.Media.Imaging;

namespace Fyreland2 {

    public delegate void TileClickedEventHandler(object source, Tile.TileEventArgs e);
    public delegate void TileMouseMovedEventHandler(object source, Tile.TileEventArgs e);

    public class Tile : Panel {
        Grid iconGrid;
        public TileInfo info;

        public static event TileClickedEventHandler MouseClick;
        public static event TileMouseMovedEventHandler MouseMoved;

        public Point Location { get; private set; }

        public Tile(int x, int y) {
            iconGrid = new Grid();
            Children.Add(iconGrid);
            Location = new Point(x, y);

            Background = Brushes.Transparent;

            MouseEnter += TileMouseEnter;
            MouseLeave += TileMouseLeave;
            MouseMove += TileMouseMove;
            MouseDown += TileMouseMove;
        }

        public void Update() {
            if (info.isGround) Background = Brushes.Black;
            if (info.isSlope) Background = Brushes.Blue;

        }

        private void TileMouseMove(object sender, System.Windows.Input.MouseEventArgs e) {
            var eargs = new TileEventArgs(this);
            MouseMoved?.Invoke(sender, eargs);
            if (e.LeftButton == System.Windows.Input.MouseButtonState.Pressed) MouseClick?.Invoke(sender, eargs);
        }

        private void TileMouseEnter(object sender, System.Windows.Input.MouseEventArgs e) {
            Background = Brushes.Teal;
            Update();
        }

        private void TileMouseLeave(object sender, System.Windows.Input.MouseEventArgs e) {
            Background = Brushes.Transparent;
            Update();
        }

        public struct TileInfo {
            public bool isGround;
            public bool isSlope;
            public bool isPole;
            public bool isSoft;
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
