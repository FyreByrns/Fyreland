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
    /// <summary>
    /// Interaction logic for MainWindow.xaml
    /// </summary>
    public partial class MainWindow : Window {
        MultiGrid contentGrid;
        Visibility toolVisibility = Visibility.Collapsed;

        public MainWindow() {
            InitializeComponent();

            ToolContainer.Visibility = toolVisibility;

            contentGrid = new MultiGrid(100, 100, 20) {
                Background = Brushes.Transparent
            };
            contentGrid.MouseMove += ContentSpace_MouseMove;
            ContentSpace.Children.Add(contentGrid);
        }

        private void ContentSpace_MouseMove(object sender, MouseEventArgs e) {
            if (e.LeftButton == MouseButtonState.Pressed) {
                Point test = e.GetPosition(contentGrid);
                int gridClickedX = (int)(test.X / contentGrid.Size);
                int gridClickedY = (int)(test.Y / contentGrid.Size);

                Rectangle toAdd = new Rectangle() {
                    Fill = Brushes.Black,
                    Width = contentGrid.Size,
                    Height = contentGrid.Size
                };

                Debug.Text = $"{gridClickedX},{gridClickedY}";

                contentGrid.Children.Add(toAdd);
                Grid.SetColumn(toAdd, gridClickedX);
                Grid.SetRow(toAdd, gridClickedY);
            }
        }

        void HideTools() { }
        void HideTiles() { }
        void HideCameras() { }

        private void ToolMenu_Click(object sender, RoutedEventArgs e) {
            if (toolVisibility == Visibility.Collapsed) toolVisibility = Visibility.Visible;
            else toolVisibility = Visibility.Collapsed;

            ToolContainer.Visibility = toolVisibility;
        }

        private void TileMenu_Click(object sender, RoutedEventArgs e) {
        }

        private void CameraMenu_Click(object sender, RoutedEventArgs e) {
        }
    }
}
