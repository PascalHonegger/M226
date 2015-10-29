using Chess.Cells;
using Chess.Properties;

namespace Chess.ChessPieces
{
	public sealed class Rook : ChessPieceBase
	{
		public Rook(bool isWhite, bool hasTextures = true) : base(isWhite)
		{
			if (hasTextures)
			{
				Texture = isWhite
					? Resources.WhiteRook.ToBitmapSource()
					: Resources.BlackRook.ToBitmapSource();
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
		}
	}
}