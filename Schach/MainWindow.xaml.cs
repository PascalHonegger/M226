using System.Windows;

namespace Chess
{
	/// <summary>
	///     Interaction logic for MainWindow.xaml
	/// </summary>
	public partial class MainWindow : Window
	{
		private Board _board;

		public MainWindow()
		{
			InitializeComponent();
		}

		private async void Button_Click(object sender, RoutedEventArgs e)
		{
			_board?.Dispose();
			DataContext = null;
			_board = new Board();
			await _board.CreateValues();
			DataContext = _board;
		}
	}
}