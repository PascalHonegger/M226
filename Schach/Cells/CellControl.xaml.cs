using System.Windows.Input;

namespace Chess.Cells
{
	/// <summary>
	///     Interaction logic for CellControl.xaml
	/// </summary>
	public partial class CellControl
	{
		public CellControl()
		{
			InitializeComponent();
		}

		private CellViewModel MyDataCellViewModel => DataContext as CellViewModel;

		private new void MouseDown(object sender, MouseButtonEventArgs e)
		{
			MyDataCellViewModel.StartColorize();
			//myDataCellViewModel.Bgc = new SolidColorBrush(Color.FromArgb(75, 55, 55, 202));
		}
	}
}