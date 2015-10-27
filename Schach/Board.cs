using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.ComponentModel;
using System.Linq;
using System.Runtime.CompilerServices;
using System.Threading.Tasks;
using System.Windows.Input;
using Chess.Annotations;
using Chess.Cells;
using Chess.ChessPieces;

namespace Chess
{
	public class Board : INotifyPropertyChanged, IBoard
	{
		private Task _createLink;
		private ObservableCollection<IChessPiece> _graveYard;
		private bool _isNotCheckmated = true;
		private bool _whiteTurn;

		public Board()
		{
			AllCells = new List<CellViewModel>(64);

			History = new ObservableCollection<HistoryViewModel>();

			_graveYard = new ObservableCollection<IChessPiece>();
		}

		private CellViewModel SelectedCellViewModel { get; set; }

		public CellViewModel A8 { get; set; }
		public CellViewModel B8 { get; set; }
		public CellViewModel C8 { get; set; }
		public CellViewModel D8 { get; set; }
		public CellViewModel E8 { get; set; }
		public CellViewModel F8 { get; set; }
		public CellViewModel G8 { get; set; }
		public CellViewModel H8 { get; set; }
		public CellViewModel A7 { get; set; }
		public CellViewModel B7 { get; set; }
		public CellViewModel C7 { get; set; }
		public CellViewModel D7 { get; set; }
		public CellViewModel E7 { get; set; }
		public CellViewModel F7 { get; set; }
		public CellViewModel G7 { get; set; }
		public CellViewModel H7 { get; set; }
		public CellViewModel A6 { get; set; }
		public CellViewModel B6 { get; set; }
		public CellViewModel C6 { get; set; }
		public CellViewModel D6 { get; set; }
		public CellViewModel E6 { get; set; }
		public CellViewModel F6 { get; set; }
		public CellViewModel G6 { get; set; }
		public CellViewModel H6 { get; set; }
		public CellViewModel A5 { get; set; }
		public CellViewModel B5 { get; set; }
		public CellViewModel C5 { get; set; }
		public CellViewModel D5 { get; set; }
		public CellViewModel E5 { get; set; }
		public CellViewModel F5 { get; set; }
		public CellViewModel G5 { get; set; }
		public CellViewModel H5 { get; set; }
		public CellViewModel A4 { get; set; }
		public CellViewModel B4 { get; set; }
		public CellViewModel C4 { get; set; }
		public CellViewModel D4 { get; set; }
		public CellViewModel E4 { get; set; }
		public CellViewModel F4 { get; set; }
		public CellViewModel G4 { get; set; }
		public CellViewModel H4 { get; set; }
		public CellViewModel A3 { get; set; }
		public CellViewModel B3 { get; set; }
		public CellViewModel C3 { get; set; }
		public CellViewModel D3 { get; set; }
		public CellViewModel E3 { get; set; }
		public CellViewModel F3 { get; set; }
		public CellViewModel G3 { get; set; }
		public CellViewModel H3 { get; set; }
		public CellViewModel A2 { get; set; }
		public CellViewModel B2 { get; set; }
		public CellViewModel C2 { get; set; }
		public CellViewModel D2 { get; set; }
		public CellViewModel E2 { get; set; }
		public CellViewModel F2 { get; set; }
		public CellViewModel G2 { get; set; }
		public CellViewModel H2 { get; set; }
		public CellViewModel A1 { get; set; }
		public CellViewModel B1 { get; set; }
		public CellViewModel C1 { get; set; }
		public CellViewModel D1 { get; set; }
		public CellViewModel E1 { get; set; }
		public CellViewModel F1 { get; set; }
		public CellViewModel G1 { get; set; }
		public CellViewModel H1 { get; set; }

		public ObservableCollection<IChessPiece> GraveYard
			=> _graveYard ?? (_graveYard = new ObservableCollection<IChessPiece>());

		public async Task StartRound()
		{
			WhiteTurn = true;
			
			await CalculatePossibleSteps();
		}

		public bool IsNotCheckmated
		{
			get { return _isNotCheckmated; }
			private set
			{
				_isNotCheckmated = value;
				OnPropertyChanged();
			}
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

		/// <summary>
		/// If True the Black player will try to select a random turn, therefor simulating a Computer
		/// </summary>
		public bool ComputerIsEnabled { get; set; }

		/// <summary>
		/// List with the old Turns. May be used with the GUI and the "En Passant"
		/// </summary>
		public ObservableCollection<HistoryViewModel> History { get; }

		/// <summary>
		/// Determins which players turn it is
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
		/// List with all Cells
		/// </summary>
		public List<CellViewModel> AllCells { get; }

		/// <summary>
		/// Method that gets called when a player clicks on a CellViewModel.
		/// </summary>
		/// <param name="mouseButtonState">Only checks wheter the Left or Right mousebutton is the ChangedButton</param>
		/// <param name="cellThatGotClicked">CellViewModel, that got clicked. Either selects or moves a Cell</param>
		public async void CellViewModelOnMouseDown(MouseButtonEventArgs mouseButtonState, CellViewModel cellThatGotClicked)
		{
			ResetColors();

			switch (mouseButtonState.ChangedButton)
			{
				case MouseButton.Left:
					await OnSelect(cellThatGotClicked);
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
			A8 = cellList[0];
			B8 = cellList[1];
			C8 = cellList[2];
			D8 = cellList[3];
			E8 = cellList[4];
			F8 = cellList[5];
			G8 = cellList[6];
			H8 = cellList[7];

			A7 = cellList[8];
			B7 = cellList[9];
			C7 = cellList[10];
			D7 = cellList[11];
			E7 = cellList[12];
			F7 = cellList[13];
			G7 = cellList[14];
			H7 = cellList[15];

			A6 = cellList[16];
			B6 = cellList[17];
			C6 = cellList[18];
			D6 = cellList[19];
			E6 = cellList[20];
			F6 = cellList[21];
			G6 = cellList[22];
			H6 = cellList[23];

			A5 = cellList[24];
			B5 = cellList[25];
			C5 = cellList[26];
			D5 = cellList[27];
			E5 = cellList[28];
			F5 = cellList[29];
			G5 = cellList[30];
			H5 = cellList[31];

			A4 = cellList[32];
			B4 = cellList[33];
			C4 = cellList[34];
			D4 = cellList[35];
			E4 = cellList[36];
			F4 = cellList[37];
			G4 = cellList[38];
			H4 = cellList[39];

			A3 = cellList[40];
			B3 = cellList[41];
			C3 = cellList[42];
			D3 = cellList[43];
			E3 = cellList[44];
			F3 = cellList[45];
			G3 = cellList[46];
			H3 = cellList[47];

			A2 = cellList[48];
			B2 = cellList[49];
			C2 = cellList[50];
			D2 = cellList[51];
			E2 = cellList[52];
			F2 = cellList[53];
			G2 = cellList[54];
			H2 = cellList[55];

			A1 = cellList[56];
			B1 = cellList[57];
			C1 = cellList[58];
			D1 = cellList[59];
			E1 = cellList[60];
			F1 = cellList[61];
			G1 = cellList[62];
			H1 = cellList[63];

			await CreateLink();
		}

		/// <summary>
		///     Calculates, wheter the King of the current Player is under attack
		/// </summary>
		/// <returns>True, when the current player is checkmated</returns>
		public bool CalculateKingUnderAttack()
		{
			var checkedKing = AllCells.Where(cell => cell.CurrentChessPiece is King).FirstOrDefault(kingCell => kingCell.CurrentChessPiece.IsWhite() == WhiteTurn);
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
				FromCell = startModel, ToCell = endModel
			};

			History.Add(historyViewModel);
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

		public void Dispose()
		{
			Dispose(true);
			GC.SuppressFinalize(this);
		}

		public event PropertyChangedEventHandler PropertyChanged;

		private async Task<IBoard> CloneBoard()
		{
			IBoard cloneBoard = new Board();

			var cellList = new List<CellViewModel>(64)
			{
				A8.CloneCellViewModel(), B8.CloneCellViewModel(), C8.CloneCellViewModel(), D8.CloneCellViewModel(), E8.CloneCellViewModel(), F8.CloneCellViewModel(), G8.CloneCellViewModel(), H8.CloneCellViewModel(), A7.CloneCellViewModel(), B7.CloneCellViewModel(), C7.CloneCellViewModel(), D7.CloneCellViewModel(), E7.CloneCellViewModel(), F7.CloneCellViewModel(), G7.CloneCellViewModel(), H7.CloneCellViewModel(), A6.CloneCellViewModel(), B6.CloneCellViewModel(), C6.CloneCellViewModel(), D6.CloneCellViewModel(), E6.CloneCellViewModel(), F6.CloneCellViewModel(), G6.CloneCellViewModel(), H6.CloneCellViewModel(), A5.CloneCellViewModel(), B5.CloneCellViewModel(), C5.CloneCellViewModel(), D5.CloneCellViewModel(), E5.CloneCellViewModel(), F5.CloneCellViewModel(), G5.CloneCellViewModel(), H5.CloneCellViewModel(), A4.CloneCellViewModel(), B4.CloneCellViewModel(), C4.CloneCellViewModel(), D4.CloneCellViewModel(), E4.CloneCellViewModel(), F4.CloneCellViewModel(), G4.CloneCellViewModel(), H4.CloneCellViewModel(), A3.CloneCellViewModel(), B3.CloneCellViewModel(), C3.CloneCellViewModel(), D3.CloneCellViewModel(), E3.CloneCellViewModel(), F3.CloneCellViewModel(), G3.CloneCellViewModel(), H3.CloneCellViewModel(), A2.CloneCellViewModel(), B2.CloneCellViewModel(), C2.CloneCellViewModel(), D2.CloneCellViewModel(), E2.CloneCellViewModel(), F2.CloneCellViewModel(), G2.CloneCellViewModel(), H2.CloneCellViewModel(), A1.CloneCellViewModel(), B1.CloneCellViewModel(), C1.CloneCellViewModel(), D1.CloneCellViewModel(), E1.CloneCellViewModel(), F1.CloneCellViewModel(), G1.CloneCellViewModel(), H1.CloneCellViewModel()
			};

			cloneBoard.WhiteTurn = WhiteTurn;

			foreach (var cell in cellList)
			{
				cell.Board = cloneBoard;
			}

			await cloneBoard.CreateChessBoardWithTemplate(cellList);

			return cloneBoard;
		}

		private async Task OnSelect(CellViewModel cellThatGotClicked)
		{
			ResetColors();

			// NextTurn, when the ViewModel moved to the newly selected ViewModel
			if (CellViewModel.MoveModel(SelectedCellViewModel, cellThatGotClicked))
			{
				AddToHistory(SelectedCellViewModel, cellThatGotClicked);
				await NextTurn();
				SelectedCellViewModel = null;
			}
			// Select ViewModel, if it's the players turn
			else if (cellThatGotClicked?.CurrentChessPiece != null && WhiteTurn == cellThatGotClicked.CurrentChessPiece.IsWhite())
			{
				SelectedCellViewModel = cellThatGotClicked;
				SelectedCellViewModel.StartColorize();
			}
			else
			{
				SelectedCellViewModel = null;
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

			await OnSelect(randomStep.Value.StartCell);

			await OnSelect(randomStep.Key);
		}

		private void MarkCheck()
		{
			foreach (var kingCell in AllCells.Where(cell => cell.CurrentChessPiece is King).Where(kingCell => kingCell.CanEatHere.Any()))
			{
				kingCell.Bgc = CellViewModel.IsCheckmateColor;
			}
		}

		private void ResetColors()
		{
			foreach (var kvp in AllCells.Where(cellViewModel => !Equals(cellViewModel.Bgc, CellViewModel.NothingColor)))
			{
				kvp.Bgc = CellViewModel.NothingColor;
			}
		}

		private async Task CreateDefaultChessBoard()
		{
			A8 = new CellViewModel(new Rook(false), this);
			B8 = new CellViewModel(new Knight(false), this);
			C8 = new CellViewModel(new Bishop(false), this);
			D8 = new CellViewModel(new Queen(false), this);
			E8 = new CellViewModel(new King(false), this);
			F8 = new CellViewModel(new Bishop(false), this);
			G8 = new CellViewModel(new Knight(false), this);
			H8 = new CellViewModel(new Rook(false), this);

			A7 = new CellViewModel(new Pawn(false), this);
			B7 = new CellViewModel(new Pawn(false), this);
			C7 = new CellViewModel(new Pawn(false), this);
			D7 = new CellViewModel(new Pawn(false), this);
			E7 = new CellViewModel(new Pawn(false), this);
			F7 = new CellViewModel(new Pawn(false), this);
			G7 = new CellViewModel(new Pawn(false), this);
			H7 = new CellViewModel(new Pawn(false), this);

			A6 = new CellViewModel(null, this);
			B6 = new CellViewModel(null, this);
			C6 = new CellViewModel(null, this);
			D6 = new CellViewModel(null, this);
			E6 = new CellViewModel(null, this);
			F6 = new CellViewModel(null, this);
			G6 = new CellViewModel(null, this);
			H6 = new CellViewModel(null, this);

			A5 = new CellViewModel(null, this);
			B5 = new CellViewModel(null, this);
			C5 = new CellViewModel(null, this);
			D5 = new CellViewModel(null, this);
			E5 = new CellViewModel(null, this);
			F5 = new CellViewModel(null, this);
			G5 = new CellViewModel(null, this);
			H5 = new CellViewModel(null, this);

			A4 = new CellViewModel(null, this);
			B4 = new CellViewModel(null, this);
			C4 = new CellViewModel(null, this);
			D4 = new CellViewModel(null, this);
			E4 = new CellViewModel(null, this);
			F4 = new CellViewModel(null, this);
			G4 = new CellViewModel(null, this);
			H4 = new CellViewModel(null, this);

			A3 = new CellViewModel(null, this);
			B3 = new CellViewModel(null, this);
			C3 = new CellViewModel(null, this);
			D3 = new CellViewModel(null, this);
			E3 = new CellViewModel(null, this);
			F3 = new CellViewModel(null, this);
			G3 = new CellViewModel(null, this);
			H3 = new CellViewModel(null, this);

			A2 = new CellViewModel(new Pawn(true), this);
			B2 = new CellViewModel(new Pawn(true), this);
			C2 = new CellViewModel(new Pawn(true), this);
			D2 = new CellViewModel(new Pawn(true), this);
			E2 = new CellViewModel(new Pawn(true), this);
			F2 = new CellViewModel(new Pawn(true), this);
			G2 = new CellViewModel(new Pawn(true), this);
			H2 = new CellViewModel(new Pawn(true), this);

			A1 = new CellViewModel(new Rook(true), this);
			B1 = new CellViewModel(new Knight(true), this);
			C1 = new CellViewModel(new Bishop(true), this);
			D1 = new CellViewModel(new Queen(true), this);
			E1 = new CellViewModel(new King(true), this);
			F1 = new CellViewModel(new Bishop(true), this);
			G1 = new CellViewModel(new Knight(true), this);
			H1 = new CellViewModel(new Rook(true), this);

			await CreateLink();
		}

		private async Task CreateEmptyChessBoard()
		{
			A8 = new CellViewModel(null, this);
			B8 = new CellViewModel(null, this);
			C8 = new CellViewModel(null, this);
			D8 = new CellViewModel(null, this);
			E8 = new CellViewModel(null, this);
			F8 = new CellViewModel(null, this);
			G8 = new CellViewModel(null, this);
			H8 = new CellViewModel(null, this);

			A7 = new CellViewModel(null, this);
			B7 = new CellViewModel(null, this);
			C7 = new CellViewModel(null, this);
			D7 = new CellViewModel(null, this);
			E7 = new CellViewModel(null, this);
			F7 = new CellViewModel(null, this);
			G7 = new CellViewModel(null, this);
			H7 = new CellViewModel(null, this);

			A6 = new CellViewModel(null, this);
			B6 = new CellViewModel(null, this);
			C6 = new CellViewModel(null, this);
			D6 = new CellViewModel(null, this);
			E6 = new CellViewModel(null, this);
			F6 = new CellViewModel(null, this);
			G6 = new CellViewModel(null, this);
			H6 = new CellViewModel(null, this);

			A5 = new CellViewModel(null, this);
			B5 = new CellViewModel(null, this);
			C5 = new CellViewModel(null, this);
			D5 = new CellViewModel(null, this);
			E5 = new CellViewModel(null, this);
			F5 = new CellViewModel(null, this);
			G5 = new CellViewModel(null, this);
			H5 = new CellViewModel(null, this);

			A4 = new CellViewModel(null, this);
			B4 = new CellViewModel(null, this);
			C4 = new CellViewModel(null, this);
			D4 = new CellViewModel(null, this);
			E4 = new CellViewModel(null, this);
			F4 = new CellViewModel(null, this);
			G4 = new CellViewModel(null, this);
			H4 = new CellViewModel(null, this);

			A3 = new CellViewModel(null, this);
			B3 = new CellViewModel(null, this);
			C3 = new CellViewModel(null, this);
			D3 = new CellViewModel(null, this);
			E3 = new CellViewModel(null, this);
			F3 = new CellViewModel(null, this);
			G3 = new CellViewModel(null, this);
			H3 = new CellViewModel(null, this);

			A2 = new CellViewModel(null, this);
			B2 = new CellViewModel(null, this);
			C2 = new CellViewModel(null, this);
			D2 = new CellViewModel(null, this);
			E2 = new CellViewModel(null, this);
			F2 = new CellViewModel(null, this);
			G2 = new CellViewModel(null, this);
			H2 = new CellViewModel(null, this);

			A1 = new CellViewModel(null, this);
			B1 = new CellViewModel(null, this);
			C1 = new CellViewModel(null, this);
			D1 = new CellViewModel(null, this);
			E1 = new CellViewModel(null, this);
			F1 = new CellViewModel(null, this);
			G1 = new CellViewModel(null, this);
			H1 = new CellViewModel(null, this);

			await CreateLink();
		}

		private async Task CreateLink()
		{
			_createLink = new TaskFactory().StartNew(() =>
			{
				A8.CreateLink(null, null, B8, B7, A7, null, null, null);
				B8.CreateLink(null, null, C8, C7, B7, A7, A8, null);
				C8.CreateLink(null, null, D8, D7, C7, B7, B8, null);
				D8.CreateLink(null, null, E8, E7, D7, C7, C8, null);
				E8.CreateLink(null, null, F8, F7, E7, D7, D8, null);
				F8.CreateLink(null, null, G8, G7, F7, E7, E8, null);
				G8.CreateLink(null, null, H8, H7, G7, F7, F8, null);
				H8.CreateLink(null, null, null, null, H7, G7, G8, null);

				A7.CreateLink(A8, B8, B7, B6, A6, null, null, null);
				B7.CreateLink(B8, C8, C7, C6, B6, A6, A7, A8);
				C7.CreateLink(C8, D8, D7, D6, C6, B6, B7, B8);
				D7.CreateLink(D8, E8, E7, E6, D6, C6, C7, C8);
				E7.CreateLink(E8, F8, F7, F6, E6, D6, D7, D8);
				F7.CreateLink(F8, G8, G7, G6, F6, E6, E7, E8);
				G7.CreateLink(G8, H8, H7, H6, G6, F6, F7, F8);
				H7.CreateLink(H8, null, null, null, H6, G6, G7, G8);

				A6.CreateLink(A7, B7, B6, B5, A5, null, null, null);
				B6.CreateLink(B7, C7, C6, C5, B5, A5, A6, A7);
				C6.CreateLink(C7, D7, D6, D5, C5, B5, B6, B7);
				D6.CreateLink(D7, E7, E6, E5, D5, C5, C6, C7);
				E6.CreateLink(E7, F7, F6, F5, E5, D5, D6, D7);
				F6.CreateLink(F7, G7, G6, G5, F5, E5, E6, E7);
				G6.CreateLink(G7, H7, H6, H5, G5, F5, F6, F7);
				H6.CreateLink(H7, null, null, null, H5, G5, G6, G7);

				A5.CreateLink(A6, B6, B5, B4, A4, null, null, null);
				B5.CreateLink(B6, C6, C5, C4, B4, A4, A5, A6);
				C5.CreateLink(C6, D6, D5, D4, C4, B4, B5, B6);
				D5.CreateLink(D6, E6, E5, E4, D4, C4, C5, C6);
				E5.CreateLink(E6, F6, F5, F4, E4, D4, D5, D6);
				F5.CreateLink(F6, G6, G5, G4, F4, E4, E5, E6);
				G5.CreateLink(G6, H6, H5, H4, G4, F4, F5, F6);
				H5.CreateLink(H6, null, null, null, H4, G4, G5, G6);

				A4.CreateLink(A5, B5, B4, B3, A3, null, null, null);
				B4.CreateLink(B5, C5, C4, C3, B3, A3, A4, A5);
				C4.CreateLink(C5, D5, D4, D3, C3, B3, B4, B5);
				D4.CreateLink(D5, E5, E4, E3, D3, C3, C4, C5);
				E4.CreateLink(E5, F5, F4, F3, E3, D3, D4, D5);
				F4.CreateLink(F5, G5, G4, G3, F3, E3, E4, E5);
				G4.CreateLink(G5, H5, H4, H3, G3, F3, F4, F5);
				H4.CreateLink(H5, null, null, null, H3, G3, G4, G5);

				A3.CreateLink(A4, B4, B3, B2, A2, null, null, null);
				B3.CreateLink(B4, C4, C3, C2, B2, A2, A3, A4);
				C3.CreateLink(C4, D4, D3, D2, C2, B2, B3, B4);
				D3.CreateLink(D4, E4, E3, E2, D2, C2, C3, C4);
				E3.CreateLink(E4, F4, F3, F2, E2, D2, D3, D4);
				F3.CreateLink(F4, G4, G3, G2, F2, E2, E3, E4);
				G3.CreateLink(G4, H4, H3, H2, G2, F2, F3, F4);
				H3.CreateLink(H4, null, null, null, H2, G2, G3, G4);

				A2.CreateLink(A3, B3, B2, B1, A1, null, null, null);
				B2.CreateLink(B3, C3, C2, C1, B1, A1, A2, A3);
				C2.CreateLink(C3, D3, D2, D1, C1, B1, B2, B3);
				D2.CreateLink(D3, E3, E2, E1, D1, C1, C2, C3);
				E2.CreateLink(E3, F3, F2, F1, E1, D1, D2, D3);
				F2.CreateLink(F3, G3, G2, G1, F1, E1, E2, E3);
				G2.CreateLink(G3, H3, H2, H1, G1, F1, F2, F3);
				H2.CreateLink(H3, null, null, null, H1, G1, G2, G3);

				A1.CreateLink(A2, B2, B1, null, null, null, null, null);
				B1.CreateLink(B2, C2, C1, null, null, null, A1, A2);
				C1.CreateLink(C2, D2, D1, null, null, null, B1, B2);
				D1.CreateLink(D2, E2, E1, null, null, null, C1, C2);
				E1.CreateLink(E2, F2, F1, null, null, null, D1, D2);
				F1.CreateLink(F2, G2, G1, null, null, null, E1, E2);
				G1.CreateLink(G2, H2, H1, null, null, null, F1, F2);
				H1.CreateLink(H2, null, null, null, null, null, G1, G2);
			});

			A8.Name = @"A8";
			B8.Name = @"B8";
			C8.Name = @"C8";
			D8.Name = @"D8";
			E8.Name = @"E8";
			F8.Name = @"F8";
			G8.Name = @"G8";
			H8.Name = @"H8";
			A7.Name = @"A7";
			B7.Name = @"B7";
			C7.Name = @"C7";
			D7.Name = @"D7";
			E7.Name = @"E7";
			F7.Name = @"F7";
			G7.Name = @"G7";
			H7.Name = @"H7";
			A6.Name = @"A6";
			B6.Name = @"B6";
			C6.Name = @"C6";
			D6.Name = @"D6";
			E6.Name = @"E6";
			F6.Name = @"F6";
			G6.Name = @"G6";
			H6.Name = @"H6";
			A5.Name = @"A5";
			B5.Name = @"B5";
			C5.Name = @"C5";
			D5.Name = @"D5";
			E5.Name = @"E5";
			F5.Name = @"F5";
			G5.Name = @"G5";
			H5.Name = @"H5";
			A4.Name = @"A4";
			B4.Name = @"B4";
			C4.Name = @"C4";
			D4.Name = @"D4";
			E4.Name = @"E4";
			F4.Name = @"F4";
			G4.Name = @"G4";
			H4.Name = @"H4";
			A3.Name = @"A3";
			B3.Name = @"B3";
			C3.Name = @"C3";
			D3.Name = @"D3";
			E3.Name = @"E3";
			F3.Name = @"F3";
			G3.Name = @"G3";
			H3.Name = @"H3";
			A2.Name = @"A2";
			B2.Name = @"B2";
			C2.Name = @"C2";
			D2.Name = @"D2";
			E2.Name = @"E2";
			F2.Name = @"F2";
			G2.Name = @"G2";
			H2.Name = @"H2";
			A1.Name = @"A1";
			B1.Name = @"B1";
			C1.Name = @"C1";
			D1.Name = @"D1";
			E1.Name = @"E1";
			F1.Name = @"F1";
			G1.Name = @"G1";
			H1.Name = @"H1";

			await _createLink;

			AllCells.Add(A8);
			AllCells.Add(B8);
			AllCells.Add(C8);
			AllCells.Add(D8);
			AllCells.Add(E8);
			AllCells.Add(F8);
			AllCells.Add(G8);
			AllCells.Add(H8);
			AllCells.Add(A7);
			AllCells.Add(B7);
			AllCells.Add(C7);
			AllCells.Add(D7);
			AllCells.Add(E7);
			AllCells.Add(F7);
			AllCells.Add(G7);
			AllCells.Add(H7);
			AllCells.Add(A6);
			AllCells.Add(B6);
			AllCells.Add(C6);
			AllCells.Add(D6);
			AllCells.Add(E6);
			AllCells.Add(F6);
			AllCells.Add(G6);
			AllCells.Add(H6);
			AllCells.Add(A5);
			AllCells.Add(B5);
			AllCells.Add(C5);
			AllCells.Add(D5);
			AllCells.Add(E5);
			AllCells.Add(F5);
			AllCells.Add(G5);
			AllCells.Add(H5);
			AllCells.Add(A4);
			AllCells.Add(B4);
			AllCells.Add(C4);
			AllCells.Add(D4);
			AllCells.Add(E4);
			AllCells.Add(F4);
			AllCells.Add(G4);
			AllCells.Add(H4);
			AllCells.Add(A3);
			AllCells.Add(B3);
			AllCells.Add(C3);
			AllCells.Add(D3);
			AllCells.Add(E3);
			AllCells.Add(F3);
			AllCells.Add(G3);
			AllCells.Add(H3);
			AllCells.Add(A2);
			AllCells.Add(B2);
			AllCells.Add(C2);
			AllCells.Add(D2);
			AllCells.Add(E2);
			AllCells.Add(F2);
			AllCells.Add(G2);
			AllCells.Add(H2);
			AllCells.Add(A1);
			AllCells.Add(B1);
			AllCells.Add(C1);
			AllCells.Add(D1);
			AllCells.Add(E1);
			AllCells.Add(F1);
			AllCells.Add(G1);
			AllCells.Add(H1);
		}

		[NotifyPropertyChangedInvocator]
		private void OnPropertyChanged([CallerMemberName] string propertyName = null)
		{
			PropertyChanged?.Invoke(this, new PropertyChangedEventArgs(propertyName));
		}

		private void Dispose(bool disposing)
		{
			if (!disposing) return;

			A8 = null;
			B8 = null;
			C8 = null;
			D8 = null;
			E8 = null;
			F8 = null;
			G8 = null;
			H8 = null;

			A7 = null;
			B7 = null;
			C7 = null;
			D7 = null;
			E7 = null;
			F7 = null;
			G7 = null;
			H7 = null;

			A6 = null;
			B6 = null;
			C6 = null;
			D6 = null;
			E6 = null;
			F6 = null;
			G6 = null;
			H6 = null;

			A5 = null;
			B5 = null;
			C5 = null;
			D5 = null;
			E5 = null;
			F5 = null;
			G5 = null;
			H5 = null;

			A4 = null;
			B4 = null;
			C4 = null;
			D4 = null;
			E4 = null;
			F4 = null;
			G4 = null;
			H4 = null;

			A3 = null;
			B3 = null;
			C3 = null;
			D3 = null;
			E3 = null;
			F3 = null;
			G3 = null;
			H3 = null;

			A2 = null;
			B2 = null;
			C2 = null;
			D2 = null;
			E2 = null;
			F2 = null;
			G2 = null;
			H2 = null;

			A1 = null;
			B1 = null;
			C1 = null;
			D1 = null;
			E1 = null;
			F1 = null;
			G1 = null;
			H1 = null;

			_createLink = null;
			_graveYard = null;
		}
	}
}