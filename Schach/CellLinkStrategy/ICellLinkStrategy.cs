using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;
using Chess.Cells;

namespace Chess.CellLinkStrategy
{
	public interface ICellLinkStrategy
	{
		bool LinkCellViewModel(CellViewModel cellViewModel, List<CellViewModel> referenCellViewModels);
	}
}
