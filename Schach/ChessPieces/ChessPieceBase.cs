using System.Collections.Generic;
using System.Collections.Specialized;
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

		protected ChessPieceBase()
		{
			PathList = new List<Path.Path>();
		}

		protected ChessPieceBase(bool isWhite)
		{
			_colorIsWhite = isWhite;
			PathList = new List<Path.Path>();
		}

		public bool DidMove
		{
			get { return _didMove; }
			set
			{
				if (value == DidMove) return;
				if (this is Pawn && value)
				{
					PathList.RemoveAll(path => path.Equals(new PathFactory().AddToPath(Movement.Direction.Top).AddToPath(Movement.Direction.Top).Create()));
					PathList.RemoveAll(path => path.Equals(new PathFactory().AddToPath(Movement.Direction.Bottom).AddToPath(Movement.Direction.Bottom).Create()));
				}
				_didMove = value;
			}
		}

		public virtual bool IsWhite()
		{
			return _colorIsWhite;
		}

		public virtual bool IsBlack()
		{
			return !_colorIsWhite;
		}

		public BitmapSource Texture { get; set; }

		public virtual List<Path.Path> PathList { get; }

		public override bool Equals(object obj)
		{
			var other = obj as IChessPiece;

			if (other == null) return false;

			if (IsWhite() != other.IsWhite())
			{
				return false;
			}
			if (DidMove != other.DidMove)
			{
				return false;
			}
			if (!PathList.Equals(other.PathList))
			{
				return false;
			}
			if (GetType().Name != other.GetType().Name)
			{
				return false;
			}

			return true;
		}

		public override int GetHashCode()
		{
			unchecked
			{
				var hashCode = _colorIsWhite.GetHashCode();
				hashCode = (hashCode*397) ^ _didMove.GetHashCode();
				hashCode = (hashCode*397) ^ (Texture?.GetHashCode() ?? 0);
				hashCode = (hashCode*397) ^ (PathList?.GetHashCode() ?? 0);
				return hashCode;
			}
		}
	}
}