using System.Collections.Generic;
using System.Windows.Media.Imaging;

namespace Chess.ChessPieces
{
	public abstract class ChessPieceBase : IChessPiece
	{
		private readonly bool _colorIsWhite;

		private bool _didMove;

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
					PathList.RemoveAt(0);
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
	}
}