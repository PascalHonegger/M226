using Chess.Cells;
using Chess.Path;

namespace Chess.ChessPieces
{
    class Pawn : ChessPieceBase
    {
        public Pawn(bool isWhite) : base(isWhite)
        {
            Texture = isWhite
                ? Properties.Resources.WhitePawn.ToBitmapSource()
                : Properties.Resources.BlackPawn.ToBitmapSource();


            if (!isWhite)
            {
                PathList.Add(
                    new PathFactory().AddToPath
                    (Movement.Direction.Bottom).SetIsRecursive(false).Create());

                PathList.Add(
                   new PathFactory().AddToPath
                    (Movement.Direction.BottomLeft).SetIsRecursive(false).Create());

                PathList.Add(
                    new PathFactory().AddToPath
                    (Movement.Direction.BottomRight).SetIsRecursive(false).Create());
            }
            else
            {
                PathList.Add(
                    new PathFactory().AddToPath
                    (Movement.Direction.Top).SetIsRecursive(false).Create());

                PathList.Add(
                    new PathFactory().AddToPath
                    (Movement.Direction.TopLeft).SetIsRecursive(false).Create());

                PathList.Add(
                    new PathFactory().AddToPath
                    (Movement.Direction.TopRight).SetIsRecursive(false).Create());
            }
        }
    }
}
