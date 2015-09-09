using System.Windows.Controls;
using System.Windows.Media;
using Color = System.Windows.Media.Color;

namespace Chess.Cells
{
    /// <summary>
    /// Interaction logic for CellControl.xaml
    /// </summary>
    public partial class CellControl : UserControl
    {
        private CellViewModel MyDataCellViewModel => DataContext as CellViewModel;


        public CellControl()
        {
            InitializeComponent();
        }

        private void Image_MouseDown(object sender, System.Windows.Input.MouseButtonEventArgs e)
        {
            MyDataCellViewModel.Eat();
            //myDataCellViewModel.Bgc = new SolidColorBrush(Color.FromArgb(75, 55, 55, 202));
        }
    }
}
