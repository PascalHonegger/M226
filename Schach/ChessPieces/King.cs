using Chess.Cells;
using Chess.Properties;

namespace Chess.ChessPieces
{
	public sealed class King : ChessPieceBase
	{
		public King(bool isWhite, bool hasTextures = true) : base(isWhite)
		{
			if (hasTextures)
			{
				Texture = isWhite
					? Resources.WhiteKing.ToBitmapSource()
					: Resources.BlackKing.ToBitmapSource();
			}

			PathList.Add(PathFactory.AddToPath(Movement.Direction.Top).SetIsRecursive(false).Create());

			PathList.Add(PathFactory.AddToPath(Movement.Direction.TopLeft).SetIsRecursive(false).Create());

			PathList.Add(PathFactory.AddToPath(Movement.Direction.Left).SetIsRecursive(false).Create());

			PathList.Add(PathFactory.AddToPath(Movement.Direction.BottomLeft).SetIsRecursive(false).Create());

			PathList.Add(PathFactory.AddToPath(Movement.Direction.Bottom).SetIsRecursive(false).Create());

			PathList.Add(PathFactory.AddToPath(Movement.Direction.BottomRight).SetIsRecursive(false).Create());

			PathList.Add(PathFactory.AddToPath(Movement.Direction.Right).SetIsRecursive(false).Create());

			PathList.Add(PathFactory.AddToPath(Movement.Direction.TopRight).SetIsRecursive(false).Create());
		}
	}
}