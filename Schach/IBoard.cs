using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.ComponentModel;
using System.Threading.Tasks;
using System.Windows.Input;
using Chess.Cells;
using Chess.ChessPieces;

namespace Chess
{
	public interface IBoard// : ICloneable
	{
		ObservableCollection<IChessPiece> GraveYard { get; }
		ObservableCollection<HistoryViewModel> History { get; }
		List<CellViewModel> AllCells { get; }
		bool IsNotCheckmated { get; }
		event PropertyChangedEventHandler PropertyChanged;
		Task CellViewModelOnMouseDown(MouseButtonEventArgs mouseButtonState, CellViewModel cellThatGotClicked);
		Task CalculatePossibleSteps(bool ignoreValidateMovement = false);
		Task<bool> ValidateMovement(CellViewModel from, CellViewModel to);
		void AddToGraveYard(CellViewModel cellViewModel);
		void AddToHistory(CellViewModel startModel, CellViewModel endModel);
		bool CalculateCheckmated();
	}
}