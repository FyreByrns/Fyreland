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
        Shape shape;
        public TileInfo info;

        public static event TileClickedEventHandler MouseClick;
        public static event TileMouseMovedEventHandler MouseMoved;

        public Point Location { get; private set; }

        public Tile(int x, int y) {
            Location = new Point(x, y);

            Background = Brushes.Transparent;

            MouseEnter += TileMouseEnter;
            MouseLeave += TileMouseLeave;
            MouseMove += TileMouseMove;
            MouseDown += TileMouseMove;
        }

        Shape inContentGrid;
        public void Update() {
            if (inContentGrid != null) MainWindow.instance.contentGrid.Children.Remove(inContentGrid);
            Background = Brushes.Transparent;

            if (info.Empty) shape = null;
            if (info.isGround) { shape = new Rectangle() { Fill = Brushes.Black }; }
            if (info.isSlope) { shape = new Triangle() { Fill = Brushes.Black }; }
            if (info.hasVerticalPole) {
                if (info.isSlope) shape = new PoleVertSlope() { Fill = Brushes.Black };
                else shape = new PoleVert() { Fill = Brushes.Black };
            }
            if (info.hasHorizontalPole) {
                if (info.isSlope) shape = new PoleHorizSlope() { Fill = Brushes.Black };
                else shape = new PoleHoriz() { Fill = Brushes.Black };
            }
            if (info.isSoft) shape = new Soft() { Fill = Brushes.Black };

            if (shape != null) {
                MainWindow.instance.contentGrid.Children.Add(shape);
                shape.MouseEnter += TileMouseEnter;
                shape.MouseLeave += TileMouseLeave;
                shape.MouseMove += TileMouseMove;
                inContentGrid = (Shape)MainWindow.instance.contentGrid.Children[MainWindow.instance.contentGrid.Children.Count - 1];

                Grid.SetColumn(shape, (int)Location.X);
                Grid.SetRow(shape, (int)Location.Y);
            }
            else MainWindow.instance.contentGrid.Children.Remove(inContentGrid);
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
            public bool Empty => !(isGround || isSlope || isPole || isSoft || isGlass || isWormGrass || hasShortcutEntrance || hasShortcutPip || hasHorizontalPole || hasVerticalPole);

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
