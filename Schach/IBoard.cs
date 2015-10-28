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
		/// <summary>
		/// List with all Cells
		/// </summary>
		List<CellViewModel> AllCells { get; }
		/// <summary>
		/// Determins which players turn it is
		/// </summary>
		bool WhiteTurn { get; set; }

		/// <summary>
		/// Method that gets called when a player clicks on a CellViewModel.
		/// </summary>
		/// <param name="mouseButtonState">Only checks wheter the Left or Right mousebutton is the ChangedButton</param>
		/// <param name="cellThatGotClicked">CellViewModel, that got clicked. Either selects or moves a Cell</param>
		void CellViewModelOnMouseDown(MouseButtonEventArgs mouseButtonState, CellViewModel cellThatGotClicked);
		/// <summary>
		///     Calculates all possible steps by calling the MarkPaths method on all Cells with a ChessPiece
		/// </summary>
		/// <param name="ignoreValidateMovement">
		///     If true, the ValidateMovement methode doesn't get called. That means, that a turn
		///     could cause illegal moves to be marked as legal
		/// </param>
		/// <returns></returns>
		Task CalculatePossibleSteps(bool ignoreValidateMovement = false);
		/// <summary>
		///     Validates a step
		/// </summary>
		/// <param name="from"></param>
		/// <param name="to"></param>
		/// <returns>True, when the step doesn't put the own king into checkmate</returns>
		Task<bool> ValidateMovement(CellViewModel from, CellViewModel to);
		/// <summary>
		/// Starts a new Turn. Calculates all possible steps, changes which players turn it is and clears the old possible steps
		/// </summary>
		/// <returns></returns>
		Task NextTurn();

		/// <summary>
		///     Add a new Element to the GraveYard
		/// </summary>
		/// <param name="cellViewModel">The Cell that will be added to the GraveYard</param>
		void AddToGraveYard(CellViewModel cellViewModel);
		/// <summary>
		///     Creates a normal chessboard either with or without the normal chesspieces
		/// </summary>
		/// <param name="hasDefaultValues">If false an empty Board gets initiated</param>
		/// <returns></returns>
		Task CreateValues(bool hasDefaultValues = true);
		/// <summary>
		///     Create a chessboard based on an already existing cellList. The cells inside of the cellList have to be ordered like
		///     this: A8 - B8 - C8 ... A7 - B7
		/// </summary>
		/// <param name="cellList">The sorted list the new ChessBoard is based on</param>
		/// <returns></returns>
		Task CreateChessBoardWithTemplate(List<CellViewModel> cellList);
		/// <summary>
		///     Calculates, wheter the King of the current Player is under attack
		/// </summary>
		/// <returns>True, when the current player is checkmated</returns>
		bool CalculateKingUnderAttack();
		/// <summary>
		/// Do the Initial step, start the Timer
		/// </summary>
		Task StartRound();

		List<KeyValuePair<CellViewModel, Path.Path>> AllPossibleSteps { get; }
	}
}