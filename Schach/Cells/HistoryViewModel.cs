using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Media.Imaging;

namespace Chess.Cells
{
	public class HistoryViewModel
	{
		public BitmapSource FromImage { get; set; }
		public BitmapSource ToImage { get; set; }
		public string FromText { get; set; }
		public string ToText { get; set; }
	}
}