using System.Collections.Generic;
using Chess.Cells;
using Chess.Path;

namespace Chess.ChessPieces
{
    public sealed class Pawn : ChessPieceBase
    {
        public readonly List<Path.Path> EatList;

        public Pawn(bool isWhite) : base(isWhite)
        {
            Texture = isWhite
                ? Properties.Resources.WhitePawn.ToBitmapSource()
                : Properties.Resources.BlackPawn.ToBitmapSource();

            EatList = new List<Path.Path>();

            if (IsBlack())
            {
                PathList.Add(
                    new PathFactory().AddToPath
                    (Movement.Direction.Bottom).SetIsRecursive(false).Create());

                EatList.Add(
                   new PathFactory().AddToPath
                    (Movement.Direction.BottomLeft).SetIsRecursive(false).Create());

                EatList.Add(
                    new PathFactory().AddToPath
                    (Movement.Direction.BottomRight).SetIsRecursive(false).Create());
            }
            else
            {
                PathList.Add(
                    new PathFactory().AddToPath
                    (Movement.Direction.Top).SetIsRecursive(false).Create());

                EatList.Add(
                    new PathFactory().AddToPath
                    (Movement.Direction.TopLeft).SetIsRecursive(false).Create());

                EatList.Add(
                    new PathFactory().AddToPath
                    (Movement.Direction.TopRight).SetIsRecursive(false).Create());
            }
        }
    }
}
