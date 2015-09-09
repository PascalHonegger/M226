using System.Drawing;
using System.Windows.Controls;
using System.Windows.Media;
using Color = System.Windows.Media.Color;

namespace Chess.BoardPieces.Cells
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
            myDataCellViewModel.Bgc = new SolidColorBrush(Color.FromArgb(50, 50, 50, 50));
        }
    }
}
