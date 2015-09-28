using Chess.Cells;
using Chess.Path;
using Chess.Properties;

namespace Chess.ChessPieces
{
	public sealed class Rook : ChessPieceBase
	{
		public Rook(bool isWhite) : base(isWhite)
		{
			Texture = isWhite
				? Resources.WhiteRook.ToBitmapSource()
				: Resources.BlackRook.ToBitmapSource();

			PathList.Add(
				new PathFactory().AddToPath
					(Movement.Direction.Top).SetIsRecursive(true).Create());
			PathList.Add(
				new PathFactory().AddToPath
					(Movement.Direction.Left).SetIsRecursive(true).Create());
			PathList.Add(
				new PathFactory().AddToPath
					(Movement.Direction.Bottom).SetIsRecursive(true).Create());
			PathList.Add(
				new PathFactory().AddToPath
					(Movement.Direction.Right).SetIsRecursive(true).Create());
		}
	}
}