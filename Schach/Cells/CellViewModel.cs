using System.Collections.Generic;
using System.ComponentModel;
using System.Linq;
using System.Runtime.CompilerServices;
using System.Windows.Media;
using System.Windows.Media.Imaging;
using Chess.Annotations;
using Chess.ChessPieces;
using Chess.Properties;

namespace Chess.Cells
{
	public class CellViewModel : INotifyPropertyChanged
	{
		private static readonly SolidColorBrush CanMoveColor = new SolidColorBrush(Color.FromArgb(205, 9, 140, 0));
		public static readonly SolidColorBrush IsCheckmateColor = new SolidColorBrush(Color.FromArgb(205, 255, 0, 0));
		private static readonly SolidColorBrush CanEatColor = new SolidColorBrush(Color.FromArgb(205, 255, 171, 0));
		private static readonly SolidColorBrush HighlightedColor = new SolidColorBrush(Color.FromArgb(205, 91, 100, 113));
		public static readonly SolidColorBrush NothingColor = new SolidColorBrush(Color.FromArgb(0, 0, 0, 0));

		private readonly Dictionary<Movement.Direction, CellViewModel> _movements =
			new Dictionary<Movement.Direction, CellViewModel>();

		public readonly Board Board;

		private SolidColorBrush _bgc;
		private IChessPiece _currentChessPiece;
		private BitmapSource _image;

		public CellViewModel(IChessPiece currentChessChessPiece, Board board)
		{
			CurrentChessPiece = currentChessChessPiece;
			Board = board;
			Bgc = NothingColor;
			CanEatHere = new List<Path.Path>();
			CanMoveHere = new List<Path.Path>();
		}

		public IChessPiece CurrentChessPiece
		{
			get { return _currentChessPiece; }
			set
			{
				_currentChessPiece = value;

				Image = _currentChessPiece?.Texture ?? Resources.blank.ToBitmapSource();
			}
		}

		public BitmapSource Image
		{
			get { return _image; }
			private set
			{
				if (Equals(_image, value) || value == null) return;
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

		public event PropertyChangedEventHandler PropertyChanged;

		private void MoveToGraveyard()
		{
			Board.AddToGraveYard(this);
			CurrentChessPiece = null;
		}


		public void CreateLink(CellViewModel top, CellViewModel topright, CellViewModel right, CellViewModel bottomright,
			CellViewModel bottom, CellViewModel bottomleft, CellViewModel left, CellViewModel topleft)
		{
			_movements.Add(Movement.Direction.Top, top);
			_movements.Add(Movement.Direction.TopRight, topright);
			_movements.Add(Movement.Direction.Right, right);
			_movements.Add(Movement.Direction.BottomRight, bottomright);
			_movements.Add(Movement.Direction.Bottom, bottom);
			_movements.Add(Movement.Direction.BottomLeft, bottomleft);
			_movements.Add(Movement.Direction.Left, left);
			_movements.Add(Movement.Direction.TopLeft, topleft);
			_movements.Add(Movement.Direction.Final, null);
		}

		[NotifyPropertyChangedInvocator]
		private void OnPropertyChanged([CallerMemberName] string propertyName = null)
		{
			PropertyChanged?.Invoke(this, new PropertyChangedEventArgs(propertyName));
		}

		#region Coloring

		public void StartColorize()
		{
			if (CurrentChessPiece?.PathList == null || !CurrentChessPiece.PathList.Any())
			{
				return;
			}

			Bgc = HighlightedColor;

			var pawn = CurrentChessPiece as Pawn;
			if (pawn != null)
			{
				foreach (var movePath in pawn.PathList.Select(p => p.Clone()))
				{
					_movements[movePath.GetStep()]?.ColorizeMove(movePath);
				}
				foreach (var eatPath in pawn.EatList.Select(p => p.Clone()))
				{
					_movements[eatPath.GetStep()]?.ColorizeEat(eatPath, CurrentChessPiece.IsWhite());
				}

				return;
			}

			foreach (var path in CurrentChessPiece.PathList.Select(p => p.Clone()))
			{
				if (CurrentChessPiece is Knight)
				{
					_movements[path.GetStep()]?.ColorizeJump(path, CurrentChessPiece.IsWhite());
				}
				else
				{
					_movements[path.GetStep()]?.ColorizeMoveEat(path, CurrentChessPiece.IsWhite());
				}
			}
		}

		private void ColorizeJump(Path.Path path, bool isWhite)
		{
			if (path.GetNextStep() == Movement.Direction.Final)
			{
				if (CurrentChessPiece == null)
				{
					Bgc = CanMoveColor;
				}
				else if (isWhite != CurrentChessPiece.IsWhite())
				{
					Bgc = CanEatColor;
				}
			}
			else
			{
				_movements[path.GetStep()]?.ColorizeJump(path, isWhite);
			}
		}


		private void ColorizeMoveEat(Path.Path path, bool isWhite)
		{
			ColorizeMove(path);
			ColorizeEat(path, isWhite);
		}

		private void ColorizeEat(Path.Path path, bool isWhite)
		{
			if (CurrentChessPiece != null)
			{
				if (isWhite != CurrentChessPiece.IsWhite())
				{
					Bgc = CanEatColor;
				}
				return;
			}
			if (path.GetStep() != Movement.Direction.Final)
			{
				_movements[path.GetNextStep()]?.ColorizeEat(path, isWhite);
			}
		}

		private void ColorizeMove(Path.Path path)
		{
			if (CurrentChessPiece != null)
			{
				return;
			}
			Bgc = CanMoveColor;
			if (path.GetStep() != Movement.Direction.Final)
			{
				_movements[path.GetNextStep()]?.ColorizeMove(path);
			}
		}

		#endregion

		#region Movement

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
			if (!startModel.FindPathTo(endModel))
			{
				return false;
			}

			// Startmodel has a ChessPece
			// Startmodel and Endmodel aren't the same color
			// Startmodel was able to find a path to the Endmodel
			// Start and Endmodel are not the same

			startModel.Board.AddToHistory(startModel, endModel);

			endModel.MoveToGraveyard();
			endModel.CurrentChessPiece = startModel.CurrentChessPiece;
			endModel.CurrentChessPiece.DidMove = true;
			startModel.CurrentChessPiece = null;

			return true;
		}

		/// <summary>
		///     Checks for a valid Path. Does check that both ChessPieces have to be a different color to be eaten.
		/// </summary>
		/// <param name="endModel">The Model to move to</param>
		/// <param name="markCanMove">If true, the two Lists MoveHere and EatHere, which are used for Check and Checkmate</param>
		/// <returns>Ture, when a possible Path was found, either for walking or eating</returns>
		public bool FindPathTo(CellViewModel endModel, bool markCanMove = false)
		{
			var pawn = CurrentChessPiece as Pawn;
			if (pawn != null)
			{
				return pawn.PathList.Select(p => p.Clone()).Any(path =>
				{
					var canMoveTo = _movements[path.GetStep()]?.ChecktMoveTo(path, endModel, markCanMove);
					return canMoveTo != null && (bool) canMoveTo;
				})
				       || pawn.EatList.Select(p => p.Clone()).Any(path =>
				       {
					       var canEatTo = _movements[path.GetStep()]?.CheckEatTo(path, endModel, markCanMove);
					       return canEatTo != null && (bool) canEatTo;
				       });
			}

			if (CurrentChessPiece is Knight)
			{
				return CurrentChessPiece.PathList.Select(p => p.Clone()).Any(path =>
				{
					var canJumpTo = _movements[path.GetStep()]?.CheckJumpEatTo(path, endModel, markCanMove);
					return canJumpTo != null && (bool) canJumpTo;
				});
			}

			return CurrentChessPiece != null && CurrentChessPiece.PathList.Select(p => p.Clone()).Any(path =>
			{
				var canMoveTo = _movements[path.GetStep()]?.ChecktMoveTo(path, endModel, markCanMove);
				var canEatTo = _movements[path.GetStep()]?.CheckEatTo(path, endModel, markCanMove);
				return (canEatTo != null && (bool)canEatTo) || (canMoveTo != null && (bool)canMoveTo);
			});
		}

		private bool ChecktMoveTo(Path.Path path, CellViewModel endModel, bool markCanMove)
		{
			if (CurrentChessPiece != null)
			{
				CanMoveHere.Add(path);
				return false;
			}
			if (Equals(this, endModel))
			{
				return true;
			}

			return _movements[path.GetNextStep()] != null && _movements[path.GetStep()].ChecktMoveTo(path, endModel, markCanMove);
		}

		private bool CheckEatTo(Path.Path path, CellViewModel endModel, bool markCanMove)
		{
			if (Equals(this, endModel) && CurrentChessPiece != null)
			{
				return true;
			}
			if (CurrentChessPiece != null)
			{
				return false;
			}

			return _movements[path.GetNextStep()] != null && _movements[path.GetStep()].CheckEatTo(path, endModel, markCanMove);
		}

		private bool CheckJumpEatTo(Path.Path path, CellViewModel endModel, bool markCanMove)
		{
			if (path.GetNextStep() == Movement.Direction.Final && Equals(this, endModel))
			{
				return true;
			}

			return _movements[path.GetStep()] != null && _movements[path.GetStep()].CheckJumpEatTo(path, endModel, markCanMove);
		}

		#endregion
	}
}