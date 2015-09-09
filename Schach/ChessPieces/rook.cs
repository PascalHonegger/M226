using Chess.Path;
using Chess.BoardPieces.Cells;
using Chess.Cells;

namespace Chess.ChessPieces
{
    class Rook : ChessPieceBase
    {
        public Rook(bool isWhite) : base(isWhite)
        {
            Texture = isWhite 
                ? Properties.Resources.WhiteRook.ToBitmapSource()
                : Properties.Resources.BlackRook.ToBitmapSource();

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
