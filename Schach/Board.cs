using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.ComponentModel;
using System.Linq;
using System.Runtime.CompilerServices;
using System.Threading.Tasks;
using System.Windows.Input;
using Chess.Annotations;
using Chess.CellLinkStrategy;
using Chess.Cells;
using Chess.ChessPieces;

namespace Chess
{
	public class Board : INotifyPropertyChanged, IBoard
	{
		private ObservableCollection<IChessPiece> _graveYard;
		private bool _isNotCheckmated = true;
		private bool _whiteTurn;

		public Board()
		{
			AllCells = new List<CellViewModel>(64);

			CellLinkStrategyList = new List<ICellLinkStrategy>
			{
				new DefaultCellLinkStrategy()
			};

			History = new ObservableCollection<HistoryViewModel>();

			_graveYard = new ObservableCollection<IChessPiece>();
		}

		private CellViewModel SelectedCellViewModel { get; set; }

		public ObservableCollection<IChessPiece> GraveYard
			=> _graveYard ?? (_graveYard = new ObservableCollection<IChessPiece>());

		public bool IsNotCheckmated
		{
			get { return _isNotCheckmated; }
			private set
			{
				_isNotCheckmated = value;
				OnPropertyChanged();
			}
		}

		/// <summary>
		///     List with the old Turns. May be used with the GUI and the "En Passant"
		/// </summary>
		public ObservableCollection<HistoryViewModel> History { get; }

		public CellViewModel A8 => GetCellViewModel(nameof(A8));
		public CellViewModel B8 => GetCellViewModel(nameof(B8));
		public CellViewModel C8 => GetCellViewModel(nameof(C8));
		public CellViewModel D8 => GetCellViewModel(nameof(D8));
		public CellViewModel E8 => GetCellViewModel(nameof(E8));
		public CellViewModel F8 => GetCellViewModel(nameof(F8));
		public CellViewModel G8 => GetCellViewModel(nameof(G8));
		public CellViewModel H8 => GetCellViewModel(nameof(H8));
		public CellViewModel A7 => GetCellViewModel(nameof(A7));
		public CellViewModel B7 => GetCellViewModel(nameof(B7));
		public CellViewModel C7 => GetCellViewModel(nameof(C7));
		public CellViewModel D7 => GetCellViewModel(nameof(D7));
		public CellViewModel E7 => GetCellViewModel(nameof(E7));
		public CellViewModel F7 => GetCellViewModel(nameof(F7));
		public CellViewModel G7 => GetCellViewModel(nameof(G7));
		public CellViewModel H7 => GetCellViewModel(nameof(H7));
		public CellViewModel A6 => GetCellViewModel(nameof(A6));
		public CellViewModel B6 => GetCellViewModel(nameof(B6));
		public CellViewModel C6 => GetCellViewModel(nameof(C6));
		public CellViewModel D6 => GetCellViewModel(nameof(D6));
		public CellViewModel E6 => GetCellViewModel(nameof(E6));
		public CellViewModel F6 => GetCellViewModel(nameof(F6));
		public CellViewModel G6 => GetCellViewModel(nameof(G6));
		public CellViewModel H6 => GetCellViewModel(nameof(H6));
		public CellViewModel A5 => GetCellViewModel(nameof(A5));
		public CellViewModel B5 => GetCellViewModel(nameof(B5));
		public CellViewModel C5 => GetCellViewModel(nameof(C5));
		public CellViewModel D5 => GetCellViewModel(nameof(D5));
		public CellViewModel E5 => GetCellViewModel(nameof(E5));
		public CellViewModel F5 => GetCellViewModel(nameof(F5));
		public CellViewModel G5 => GetCellViewModel(nameof(G5));
		public CellViewModel H5 => GetCellViewModel(nameof(H5));
		public CellViewModel A4 => GetCellViewModel(nameof(A4));
		public CellViewModel B4 => GetCellViewModel(nameof(B4));
		public CellViewModel C4 => GetCellViewModel(nameof(C4));
		public CellViewModel D4 => GetCellViewModel(nameof(D4));
		public CellViewModel E4 => GetCellViewModel(nameof(E4));
		public CellViewModel F4 => GetCellViewModel(nameof(F4));
		public CellViewModel G4 => GetCellViewModel(nameof(G4));
		public CellViewModel H4 => GetCellViewModel(nameof(H4));
		public CellViewModel A3 => GetCellViewModel(nameof(A3));
		public CellViewModel B3 => GetCellViewModel(nameof(B3));
		public CellViewModel C3 => GetCellViewModel(nameof(C3));
		public CellViewModel D3 => GetCellViewModel(nameof(D3));
		public CellViewModel E3 => GetCellViewModel(nameof(E3));
		public CellViewModel F3 => GetCellViewModel(nameof(F3));
		public CellViewModel G3 => GetCellViewModel(nameof(G3));
		public CellViewModel H3 => GetCellViewModel(nameof(H3));
		public CellViewModel A2 => GetCellViewModel(nameof(A2));
		public CellViewModel B2 => GetCellViewModel(nameof(B2));
		public CellViewModel C2 => GetCellViewModel(nameof(C2));
		public CellViewModel D2 => GetCellViewModel(nameof(D2));
		public CellViewModel E2 => GetCellViewModel(nameof(E2));
		public CellViewModel F2 => GetCellViewModel(nameof(F2));
		public CellViewModel G2 => GetCellViewModel(nameof(G2));
		public CellViewModel H2 => GetCellViewModel(nameof(H2));
		public CellViewModel A1 => GetCellViewModel(nameof(A1));
		public CellViewModel B1 => GetCellViewModel(nameof(B1));
		public CellViewModel C1 => GetCellViewModel(nameof(C1));
		public CellViewModel D1 => GetCellViewModel(nameof(D1));
		public CellViewModel E1 => GetCellViewModel(nameof(E1));
		public CellViewModel F1 => GetCellViewModel(nameof(F1));
		public CellViewModel G1 => GetCellViewModel(nameof(G1));
		public CellViewModel H1 => GetCellViewModel(nameof(H1));


		/// <summary>
		///     If true, the Computer will automatically take a move once the Black player has to move.
		/// </summary>
		public bool ComputerIsEnabled { get; set; } = true;

		public async Task StartRound()
		{
			WhiteTurn = true;

			await CalculatePossibleSteps();
		}

		public List<KeyValuePair<CellViewModel, Path.Path>> AllPossibleSteps
		{
			get
			{
				var possibleSteps = new List<KeyValuePair<CellViewModel, Path.Path>>();

				AllCells.ForEach(
					cell => cell.CanMoveHere.ForEach(path => possibleSteps.Add(new KeyValuePair<CellViewModel, Path.Path>(cell, path))));
				AllCells.ForEach(
					cell => cell.CanEatHere.ForEach(path => possibleSteps.Add(new KeyValuePair<CellViewModel, Path.Path>(cell, path))));

				return possibleSteps.Where(kvp => kvp.Value.IsWhite == WhiteTurn).ToList();
			}
		}

		public List<ICellLinkStrategy> CellLinkStrategyList { get; }

		/// <summary>
		///     Determins which players turn it is
		/// </summary>
		public bool WhiteTurn
		{
			get { return _whiteTurn; }
			set
			{
				_whiteTurn = value;
				OnPropertyChanged();
				//TODO Track time for each Player

				//TODO Give GUI-Feedback, that the turn counted and the other Color has to play
			}
		}

		/// <summary>
		///     List with all Cells
		/// </summary>
		public List<CellViewModel> AllCells { get; }

		/// <summary>
		///     Method that gets called when a player clicks on a CellViewModel.
		/// </summary>
		/// <param name="mouseButtonState">Only checks wheter the Left or Right mousebutton is the ChangedButton</param>
		/// <param name="cellThatGotClicked">CellViewModel, that got clicked. Either selects or moves a Cell</param>
		public async Task CellViewModelOnMouseDown(MouseButtonEventArgs mouseButtonState, CellViewModel cellThatGotClicked)
		{
			ResetColors();

			switch (mouseButtonState.ChangedButton)
			{
				case MouseButton.Left:
					await SelectCellViewModel(cellThatGotClicked);
					break;
				case MouseButton.Right:
					// Mark CellViewModel's which can move here, just colorisation
					foreach (var canMoveHere in cellThatGotClicked.CanMoveHere)
					{
						canMoveHere.StartCell.Bgc = CellViewModel.CanMoveHereColor;
					}
					foreach (var canEathere in cellThatGotClicked.CanEatHere)
					{
						canEathere.StartCell.Bgc = CellViewModel.CanEatHereColor;
					}
					break;
				case MouseButton.Middle:
					break;
				case MouseButton.XButton1:
					break;
				case MouseButton.XButton2:
					break;
				default:
					throw new ArgumentOutOfRangeException();
			}
		}

		/// <summary>
		///     Calculates all possible steps by calling the MarkPaths method on all Cells with a ChessPiece
		/// </summary>
		/// <param name="ignoreValidateMovement">
		///     If true, the ValidateMovement methode doesn't get called. That means, that a turn
		///     could cause illegal moves to be marked as legal
		/// </param>
		/// <returns></returns>
		public async Task CalculatePossibleSteps(bool ignoreValidateMovement = false)
		{
			foreach (var cell in AllCells.Where(cell => cell.CurrentChessPiece != null))
			{
				await cell.MarkPaths(ignoreValidateMovement);
			}
		}

		/// <summary>
		///     Create a chessboard based on an already existing cellList. The cells inside of the cellList have to be ordered like
		///     this: A8 - B8 - C8 ... A7 - B7
		/// </summary>
		/// <param name="cellList">The sorted list the new ChessBoard is based on</param>
		/// <returns></returns>
		public async Task CreateChessBoardWithTemplate(List<CellViewModel> cellList)
		{
			AllCells.Clear();

			AllCells.AddRange(cellList);

			await CreateLink();
		}

		/// <summary>
		///     Calculates, wheter the King of the current Player is under attack
		/// </summary>
		/// <returns>True, when the current player is checkmated</returns>
		public bool CalculateKingUnderAttack()
		{
			var checkedKing =
				AllCells.Where(cell => cell.CurrentChessPiece is King)
					.FirstOrDefault(kingCell => kingCell.CurrentChessPiece.IsWhite() == WhiteTurn);
			return checkedKing != null && checkedKing.CanEatHere.Any();
		}

		/// <summary>
		///     Validates a step
		/// </summary>
		/// <param name="from"></param>
		/// <param name="to"></param>
		/// <returns>True, when the step doesn't put the own king into checkmate</returns>
		public async Task<bool> ValidateMovement(CellViewModel from, CellViewModel to)
		{
			var tmpBoard = await CloneBoard();

			var tmpFrom = tmpBoard.AllCells.FirstOrDefault(cell => cell.Equals(from));
			var tmpTo = tmpBoard.AllCells.FirstOrDefault(cell => cell.Equals(to));

			await tmpBoard.CalculatePossibleSteps(true);

			CellViewModel.MoveModel(tmpFrom, tmpTo);

			foreach (var cell in tmpBoard.AllCells)
			{
				cell.CanEatHere.Clear();
				cell.CanMoveHere.Clear();
			}

			await tmpBoard.CalculatePossibleSteps(true);

			return !tmpBoard.CalculateKingUnderAttack();
		}

		/// <summary>
		///     Add a new Element to the GraveYard
		/// </summary>
		/// <param name="cellViewModel">The Cell that will be added to the GraveYard</param>
		public void AddToGraveYard(CellViewModel cellViewModel)
		{
			if (cellViewModel?.CurrentChessPiece == null)
			{
				return;
			}
			GraveYard.Add(cellViewModel.CurrentChessPiece);
		}

		/// <summary>
		///     Creates a normal chessboard either with or without the normal chesspieces
		/// </summary>
		/// <param name="hasDefaultValues">If false an empty Board gets initiated</param>
		/// <returns></returns>
		public async Task CreateValues(bool hasDefaultValues = true)
		{
			if (hasDefaultValues)
			{
				await CreateDefaultChessBoard();
			}
			else
			{
				await CreateEmptyChessBoard();
			}
		}

		public async Task NextTurn()
		{
			WhiteTurn = !WhiteTurn;

			foreach (var cell in AllCells)
			{
				cell.CanEatHere.Clear();
				cell.CanMoveHere.Clear();
			}

			await CalculatePossibleSteps();

			MarkCheck();

			IsNotCheckmated = AllPossibleSteps.Any();

			if (ComputerIsEnabled && !WhiteTurn)
			{
				await DoRandomStep();
			}
		}

		public event PropertyChangedEventHandler PropertyChanged;

		/// <summary>
		///     Add a new Element to the History
		/// </summary>
		/// <param name="startModel">The StartCell, which was selected first</param>
		/// <param name="endModel">The EndCell, which was moved / eaten to</param>
		private void AddToHistory(CellViewModel startModel, CellViewModel endModel)
		{
			if (startModel == null || endModel == null)
			{
				return;
			}

			var historyViewModel = new HistoryViewModel
			{
				FromCell = startModel,
				ToCell = endModel
			};

			History.Add(historyViewModel);
		}

		private async Task<IBoard> CloneBoard()
		{
			IBoard cloneBoard = new Board();

			var cellList = AllCells.Select(cell => cell.CloneCellViewModel()).ToList();

			cloneBoard.WhiteTurn = WhiteTurn;

			foreach (var cell in cellList)
			{
				cell.Board = cloneBoard;
			}

			await cloneBoard.CreateChessBoardWithTemplate(cellList);

			return cloneBoard;
		}

		private async Task SelectCellViewModel(CellViewModel cellThatGotClicked)
		{
			// NextTurn, when the ViewModel moved to the newly selected ViewModel
			if (CellViewModel.MoveModel(SelectedCellViewModel, cellThatGotClicked))
			{
				AddToHistory(SelectedCellViewModel, cellThatGotClicked);

				// Reset CheckedKingColor
				ResetColors(true);

				await NextTurn();

				SelectedCellViewModel = null;
			}
			// Select ViewModel, if it's the players turn
			else if (cellThatGotClicked?.CurrentChessPiece != null && WhiteTurn == cellThatGotClicked.CurrentChessPiece.IsWhite())
			{
				SelectedCellViewModel = cellThatGotClicked;
				SelectedCellViewModel.StartColorize();
				MarkCheck();
			}
			else
			{
				SelectedCellViewModel = null;
			}
		}

		private async Task DoRandomStep()
		{
			if (!IsNotCheckmated)
			{
				return;
			}
			var random = new Random();

			var values = AllPossibleSteps;

			var randomStep = values[random.Next(values.Count)];

			SelectedCellViewModel = null;

			await SelectCellViewModel(randomStep.Value.StartCell);

			await SelectCellViewModel(randomStep.Key);
		}

		private void MarkCheck()
		{
			foreach (
				var kingCell in AllCells.Where(cell => cell.CurrentChessPiece is King).Where(kingCell => kingCell.CanEatHere.Any()))
			{
				kingCell.Bgc = CellViewModel.IsCheckmateColor;
			}
		}

		private void ResetColors(bool resetCheckedKingColor = false)
		{
			foreach (
				var kvp in
					AllCells.Where(
						cellViewModel =>
							!Equals(cellViewModel.Bgc, CellViewModel.NothingColor) &&
							resetCheckedKingColor == Equals(cellViewModel.Bgc, CellViewModel.IsCheckmateColor)))
			{
				kvp.Bgc = CellViewModel.NothingColor;
			}
		}

		private async Task CreateDefaultChessBoard()
		{
			await CreateEmptyChessBoard();

			AllCells.First(cell => Equals(cell.Name, "A8")).CurrentChessPiece = new Rook(false);
			AllCells.First(cell => Equals(cell.Name, "B8")).CurrentChessPiece = new Knight(false);
			AllCells.First(cell => Equals(cell.Name, "C8")).CurrentChessPiece = new Bishop(false);
			AllCells.First(cell => Equals(cell.Name, "D8")).CurrentChessPiece = new Queen(false);
			AllCells.First(cell => Equals(cell.Name, "E8")).CurrentChessPiece = new King(false);
			AllCells.First(cell => Equals(cell.Name, "F8")).CurrentChessPiece = new Bishop(false);
			AllCells.First(cell => Equals(cell.Name, "G8")).CurrentChessPiece = new Knight(false);
			AllCells.First(cell => Equals(cell.Name, "H8")).CurrentChessPiece = new Rook(false);

			AllCells.First(cell => Equals(cell.Name, "A7")).CurrentChessPiece = new Pawn(false);
			AllCells.First(cell => Equals(cell.Name, "B7")).CurrentChessPiece = new Pawn(false);
			AllCells.First(cell => Equals(cell.Name, "C7")).CurrentChessPiece = new Pawn(false);
			AllCells.First(cell => Equals(cell.Name, "D7")).CurrentChessPiece = new Pawn(false);
			AllCells.First(cell => Equals(cell.Name, "E7")).CurrentChessPiece = new Pawn(false);
			AllCells.First(cell => Equals(cell.Name, "F7")).CurrentChessPiece = new Pawn(false);
			AllCells.First(cell => Equals(cell.Name, "G7")).CurrentChessPiece = new Pawn(false);
			AllCells.First(cell => Equals(cell.Name, "H7")).CurrentChessPiece = new Pawn(false);

			AllCells.First(cell => Equals(cell.Name, "A2")).CurrentChessPiece = new Pawn(true);
			AllCells.First(cell => Equals(cell.Name, "B2")).CurrentChessPiece = new Pawn(true);
			AllCells.First(cell => Equals(cell.Name, "C2")).CurrentChessPiece = new Pawn(true);
			AllCells.First(cell => Equals(cell.Name, "D2")).CurrentChessPiece = new Pawn(true);
			AllCells.First(cell => Equals(cell.Name, "E2")).CurrentChessPiece = new Pawn(true);
			AllCells.First(cell => Equals(cell.Name, "F2")).CurrentChessPiece = new Pawn(true);
			AllCells.First(cell => Equals(cell.Name, "G2")).CurrentChessPiece = new Pawn(true);
			AllCells.First(cell => Equals(cell.Name, "H2")).CurrentChessPiece = new Pawn(true);

			AllCells.First(cell => Equals(cell.Name, "A1")).CurrentChessPiece = new Rook(true);
			AllCells.First(cell => Equals(cell.Name, "B1")).CurrentChessPiece = new Knight(true);
			AllCells.First(cell => Equals(cell.Name, "C1")).CurrentChessPiece = new Bishop(true);
			AllCells.First(cell => Equals(cell.Name, "D1")).CurrentChessPiece = new Queen(true);
			AllCells.First(cell => Equals(cell.Name, "E1")).CurrentChessPiece = new King(true);
			AllCells.First(cell => Equals(cell.Name, "F1")).CurrentChessPiece = new Bishop(true);
			AllCells.First(cell => Equals(cell.Name, "G1")).CurrentChessPiece = new Knight(true);
			AllCells.First(cell => Equals(cell.Name, "H1")).CurrentChessPiece = new Rook(true);
		}

		private async Task CreateEmptyChessBoard()
		{
			for (var number = 1; number <= 8; number++)
			{
				for (int character = 'A'; character <= 'H'; character++)
				{
					var propertyName = (char) character + number.ToString();
					var property = new CellViewModel(null, this)
					{
						Name = propertyName
					};
					AllCells.Add(property);
				}
			}
			await CreateLink();
		}

		private async Task CreateLink()
		{
			await new TaskFactory().StartNew(
					() => { AllCells.ForEach(cell => CellLinkStrategyList.ForEach(s => s.LinkCellViewModel(cell, AllCells))); });
		}

		[NotifyPropertyChangedInvocator]
		private void OnPropertyChanged([CallerMemberName] string propertyName = null)
		{
			PropertyChanged?.Invoke(this, new PropertyChangedEventArgs(propertyName));
		}

		private CellViewModel GetCellViewModel(string cellName)
		{
			return AllCells.FirstOrDefault(o => Equals(cellName, o.Name));
		}
	}
}