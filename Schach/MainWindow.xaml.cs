using System.Windows;
using Chess.BoardPieces;

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
            new Board();
        }
    }
}