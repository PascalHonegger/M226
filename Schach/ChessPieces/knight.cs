using Chess.Cells;
using Chess.Properties;

namespace Chess.ChessPieces
{
	public sealed class Knight : ChessPieceBase
	{
		public Knight(bool isWhite, bool hasTextures = true) : base(isWhite)
		{
			if (hasTextures)
			{
				Texture = isWhite
					? Resources.WhiteKnight.ToBitmapSource()
					: Resources.BlackKnight.ToBitmapSource();
			}

			PathList.Add(
				PathFactory.AddToPath
					(Movement.Direction.Top).AddToPath
					(Movement.Direction.TopLeft).SetIsRecursive(false).Create());

			PathList.Add(
				PathFactory.AddToPath
					(Movement.Direction.Top).AddToPath
					(Movement.Direction.TopRight).SetIsRecursive(false).Create());

			PathList.Add(
				PathFactory.AddToPath
					(Movement.Direction.Bottom).AddToPath
					(Movement.Direction.BottomRight).SetIsRecursive(false).Create());

			PathList.Add(
				PathFactory.AddToPath
					(Movement.Direction.Bottom).AddToPath
					(Movement.Direction.BottomLeft).SetIsRecursive(false).Create());
			PathList.Add(
				PathFactory.AddToPath
					(Movement.Direction.Left).AddToPath
					(Movement.Direction.TopLeft).SetIsRecursive(false).Create());
			PathList.Add(
				PathFactory.AddToPath
					(Movement.Direction.Left).AddToPath
					(Movement.Direction.BottomLeft).SetIsRecursive(false).Create());
			PathList.Add(
				PathFactory.AddToPath
					(Movement.Direction.Right).AddToPath
					(Movement.Direction.TopRight).SetIsRecursive(false).Create());
			PathList.Add(
				PathFactory.AddToPath
					(Movement.Direction.Right).AddToPath
					(Movement.Direction.BottomRight).SetIsRecursive(false).Create());
		}
	}
}