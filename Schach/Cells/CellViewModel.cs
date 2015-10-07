using System.Collections.Generic;
using System.ComponentModel;
using System.Linq;
using System.Runtime.CompilerServices;
using System.Windows.Media;
using System.Windows.Media.Imaging;
using Chess.Annotations;
using Chess.ChessPieces;
using Chess.Properties;
using NUnit.Framework;

namespace Chess.Cells
{
	public class CellViewModel : INotifyPropertyChanged
	{
		private static readonly SolidColorBrush CanMoveColor = new SolidColorBrush(Color.FromArgb(205, 9, 140, 0));
		public static readonly SolidColorBrush IsCheckmateColor = new SolidColorBrush(Color.FromArgb(205, 255, 0, 0));
		private static readonly SolidColorBrush CanEatColor = new SolidColorBrush(Color.FromArgb(205, 255, 171, 0));
		private static readonly SolidColorBrush HighlightedColor = new SolidColorBrush(Color.FromArgb(205, 91, 100, 113));
		public static readonly SolidColorBrush NothingColor = new SolidColorBrush(Color.FromArgb(0, 0, 0, 0));

		public readonly Board Board;

		public Dictionary<Movement.Direction, CellViewModel> Movements { get; } = new Dictionary<Movement.Direction, CellViewModel>();

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

		public string Name { get; set; }

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
			Movements.Add(Movement.Direction.Top, top);
			Movements.Add(Movement.Direction.TopRight, topright);
			Movements.Add(Movement.Direction.Right, right);
			Movements.Add(Movement.Direction.BottomRight, bottomright);
			Movements.Add(Movement.Direction.Bottom, bottom);
			Movements.Add(Movement.Direction.BottomLeft, bottomleft);
			Movements.Add(Movement.Direction.Left, left);
			Movements.Add(Movement.Direction.TopLeft, topleft);
			Movements.Add(Movement.Direction.Final, null);
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
					Movements[movePath.GetStep()]?.ColorizeMove(movePath);
				}
				foreach (var eatPath in pawn.EatList.Select(p => p.Clone()))
				{
					Movements[eatPath.GetStep()]?.ColorizeEat(eatPath, CurrentChessPiece.IsWhite());
				}

				return;
			}

			foreach (var path in CurrentChessPiece.PathList.Select(p => p.Clone()))
			{
				if (CurrentChessPiece is Knight)
				{
					Movements[path.GetStep()]?.ColorizeJump(path, CurrentChessPiece.IsWhite());
				}
				else
				{
					Movements[path.GetStep()]?.ColorizeMoveEat(path, CurrentChessPiece.IsWhite());
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
				Movements[path.GetStep()]?.ColorizeJump(path, isWhite);
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
				Movements[path.GetNextStep()]?.ColorizeEat(path, isWhite);
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
				Movements[path.GetNextStep()]?.ColorizeMove(path);
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
			if (!endModel.CanMoveHere.Any(o => o.StartCell.Equals(startModel)) && !endModel.CanEatHere.Any(o => o.StartCell.Equals(startModel)))
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
		/// <returns>Ture, when a possible Path was found, either for walking or eating</returns>
		public void MarkPaths()
		{
			Assert.IsNotNull(CurrentChessPiece, "CurrentChessPiece  HAS TO EXIST");

			var pawn = CurrentChessPiece as Pawn;
			if (pawn != null)
			{
				foreach (var path in pawn.PathList.Select(p => p.Clone()))
				{
					path.StartCell = this;
					Movements[path.GetStep()]?.MarkMoveTo(path);
				}
				foreach (var path in pawn.EatList.Select(p => p.Clone()))
				{
					path.StartCell = this;
					Movements[path.GetStep()]?.MarkEatTo(path);
				}
			}
			else if (CurrentChessPiece is Knight)
			{
				foreach (var path in CurrentChessPiece.PathList.Select(p => p.Clone()))
				{
					path.StartCell = this;
					Movements[path.GetStep()]?.CheckJumpEatTo(path);
				}
			}
			else
			{
				var enumerable = CurrentChessPiece.PathList?.Select(p => p.Clone());
				if (enumerable == null)
				{
					return;
				}
					foreach (var path in enumerable)
					{
						path.StartCell = this;
						Movements[path.GetStep()]?.MarkMoveTo(path);
						Movements[path.GetStep()]?.MarkEatTo(path);
					}
			}
		}

		private void MarkMoveTo(Path.Path path)
		{
			if (CurrentChessPiece != null)
			{
				return;
			}

			CanMoveHere.Add(path);

			if (Movements[path.GetNextStep()] != null)
			{
				Movements[path.GetStep()].MarkMoveTo(path);
			}
		}

		private void MarkEatTo(Path.Path path)
		{
			if (CurrentChessPiece != null && CurrentChessPiece.IsWhite() != path.IsWhite)
			{
				CanEatHere.Add(path);
				return;
			}

			if (Movements[path.GetNextStep()] != null)
			{
				Movements[path.GetStep()].MarkEatTo(path);
			}
		}

		private void CheckJumpEatTo(Path.Path path)
		{
			if (path.GetNextStep() == Movement.Direction.Final)
			{
				CanMoveHere.Add(path);
				CanEatHere.Add(path);
				return;
			}

			if (Movements[path.GetStep()] != null)
			{
				Movements[path.GetStep()].CheckJumpEatTo(path);
			}
		}

		#endregion
	}
}