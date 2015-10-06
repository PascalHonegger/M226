using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Data;
using System.Windows.Documents;
using System.Windows.Input;
using System.Windows.Media;
using System.Windows.Media.Imaging;
using System.Windows.Navigation;
using System.Windows.Shapes;

namespace Chess.Cells
{
	/// <summary>
	/// Interaction logic for HistoryControl.xaml
	/// </summary>
	public partial class HistoryControl
	{
		public int test { get; set; }

		public HistoryControl()
		{
			InitializeComponent();
		}

		private void OnDataContextChangedTo(object sender, DependencyPropertyChangedEventArgs e)
		{
			//throw new NotImplementedException();
		}

		private void OnDataContextChangedFrom(object sender, DependencyPropertyChangedEventArgs e)
		{
			//throw new NotImplementedException();
		}
	}
}
