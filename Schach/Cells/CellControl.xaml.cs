﻿using System.Windows.Input;

namespace Chess.Cells
{
	/// <summary>
	///     Interaction logic for CellControl.xaml
	/// </summary>
	public partial class CellControl
	{
		/// <summary>
		/// Standard constructor. InitializeComponent() gets called.
		/// </summary>
		public CellControl()
		{
			InitializeComponent();
		}

		private CellViewModel MyDataCellViewModel => DataContext as CellViewModel;

		private new void MouseDown(object sender, MouseButtonEventArgs e)
		{
			MyDataCellViewModel.Board.CellViewModelOnMouseDown(e, MyDataCellViewModel);
		}
	}
}