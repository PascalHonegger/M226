using System.Collections.Generic;
using Chess.Cells;

namespace Chess.CellLinkStrategy
{
	/// <summary>
	/// The standard Interface foo all Cell Link Strategies, which are 
	/// </summary>
	public interface ICellLinkStrategy
	{
		/// <summary>
		/// The Function which links a CellViewModel to his surrounders.
		/// </summary>
		/// <param name="cellViewModel">CellViewModel to link</param>
		/// <param name="referenCellViewModels">The list with ALL cellViewModels that has to contain at least the FieldViewModels which surround the to be linked CellViewModel</param>
		/// <returns>Wheter or not the Link can be established by the </returns>
		bool LinkCellViewModel(CellViewModel cellViewModel, List<CellViewModel> referenCellViewModels);
	}
}
