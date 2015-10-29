using Chess.Cells;
using Chess.Properties;

namespace Chess.ChessPieces
{
	public sealed class Bishop : ChessPieceBase
	{
		public Bishop(bool isWhite, bool hasTextures = true) : base(isWhite)
		{
			if (hasTextures)
			{
				Texture = isWhite
					? Resources.WhiteBishop.ToBitmapSource()
					: Resources.BlackBishop.ToBitmapSource();
			}

			var path = PathFactory.AddToPath(Movement.Direction.TopLeft).SetIsRecursive(true).Create(isWhite);
			PathList.Add(path);

			path = PathFactory.AddToPath(Movement.Direction.TopRight).SetIsRecursive(true).Create(isWhite);
			PathList.Add(path);

			path = PathFactory.AddToPath(Movement.Direction.BottomLeft).SetIsRecursive(true).Create(isWhite);
			PathList.Add(path);

			path = PathFactory.AddToPath(Movement.Direction.BottomRight).SetIsRecursive(true).Create(isWhite);
			PathList.Add(path);
		}
	}
}