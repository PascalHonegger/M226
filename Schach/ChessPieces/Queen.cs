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
            PathList.Add(
                    new PathFactory().AddToPath
                    (Movement.Direction.TopLeft).SetIsRecursive(true).Create());
            PathList.Add(
                    new PathFactory().AddToPath
                    (Movement.Direction.TopRight).SetIsRecursive(true).Create());
            PathList.Add(
                    new PathFactory().AddToPath
                    (Movement.Direction.BottomLeft).SetIsRecursive(true).Create());
            PathList.Add(
                    new PathFactory().AddToPath
                    (Movement.Direction.BottomRight).SetIsRecursive(true).Create());
        }
    }
}
