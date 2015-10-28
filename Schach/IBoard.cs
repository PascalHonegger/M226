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
	public interface IBoard : IDisposable
	{
		List<CellViewModel> AllCells { get; }
		bool WhiteTurn { get; set; }
		void CellViewModelOnMouseDown(MouseButtonEventArgs mouseButtonState, CellViewModel cellThatGotClicked);
		Task CalculatePossibleSteps(bool ignoreValidateMovement = false);
		Task<bool> ValidateMovement(CellViewModel from, CellViewModel to);
		Task NextTurn();
		void AddToGraveYard(CellViewModel cellViewModel);
		void AddToHistory(CellViewModel startModel, CellViewModel endModel);
		Task CreateValues(bool hasDefaultValues = true);
		Task CreateChessBoardWithTemplate(List<CellViewModel> cellList);
		bool CalculateKingUnderAttack();
		ObservableCollection<HistoryViewModel> History { get; }
		/// <summary>
		/// Do the Initial step, start the Timer
		/// </summary>
		Task StartRound();

		List<KeyValuePair<CellViewModel, Path.Path>> AllPossibleSteps { get; }
	}
}