using Chess.Cells;
using Chess.Path;
using Chess.Properties;

namespace Chess.ChessPieces
{
	public sealed class Queen : ChessPieceBase
	{
		public Queen(bool isWhite, bool hasTextures = true) : base(isWhite)
		{
			if (hasTextures)
			{
				Texture = isWhite
					? Resources.WhiteQueen.ToBitmapSource()
					: Resources.BlackQueen.ToBitmapSource();
			}

			PathList.Add(
				PathFactory.AddToPath
					(Movement.Direction.Top).SetIsRecursive(true).Create(isWhite));
			PathList.Add(
				PathFactory.AddToPath
					(Movement.Direction.Left).SetIsRecursive(true).Create(isWhite));
			PathList.Add(
				PathFactory.AddToPath
					(Movement.Direction.Bottom).SetIsRecursive(true).Create(isWhite));
			PathList.Add(
				PathFactory.AddToPath
					(Movement.Direction.Right).SetIsRecursive(true).Create(isWhite));
			PathList.Add(
				PathFactory.AddToPath
					(Movement.Direction.TopLeft).SetIsRecursive(true).Create(isWhite));
			PathList.Add(
				PathFactory.AddToPath
					(Movement.Direction.TopRight).SetIsRecursive(true).Create(isWhite));
			PathList.Add(
				PathFactory.AddToPath
					(Movement.Direction.BottomLeft).SetIsRecursive(true).Create(isWhite));
			PathList.Add(
				PathFactory.AddToPath
					(Movement.Direction.BottomRight).SetIsRecursive(true).Create(isWhite));
		}
	}
}