using System.Diagnostics;
using System.Windows;

namespace Chess
{
	/// <summary>
	///     Interaction logic for MainWindow.xaml
	/// </summary>
	public partial class MainWindow : Window
	{
		private IBoard _board;

		public MainWindow()
		{
			InitializeComponent();
			DataContext = _board = new Board();
		}

		private async void Button_Click(object sender, RoutedEventArgs e)
		{
			ComputerIsEnabledCheckBox.Visibility = Visibility.Hidden;
			_board = new Board
			{
				ComputerIsEnabled = _board.ComputerIsEnabled
			};
			await _board.CreateValues();
			await _board.StartRound();
			DataContext = _board;
		}
	}
}