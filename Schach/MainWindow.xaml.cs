using System.ComponentModel;
using System.Linq;
using System.Windows;
using System.Windows.Input;
using Chess.BoardPieces;
using Chess.BoardPieces.Cells;

namespace Chess
{
    /// <summary>
    /// Interaction logic for MainWindow.xaml
    /// </summary>
    public partial class MainWindow : Window
    {
        public MainWindow()
        {
            InitializeComponent();
        }

        // Temporary
        private void StartButton_Click(object sender, RoutedEventArgs e)
        {
            foreach (CellControl c in ChessBoard.Children)
            {
                c.SetToDefaultColor();
            }
        }
    }
}