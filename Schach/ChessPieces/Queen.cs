using Chess.Path;
using Chess.BoardPieces.Cells;

namespace Chess.ChessPieces
{
    class Queen : ChessPieceBase
    {
        public Queen(bool isWhite) : base(isWhite)
        {
            Texture = isWhite
                ? Properties.Resources.WhiteQueen.ToBitmapSource()
                : Properties.Resources.BlackQueen.ToBitmapSource();

            PathList.Add(
                    new PathCreator().AddToPath
                    (Movement.Direction.Top).SetIsRecursive(true).Build());
            PathList.Add(
                    new PathCreator().AddToPath
                    (Movement.Direction.Left).SetIsRecursive(true).Build());
            PathList.Add(
                    new PathCreator().AddToPath
                    (Movement.Direction.Bottom).SetIsRecursive(true).Build());
            PathList.Add(
                    new PathCreator().AddToPath
                    (Movement.Direction.Right).SetIsRecursive(true).Build());
            PathList.Add(
                    new PathCreator().AddToPath
                    (Movement.Direction.TopLeft).SetIsRecursive(true).Build());
            PathList.Add(
                    new PathCreator().AddToPath
                    (Movement.Direction.TopRight).SetIsRecursive(true).Build());
            PathList.Add(
                    new PathCreator().AddToPath
                    (Movement.Direction.BottomLeft).SetIsRecursive(true).Build());
            PathList.Add(
                    new PathCreator().AddToPath
                    (Movement.Direction.BottomRight).SetIsRecursive(true).Build());
        }
    }
}
