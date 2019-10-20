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

        public MultiGrid(int rows, int columns, int size) {
            for (int i = 0; i < rows; i++) RowDefinitions.Add(new RowDefinition() { Height = new GridLength(size, GridUnitType.Pixel) });
            for (int i = 0; i < columns; i++) ColumnDefinitions.Add(new ColumnDefinition() { Width = new GridLength(size, GridUnitType.Pixel) });
            for (int i = 0; i < rows; i++) for (int j = 0; j < columns; j++) {
                    Border b = new Border() {
                        BorderThickness = new Thickness(1),
                        BorderBrush = Brushes.Black
                    };

                    Children.Add(b);
                    SetRow(b, i);
                    SetColumn(b, j);
                }

            Height = size * rows;
            Width = size * columns;

            Rows = rows;
            Columns = columns;
            Size = size;
        }
    }
}
