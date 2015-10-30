using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Linq;
using System.Runtime.CompilerServices;
using System.Threading.Tasks;
using System.Windows.Media;
using System.Windows.Media.Imaging;
using Chess.Annotations;
using Chess.ChessPieces;

namespace Chess.Cells
{
	/// <summary>
	/// The Logic behind a cell. Can calculate where to go, change color and move the own ChessPiece to a destination
	/// </summary>
	public class CellViewModel : INotifyPropertyChanged, ICloneable
	{
		/// <summary>
		/// The cells this cell can move to have this color
		/// </summary>
		private static readonly SolidColorBrush CanMoveToColor = new SolidColorBrush(Color.FromArgb(205, 9, 140, 0));
		/// <summary>
		/// A CheckMated King has this color
		/// </summary>
		public static readonly SolidColorBrush IsCheckmateColor = new SolidColorBrush(Color.FromArgb(250, 255, 0, 0));
		/// <summary>
		/// The cells this cell can eat have this color
		/// </summary>
		private static readonly SolidColorBrush CanEatToColor = new SolidColorBrush(Color.FromArgb(205, 255, 101, 0));
		/// <summary>
		/// The cells that is selected has this color
		/// </summary>
		private static readonly SolidColorBrush HighlightedColor = new SolidColorBrush(Color.FromArgb(205, 91, 100, 113));
		/// <summary>
		/// The cells this cell can move to have this color
		/// </summary>
		public static readonly SolidColorBrush CanMoveHereColor = new SolidColorBrush(Color.FromArgb(205, 192, 255, 0));
		/// <summary>
		/// The cell that can eat this cell has this color
		/// </summary>
		public static readonly SolidColorBrush CanEatHereColor = new SolidColorBrush(Color.FromArgb(205, 255, 153, 0));
		/// <summary>
		/// Default Color every cell has if none of the other colors are applied.
		/// </summary>
		public static readonly SolidColorBrush NothingColor = new SolidColorBrush(Color.FromArgb(0, 0, 0, 0));

		/// <summary>
		/// The board this CellViewModel belongs to
		/// </summary>
		public IBoard Board;

		private SolidColorBrush _bgc;
		private IChessPiece _currentChessPiece;
		private BitmapSource _image;

		/// <summary>
		/// The Constructor which sets the color to nothing and initializes the CanEatHere and CanMoveHere lists
		/// </summary>
		/// <param name="currentChessChessPiece">The Chesspiece, which will be on this CellViewModel</param>
		/// <param name="board">The Board this CellViewModel is on</param>
		public CellViewModel(IChessPiece currentChessChessPiece, [NotNull]IBoard board)
		{
			CurrentChessPiece = currentChessChessPiece;
			Board = board;
			Bgc = NothingColor;
			CanEatHere = new List<Path.Path>();
			CanMoveHere = new List<Path.Path>();
		}

		public Dictionary<Movement.Direction, CellViewModel> Movements { get; private set; } =
			new Dictionary<Movement.Direction, CellViewModel>();

		/// <summary>
		/// The Name of this Cell, like 'A8' or 'D4'
		/// </summary>
		public string Name { get; set; }

		/// <summary>
		/// The ChessPiece that is on this Cell.
		/// </summary>
		public IChessPiece CurrentChessPiece
		{
			get { return _currentChessPiece; }
			set
			{
				_currentChessPiece = value;

				Image = _currentChessPiece?.Texture;
			}
		}

		public BitmapSource Image
		{
			get { return _image; }
			private set
			{
				_image = value;
				OnPropertyChanged(nameof(Image));
			}
		}

		public List<Path.Path> CanEatHere { get; }
		public List<Path.Path> CanMoveHere { get; }

		public SolidColorBrush Bgc
		{
			get { return _bgc; }
			set
			{
				if (Equals(_bgc, value))
				{
					return;
				}
				_bgc = value;
				OnPropertyChanged(nameof(Bgc));
			}
		}

		public object Clone()
		{
			var newCellViewModel = new CellViewModel(CurrentChessPiece, Board);

			newCellViewModel.CanEatHere.AddRange(CanEatHere.Select(path => path.ClonePath()));
			newCellViewModel.CanMoveHere.AddRange(CanEatHere.Select(path => path.ClonePath()));

			newCellViewModel.Bgc = Bgc;
			newCellViewModel.Name = Name;

			newCellViewModel.CurrentChessPiece = CurrentChessPiece?.CloneChessPiece();

			return newCellViewModel;
		}

		public event PropertyChangedEventHandler PropertyChanged;

		private void MoveToGraveyard()
		{
			Board.AddToGraveYard(this);
			CurrentChessPiece = null;
		}

		public override bool Equals(object obj)
		{
			var otherCell = obj as CellViewModel;
			if (otherCell == null)
			{
				return false;
			}

			if (!string.Equals(otherCell.Name, Name))
			{
				return false;
			}

			return true;
		}

		public override int GetHashCode()
		{
			return Name.GetHashCode();
		}

		public void CreateLink(Dictionary<Movement.Direction, CellViewModel> movements)
		{
			Movements = movements;
		}

		[NotifyPropertyChangedInvocator]
		private void OnPropertyChanged([CallerMemberName] string propertyName = null)
		{
			PropertyChanged?.Invoke(this, new PropertyChangedEventArgs(propertyName));
		}

		public void StartColorize()
		{
			var pawn = CurrentChessPiece as Pawn;
			if (CurrentChessPiece?.PathList == null || !CurrentChessPiece.PathList.Any() || pawn != null && !pawn.EatList.Any())
			{
				return;
			}

			Bgc = HighlightedColor;

			foreach (var cell in Board.AllCells)
			{
				if (cell.CanMoveHere.Select(p => p.StartCell).Contains(this))
				{
					cell.Bgc = CanMoveToColor;
				}
				else if (cell.CanEatHere.Select(p => p.StartCell).Contains(this))
				{
					cell.Bgc = CanEatToColor;
				}
			}
		}

		/// <summary>
		/// Casts the Clone() method to a CellViewModel.
		/// </summary>
		/// <returns></returns>
		public CellViewModel CloneCellViewModel()
		{
			return Clone() as CellViewModel;
		}

		#region Movement

		/// <summary>
		/// 
		/// </summary>
		/// <param name="startModel"></param>
		/// <param name="endModel"></param>
		/// <returns></returns>
		public static bool MoveModel(CellViewModel startModel, CellViewModel endModel)
		{
			// No Chesspiece to move or one of the ViewModel's is empty
			if (startModel == null || endModel == null || startModel.CurrentChessPiece == null)
			{
				return false;
			}
			// Same color
			if (endModel.CurrentChessPiece != null &&
			    (startModel.CurrentChessPiece.IsWhite() == endModel.CurrentChessPiece.IsWhite()))
			{
				return false;
			}

			// No Path to the destination / endModel
			if (!endModel.CanMoveHere.Any(o => Equals(o.StartCell, startModel)) &&
			    !endModel.CanEatHere.Any(o => Equals(o.StartCell, startModel)))
			{
				return false;
			}

			// Startmodel has a ChessPece
			// Startmodel and Endmodel aren't the same color
			// Startmodel was able to find a path to the Endmodel
			// Start and Endmodel are not the same
			endModel.MoveToGraveyard();
			endModel.CurrentChessPiece = startModel.CurrentChessPiece;
			endModel.CurrentChessPiece.DidMove = true;
			startModel.CurrentChessPiece = null;

			return true;
		}

		/// <summary>
		///     Fills in the CanMoveHere and CanEatHere Lists, which are used for the Logic of this Game
		/// </summary>
		public async Task MarkPaths(bool ignoreValidateMovement)
		{
			var pawn = CurrentChessPiece as Pawn;
			if (pawn != null)
			{
				foreach (var path in pawn.PathList.Select(p => p.ClonePath()))
				{
					path.StartCell = ignoreValidateMovement ? CloneCellViewModel() : this;
					var markMoveTo = Movements[path.GetStep()]?.MarkMoveTo(path, ignoreValidateMovement);
					if (markMoveTo != null)
					{
						await markMoveTo;
					}
				}
				foreach (var path in pawn.EatList.Select(p => p.ClonePath()))
				{
					path.StartCell = ignoreValidateMovement ? CloneCellViewModel() : this;
					var markEatTo = Movements[path.GetStep()]?.MarkEatTo(path, ignoreValidateMovement);
					if (markEatTo != null)
					{
						await markEatTo;
					}
				}
			}
			else if (CurrentChessPiece is Knight)
			{
				foreach (var path in CurrentChessPiece.PathList.Select(p => p.ClonePath()))
				{
					path.StartCell = this;
					var checkJumpEatTo = Movements[path.GetStep()]?.CheckJumpEatTo(path, ignoreValidateMovement);
					if (checkJumpEatTo != null)
						await checkJumpEatTo;
				}
			}
			else
			{
				var enumerable = CurrentChessPiece.PathList?.Select(p => p.ClonePath());
				if (enumerable == null)
				{
					return;
				}
				foreach (var path in enumerable)
				{
					path.StartCell = this;
					var markMoveTo = Movements[path.GetStep()]?.MarkMoveTo(path, ignoreValidateMovement);
					if (markMoveTo != null)
						await markMoveTo;
					var markEatTo = Movements[path.GetStep()]?.MarkEatTo(path, ignoreValidateMovement);
					if (markEatTo != null)
						await markEatTo;
				}
			}
		}

		private async Task MarkMoveTo(Path.Path path, bool ignoreValidateMovement)
		{
			if (CurrentChessPiece != null)
			{
				return;
			}

			if (ignoreValidateMovement || await Board.ValidateMovement(path.StartCell, this))
			{
				CanMoveHere.Add(path);
			}

			if (Movements[path.GetNextStep()] != null)
			{
				await Movements[path.GetStep()].MarkMoveTo(path, ignoreValidateMovement);
			}
		}

		private async Task MarkEatTo(Path.Path path, bool ignoreValidateMovement)
		{
			if (CurrentChessPiece != null)
			{
				if (CurrentChessPiece.IsWhite() != path.StartCell.CurrentChessPiece?.IsWhite() &&
				    (ignoreValidateMovement || await Board.ValidateMovement(path.StartCell, this)))
				{
					CanEatHere.Add(path);
				}
				return;
			}

			if (Movements[path.GetNextStep()] != null)
			{
				await Movements[path.GetStep()].MarkEatTo(path, ignoreValidateMovement);
			}
		}

		private async Task CheckJumpEatTo(Path.Path path, bool ignoreValidateMovement)
		{
			if (path.GetNextStep() == Movement.Direction.Final)
			{
				if (CurrentChessPiece == null && (ignoreValidateMovement || await Board.ValidateMovement(path.StartCell, this)))
				{
					CanMoveHere.Add(path);
				}
				else if (CurrentChessPiece?.IsWhite() != path.StartCell.CurrentChessPiece?.IsWhite() 
					&& (ignoreValidateMovement 
					|| await Board.ValidateMovement(path.StartCell, this)))
				{
					CanEatHere.Add(path);
				}
				return;
			}

			if (Movements[path.GetStep()] != null)
			{
				await Movements[path.GetStep()].CheckJumpEatTo(path, ignoreValidateMovement);
			}
		}

		#endregion
	}
}