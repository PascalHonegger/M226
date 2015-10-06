using Chess.Cells;
using Chess.Path;
using Chess.Properties;

namespace Chess.ChessPieces
{
	public sealed class Bishop : ChessPieceBase
	{
		public Bishop(bool isWhite) : base(isWhite)
		{
			Texture = isWhite
				? Resources.WhiteBishop.ToBitmapSource()
				: Resources.BlackBishop.ToBitmapSource();

			var path = new PathFactory().AddToPath(Movement.Direction.TopLeft).SetIsRecursive(true).Create(isWhite);
			PathList.Add(path);

			path = new PathFactory().AddToPath(Movement.Direction.TopRight).SetIsRecursive(true).Create(isWhite);
			PathList.Add(path);

			path = new PathFactory().AddToPath(Movement.Direction.BottomLeft).SetIsRecursive(true).Create(isWhite);
			PathList.Add(path);

			path = new PathFactory().AddToPath(Movement.Direction.BottomRight).SetIsRecursive(true).Create(isWhite);
			PathList.Add(path);
		}
	}
}