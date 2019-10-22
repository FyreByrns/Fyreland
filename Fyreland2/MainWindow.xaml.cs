using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Data;
using System.Windows.Documents;
using System.Windows.Input;
using System.Windows.Media;
using System.Windows.Media.Imaging;
using System.Windows.Navigation;
using System.Windows.Shapes;

namespace Fyreland2 {
    public enum Tool {
        Erase, Air, Ground, Slope, PoleVertical, PoleHorizontal, Soft, Glass, Wormgrass, Entrance, Shortcut,
    }

    public enum ToolType {
        Single, Rect, OpenRect, Line, Circle, OpenCircle, AnotherToolMode
    }

    /// <summary>
    /// Interaction logic for MainWindow.xaml
    /// </summary>
    public partial class MainWindow : Window {
        MultiGrid contentGrid;
        Visibility toolVisibility = Visibility.Collapsed;
        Tool tool = Tool.Ground;
        ToolType toolType = ToolType.Single;

        public MainWindow() {
            InitializeComponent();

            ToolContainer.Visibility = toolVisibility;

            contentGrid = new MultiGrid(100, 100, 20) {
                Background = Brushes.Transparent
            };
            Tile.MouseClick += new TileClickedEventHandler(Tile_TileClickEvent);
            ContentSpace.Children.Add(contentGrid);

            foreach (string s in Enum.GetNames(typeof(Tool))) {
                RadioButton rb = new RadioButton() {
                    Content = s,
                    GroupName = "Tool",
                };
                rb.Checked += ToolChecked;
                ToolSelector.Children.Add(rb);
            }
            ToolSelector.Children.OfType<RadioButton>().FirstOrDefault().IsChecked = true;

            foreach (string s in Enum.GetNames(typeof(ToolType))) {
                RadioButton rb = new RadioButton() {
                    Content = s,
                    GroupName = "ToolType",
                };
                rb.Checked += ToolTypeChecked;
                ToolTypeSelector.Children.Add(rb);
            }
            ToolTypeSelector.Children.OfType<RadioButton>().FirstOrDefault().IsChecked = true;
        }

        private void ToolChecked(object sender, RoutedEventArgs e) {
            tool = (Tool)Enum.Parse(typeof(Tool), (sender as RadioButton)?.Content.ToString());
        }

        private void ToolTypeChecked(object sender, RoutedEventArgs e) {
            toolType = (ToolType)Enum.Parse(typeof(ToolType), (sender as RadioButton)?.Content.ToString());
        }

        private void Tile_TileClickEvent(object sender, Tile.TileEventArgs e) {
            int gridClickedX = (int)e.tile.Location.X;
            int gridClickedY = (int)e.tile.Location.Y;
            Debug.Text = $"{gridClickedX},{gridClickedY}";

            //Rectangle toAdd = new Rectangle() {
            //Fill = Brushes.Black,
            //Width = contentGrid.Size,
            //Height = contentGrid.Size
            //};


            //contentGrid.Children.Add(toAdd);
            //Grid.SetColumn(toAdd, gridClickedX);
            //Grid.SetRow(toAdd, gridClickedY);

            Tile clicked = e.tile;

            switch (toolType) {
                case ToolType.Single: ToolAccordingToTile(ref e.tile.info); e.tile.Update(); break;
                case ToolType.Rect: break;
                case ToolType.OpenRect: break;
                case ToolType.Line: break;
                case ToolType.Circle: break;
                case ToolType.OpenCircle: break;

                default: break;
            }
        }

        void ToolAccordingToTile(ref Tile.TileInfo working) {
            switch (tool) {
                case Tool.Air: {
                        working.isGround = false;
                        working.isSlope = false;
                        working.isGlass = false;
                        working.isSoft = false;
                        working.isWormGrass = false;
                        working.hasHorizontalPole = false;
                        working.hasVerticalPole = false;
                        working.hasShortcutEntrance = false;
                        break;
                    }
                case Tool.Ground: {
                        working.isGround = true;
                        working.isSlope = false;
                        working.isGlass = false;
                        working.isWormGrass = false;
                        working.hasHorizontalPole = false;
                        working.hasVerticalPole = false;
                        break;
                    }
                case Tool.Slope: {
                        working.isSlope = true;
                        working.isGround = false;
                        working.isGlass = false;
                        working.isWormGrass = false;
                        working.hasShortcutEntrance = false;
                        break;
                    }
                case Tool.PoleVertical: {
                        working.hasVerticalPole = true;
                        working.isGround = false;
                        working.isGlass = false;
                        working.isWormGrass = false;
                        break;
                    }
                case Tool.PoleHorizontal: {
                        working.hasHorizontalPole = true;
                        working.isGround = false;
                        working.isGlass = false;
                        working.isWormGrass = false;
                        break;
                    }
                case Tool.Soft: {
                        working.isSoft = true;
                        break;
                    }
                case Tool.Glass: {
                        working.isGlass = true;
                        break;
                    }
                case Tool.Wormgrass: {
                        working.isWormGrass = true;
                        break;
                    }
                case Tool.Entrance: {
                        working.isGround = true; working.hasShortcutEntrance = true;
                        break;
                    }
                case Tool.Shortcut: {
                        working.hasShortcutPip = true;
                        break;
                    }

                case Tool.Erase: working = new Tile.TileInfo(); break;
                default: break;
            }
        }

        private void ToolMenu_Click(object sender, RoutedEventArgs e) {
            if (ToolContainer.Visibility == Visibility.Visible) ToolContainer.Visibility = Visibility.Collapsed;
            else ToolContainer.Visibility = Visibility.Visible;
        }

        private void TileMenu_Click(object sender, RoutedEventArgs e) {
        }

        private void CameraMenu_Click(object sender, RoutedEventArgs e) {
        }
    }
}
