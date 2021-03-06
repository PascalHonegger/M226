﻿using System.Windows;

namespace Chess
{
	/// <summary>
	///     Interaction logic for MainWindow.xaml
	/// </summary>
	public partial class MainWindow : Window
	{
		private IBoard _board;

		/// <summary>
		/// Entry-Point of the Application
		/// </summary>
		public MainWindow()
		{
			InitializeComponent();
			DataContext = _board = new Board();
		}

		private async void Button_Click(object sender, RoutedEventArgs e)
		{
			DataContext = null;
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