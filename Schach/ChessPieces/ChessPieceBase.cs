using System;
using System.Collections.Generic;
using System.Linq;
using System.Windows.Media.Imaging;
using Chess.Cells;
using Chess.Path;

namespace Chess.ChessPieces
{
	public abstract class ChessPieceBase : IChessPiece
	{
		private readonly bool _colorIsWhite;

		private bool _didMove;

		/// <summary>
		/// Forward to PathFactory.Instance. Makes the code more readable!
		/// </summary>
		public static PathFactory PathFactory => PathFactory.Instance;

		protected ChessPieceBase()
		{
			
		}

		protected ChessPieceBase(bool isWhite)
		{
			_colorIsWhite = isWhite;
		}

		public bool DidMove
		{
			get { return _didMove; }
			set
			{
				if (value == DidMove) return;
				if (this is Pawn && value)
				{
					PathList.RemoveAll(
						path =>
							path.Equals(PathFactory.AddToPath(Movement.Direction.Top).AddToPath(Movement.Direction.Top).Create()));
					PathList.RemoveAll(
						path =>
							path.Equals(
								PathFactory.AddToPath(Movement.Direction.Bottom).AddToPath(Movement.Direction.Bottom).Create()));
				}
				_didMove = value;
			}
		}

		public virtual bool IsWhite()
		{
			return _colorIsWhite;
		}

		public IChessPiece CloneChessPiece()
		{
			return Clone() as IChessPiece;
		}

		public virtual bool IsBlack()
		{
			return !_colorIsWhite;
		}

		public BitmapSource Texture { get; set; }

		public virtual List<Path.Path> PathList { get; protected set; } = new List<Path.Path>();
		/// <summary>
		/// Only used in Pawn!
		/// </summary>
		public List<Path.Path> EatList { get; private set; } = new List<Path.Path>();

		public object Clone()
		{
			IChessPiece clonedChessPiece;

			if (this is Pawn)
			{
				clonedChessPiece = new Pawn(IsWhite(), false)
				{
					DidMove = DidMove,
					PathList = PathList.Select(path => path.ClonePath()).ToList(),
					EatList = EatList
				};
			}
			else if (this is King)
			{
				clonedChessPiece = new King(IsWhite(), false)
				{
					DidMove = DidMove,
					PathList = PathList.Select(path => path.ClonePath()).ToList(),
				};
			}
			else if (this is Queen)
			{
				clonedChessPiece = new Queen(IsWhite(), false)
				{
					DidMove = DidMove,
					PathList = PathList.Select(path => path.ClonePath()).ToList(),
				};
			}
			else if (this is Rook)
			{
				clonedChessPiece = new Rook(IsWhite(), false)
				{
					DidMove = DidMove,
					PathList = PathList.Select(path => path.ClonePath()).ToList(),
				};
			}
			else if (this is Knight)
			{
				clonedChessPiece = new Knight(IsWhite(), false)
				{
					DidMove = DidMove,
					PathList = PathList.Select(path => path.ClonePath()).ToList(),
				};
			}
			else if (this is Bishop)
			{
				clonedChessPiece = new Bishop(IsWhite(), false)
				{
					DidMove = DidMove,
					PathList = PathList.Select(path => path.ClonePath()).ToList(),
				};
			}
			else
			{
				throw new NotImplementedException();
			}

			return clonedChessPiece;
		}
	}
}