using System.Windows.Media.Imaging;

namespace Chess.Cells
{
	public class HistoryViewModel
	{
		public CellViewModel FromCell { get; set; }
		public CellViewModel ToCell { get; set; }

		public BitmapSource FromImage => FromCell.Image;

		public BitmapSource ToImage => ToCell.Image;

		public string FromText => FromCell.Name;

		public string ToText => ToCell.Name;
	}
}