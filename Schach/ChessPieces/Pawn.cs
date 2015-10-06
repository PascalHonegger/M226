using System.Collections.Generic;
using Chess.Cells;
using Chess.Path;
using Chess.Properties;

namespace Chess.ChessPieces
{
	public sealed class Pawn : ChessPieceBase
	{
		public readonly List<Path.Path> EatList;

		public Pawn(bool isWhite) : base(isWhite)
		{
			Texture = isWhite
				? Resources.WhitePawn.ToBitmapSource()
				: Resources.BlackPawn.ToBitmapSource();

			EatList = new List<Path.Path>();

			if (IsBlack())
			{
				PathList.Add(
					new PathFactory().AddToPath
						(Movement.Direction.Bottom).AddToPath
						(Movement.Direction.Bottom).SetIsRecursive(false).Create(isWhite));

				PathList.Add(
					new PathFactory().AddToPath
						(Movement.Direction.Bottom).SetIsRecursive(false).Create(isWhite));

				EatList.Add(
					new PathFactory().AddToPath
						(Movement.Direction.BottomLeft).SetIsRecursive(false).Create(isWhite));

				EatList.Add(
					new PathFactory().AddToPath
						(Movement.Direction.BottomRight).SetIsRecursive(false).Create(isWhite));
			}
			else
			{
				PathList.Add(
					new PathFactory().AddToPath
						(Movement.Direction.Top).AddToPath
						(Movement.Direction.Top).SetIsRecursive(false).Create(isWhite));

				PathList.Add(
					new PathFactory().AddToPath
						(Movement.Direction.Top).SetIsRecursive(false).Create(isWhite));

				EatList.Add(
					new PathFactory().AddToPath
						(Movement.Direction.TopLeft).SetIsRecursive(false).Create(isWhite));

				EatList.Add(
					new PathFactory().AddToPath
						(Movement.Direction.TopRight).SetIsRecursive(false).Create(isWhite));
			}
		}
	}
}