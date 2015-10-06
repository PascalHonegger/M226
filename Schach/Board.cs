using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Linq;
using System.Windows.Media;
using Chess.Cells;
using Chess.ChessPieces;

namespace Chess
{
	public sealed class Board
	{
		private ObservableCollection<IChessPiece> _graveYard;
		private CellViewModel _selectedCellViewModel;
		private bool _whiteTurn;

		public Board(bool hasDefaultValues = true)
		{
			AllCells = new Dictionary<string, CellViewModel>();

			if (hasDefaultValues)
			{
				CreateDefaultChessBoard();
			}
			else
			{
				CreateEmptyChessBoard();
			}

			History = new ObservableCollection<HistoryControl>();

			WhiteTurn = true;
		}

		public CellViewModel SelectedCellViewModel
		{
			set
			{
				ResetColors();

				// NextTurn, when the ViewModel moved to the newly selected ViewModel
				if (CellViewModel.MoveModel(_selectedCellViewModel, value))
				{
					NextTurn();
				}
				// Select ViewModel, if it's the players turn
				else if (value?.CurrentChessPiece != null && WhiteTurn == value.CurrentChessPiece.IsWhite())
				{
					_selectedCellViewModel = value;

					_selectedCellViewModel?.StartColorize();
				}
				else
				{
					_selectedCellViewModel = null;
				}
			}
		}

		// ReSharper disable once ConvertToAutoProperty
		private bool WhiteTurn
		{
			get { return _whiteTurn; }
			set
			{
				_whiteTurn = value;

				//TODO Track time for each Player

				//TODO Give GUI-Feedback, that the turn counted and the other Color has to play
			}
		}

		public ObservableCollection<IChessPiece> GraveYard
			=> _graveYard ?? (_graveYard = new ObservableCollection<IChessPiece>());

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

		public ObservableCollection<HistoryControl> History { get; }

		public Dictionary<string, CellViewModel> AllCells { get; }

		private void NextTurn()
		{
			WhiteTurn = !WhiteTurn;

			CalculatePossibleSteps();

			MarkCheck();

			if (CalculateCheckmate())
			{
				//TODO GameOver
			}
		}

		private void CalculatePossibleSteps()
		{
			foreach (var cell in AllCells.Select(kvp => kvp.Value).Where(cell => cell.CurrentChessPiece != null))
			{
				var dummyCellViewModel = new CellViewModel(new Queen(true), new Board(false));
                cell.FindPathTo(dummyCellViewModel, true);
			}
		}

		private void MarkCheck()
		{
			foreach (var kingCell in AllCells
				.Where(cell => cell.Value.CurrentChessPiece is King)
				.Select(kvp => kvp.Value)
				.Where(kingCell => kingCell.CanEatHere
				.Any(path => path.IsWhite != kingCell.CurrentChessPiece.IsWhite())))
			{
				kingCell.Bgc = CellViewModel.IsCheckmateColor;
			}
		}

		private bool CalculateCheckmate()
		{


			return false;
		}

		private void ResetColors()
		{
			foreach (var kvp in AllCells.Where(kvp => !Equals(kvp.Value.Bgc, CellViewModel.IsCheckmateColor)))
			{
				kvp.Value.Bgc = CellViewModel.NothingColor;
			}
		}

		private void CreateDefaultChessBoard()
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

			CreateLink();
		}

		private void CreateEmptyChessBoard()
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

			CreateLink();
		}

		private void CreateLink()
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

			for (var number = 1; number <= 8; number++)
			{
				for (int character = 'A'; character <= 'H'; character++)
				{
					var propertyName = (char)character + number.ToString();
					var property = (CellViewModel)GetType().GetProperty(propertyName).GetValue(this);
					AllCells.Add(propertyName, property);
				}
			}
		}

		public void AddToGraveYard(CellViewModel cellViewModel)
		{
			if (cellViewModel?.CurrentChessPiece == null)
			{
				return;
			}
			if (cellViewModel.CurrentChessPiece.IsWhite())
			{
				GraveYard.Add(cellViewModel.CurrentChessPiece);
			}
			else
			{
				GraveYard.Add(cellViewModel.CurrentChessPiece);
			}
		}

		public void AddToHistory(CellViewModel startModel, CellViewModel endModel)
		{
			if (startModel == null || endModel == null)
			{
				return;
			}

			var from = new CellViewModel(startModel.CurrentChessPiece, startModel.Board);

			var to = new CellViewModel(endModel.CurrentChessPiece, endModel.Board);

			var historyControl = new HistoryControl
			{
				From = {DataContext = from},
				To = {DataContext = to},
				FromText = {Text = GetCellName(startModel)},
				ToText = {Text = GetCellName(endModel)}
			};

			History.Add(historyControl);
		}

		private string GetCellName(CellViewModel nameNeeded)
		{
			return AllCells.FirstOrDefault(kp => kp.Value.Equals(nameNeeded)).Key;
		}
	}
}