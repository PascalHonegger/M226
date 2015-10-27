﻿using System.Diagnostics;
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
		}

		private async void Button_Click(object sender, RoutedEventArgs e)
		{
			_board?.Dispose();
			DataContext = null;
			ComputerIsEnabledCheckBox.Visibility = Visibility.Hidden;
			_board = new Board();
			await _board.CreateValues();
			DataContext = _board;
		}
	}
}