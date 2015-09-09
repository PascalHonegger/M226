﻿using System.Windows.Controls;
using System.Windows.Media;
using Chess.BoardPieces.Cells;
using Color = System.Windows.Media.Color;

namespace Chess.Cells
{
    /// <summary>
    /// Interaction logic for CellControl.xaml
    /// </summary>
    public partial class CellControl : UserControl
    {
        private CellViewModel myDataCellViewModel => DataContext as CellViewModel;


        public CellControl()
        {
            InitializeComponent();
        }

        private void Image_MouseDown(object sender, System.Windows.Input.MouseButtonEventArgs e)
        {
            myDataCellViewModel.Bgc = new SolidColorBrush(Color.FromArgb(75, 55, 55, 202));
        }
    }
}
