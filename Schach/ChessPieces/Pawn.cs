using Chess.Cells;
using Chess.Properties;

namespace Chess.ChessPieces
{
	public sealed class Pawn : ChessPieceBase
	{
		public Pawn(bool isWhite, bool hasTextures = true) : base(isWhite)
		{
			if (hasTextures)
			{
				Texture = isWhite
				? Resources.WhitePawn.ToBitmapSource()
				: Resources.BlackPawn.ToBitmapSource();
			}

			if (IsBlack())
			{
				PathList.Add(
					PathFactory.AddToPath
						(Movement.Direction.Bottom).AddToPath
						(Movement.Direction.Bottom).SetIsRecursive(false).Create());

				PathList.Add(
					PathFactory.AddToPath
						(Movement.Direction.Bottom).SetIsRecursive(false).Create());

				EatList.Add(
					PathFactory.AddToPath
						(Movement.Direction.BottomLeft).SetIsRecursive(false).Create());

				EatList.Add(
					PathFactory.AddToPath
						(Movement.Direction.BottomRight).SetIsRecursive(false).Create());
			}
			else
			{
				PathList.Add(
					PathFactory.AddToPath
						(Movement.Direction.Top).AddToPath
						(Movement.Direction.Top).SetIsRecursive(false).Create());

				PathList.Add(
					PathFactory.AddToPath
						(Movement.Direction.Top).SetIsRecursive(false).Create());

				EatList.Add(
					PathFactory.AddToPath
						(Movement.Direction.TopLeft).SetIsRecursive(false).Create());

				EatList.Add(
					PathFactory.AddToPath
						(Movement.Direction.TopRight).SetIsRecursive(false).Create());
			}
		}
	}
}