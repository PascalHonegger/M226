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
		private static readonly SolidColorBrush CanMoveHereColor = Brushes.Green;
		private static readonly SolidColorBrush IsCheckmateColor = Brushes.Red;
		private static readonly SolidColorBrush CanEatColor = Brushes.Orange;
		private static readonly SolidColorBrush HighlightedColor = Brushes.Gray;

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

			foreach (var path in CurrentChessPiece.PathList)
			{
				if (CurrentChessPiece is Pawn)
				{
					_movements[path.GetStep()]?.ColorizeMove(path);
					_movements[path.GetStep()]?.ColorizeEat(path);
				}
				else if (CurrentChessPiece is Knight)
				{
					_movements[path.GetStep()]?.ColorizeJump(CurrentChessPiece.IsWhite());
				}
				else
				{
					_movements[path.GetStep()]?.ColorizeMoveEat(path);
				}
			}
		}

		private void ColorizeJump(bool isWhite)
		{
			if (CurrentChessPiece == null)
			{
				Bgc = CanMoveHereColor;
			}
			else if (isWhite && CurrentChessPiece.IsBlack())
			{
				Bgc = CanEatColor;
			}
		}


		private void ColorizeMoveEat(Path.Path path)
		{
			ColorizeMove(path);
			ColorizeEat(path);
		}

		private void ColorizeEat(Path.Path path)
		{
			if (CurrentChessPiece != null)
			{
				Bgc = CanEatColor;
				return;
			}
			if (path.GetStep() != Movement.Direction.Final)
			{
				_movements[path.GetNextStep()]?.ColorizeEat(path);
			}
		}

		private void ColorizeMove(Path.Path path)
		{
			if (CurrentChessPiece != null)
			{
				return;
			}
			Bgc = CanMoveHereColor;
			if (path.GetStep() != Movement.Direction.Final)
			{
				_movements[path.GetNextStep()]?.ColorizeMove(path);
			}
		}

		#endregion

		#region Movement

		public static bool MoveModel(CellViewModel startModel, CellViewModel endModel)
		{
			if (startModel.CurrentChessPiece == null)
			{
				return false;
			}
			if (endModel.CurrentChessPiece != null &&
			    ((startModel.CurrentChessPiece.IsWhite() && endModel.CurrentChessPiece.IsWhite()) ||
			     (startModel.CurrentChessPiece.IsBlack() && endModel.CurrentChessPiece.IsBlack())))
			{
				return false;
			}
			if (!startModel.FindPathTo(endModel))
			{
				return false;
			}

			endModel.MoveToGraveyard();
			endModel.CurrentChessPiece = startModel.CurrentChessPiece;
			endModel.CurrentChessPiece.DidMove = true;
			startModel.CurrentChessPiece = null;

			return true;
		}

		private bool FindPathTo(CellViewModel endModel)
		{
			var pawn = CurrentChessPiece as Pawn;
			if (pawn != null)
			{
				return pawn.PathList.Any(path =>
				{
					var canMoveTo = _movements[path.GetStep()]?.MoveTo(path, endModel);
					return canMoveTo != null && (bool) canMoveTo;
				}) || pawn.EatList.Any(path =>
				{
					var canEatTo = _movements[path.GetStep()]?.EatTo(path, endModel);
					return canEatTo != null && (bool) canEatTo;
				});
			}
			if (CurrentChessPiece is Knight)
			{
				return CurrentChessPiece.PathList.Any(path =>
				{
					var canJumpTo = _movements[path.GetStep()]?.JumpEatTo(path, endModel);
					return canJumpTo != null && (bool) canJumpTo;
				});
			}

			return CurrentChessPiece != null && CurrentChessPiece.PathList.Any(path =>
			{
				var canMoveOrEat = _movements[path.GetStep()]?.MoveEatTo(path, endModel);
				return canMoveOrEat != null && (bool) canMoveOrEat;
			});
		}

		private bool MoveTo(Path.Path path, CellViewModel endModel)
		{
			if (CurrentChessPiece != null)
			{
				return false;
			}
			if (Equals(this, endModel))
			{
				return true;
			}
			return _movements[path.GetNextStep()] != null && _movements[path.GetStep()].MoveTo(path, endModel);
		}

		private bool EatTo(Path.Path path, CellViewModel endModel)
		{
			if (Equals(this, endModel) && CurrentChessPiece != null)
			{
				return true;
			}
			if (CurrentChessPiece != null)
			{
				return false;
			}
			return _movements[path.GetNextStep()] != null && _movements[path.GetStep()].EatTo(path, endModel);
		}

		private bool MoveEatTo(Path.Path path, CellViewModel endModel)
		{
			if (Equals(this, endModel))
			{
				return true;
			}
			if (CurrentChessPiece != null)
			{
				return false;
			}
			return _movements[path.GetNextStep()] != null && _movements[path.GetStep()].MoveEatTo(path, endModel);
		}

		private bool JumpEatTo(Path.Path path, CellViewModel endModel)
		{
			if (Equals(this, endModel))
			{
				return true;
			}
			return _movements[path.GetNextStep()] != null && _movements[path.GetStep()].JumpEatTo(path, endModel);
		}

		#endregion
	}
}