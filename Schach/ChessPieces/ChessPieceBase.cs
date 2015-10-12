using System;
using System.Collections.Generic;
using System.Reflection;
using System.Runtime.CompilerServices;
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
					PathList.RemoveAll(
						path =>
							path.Equals(new PathFactory().AddToPath(Movement.Direction.Top).AddToPath(Movement.Direction.Top).Create(true)));
					PathList.RemoveAll(
						path =>
							path.Equals(new PathFactory().AddToPath(Movement.Direction.Bottom).AddToPath(Movement.Direction.Bottom).Create(false)));
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
		public object Clone()
		{
			if (this is Pawn)
			{
				return new Pawn(IsWhite());
			}
			if (this is King)
			{
				return new King(IsWhite());
			}

			return null;
		}
	}
}