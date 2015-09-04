using System.Windows.Controls;
using System.Windows.Media;

namespace Chess.BoardPieces.Cells
{
    /// <summary>
    /// Interaction logic for CellControl.xaml
    /// </summary>
    public partial class CellControl : UserControl
    {
        public CellControl()
        {
            InitializeComponent();
        }
        public CellViewModel CellDataContext => DataContext as CellViewModel;
        public void SetToDefaultColor()
        {
            Background = CellDataContext == null ? Brushes.Red : CellDataContext.Bgc;
        }
    }
}
