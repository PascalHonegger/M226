using Chess.Cells;
using Chess.Path;

namespace Chess.ChessPieces
{
    public class King : ChessPieceBase
    {
        public King(bool isWhite) : base(isWhite)
        {
            Texture = isWhite
                ? Properties.Resources.WhiteKing.ToBitmapSource()
                : Properties.Resources.BlackKing.ToBitmapSource();

            PathList.Add(new PathFactory().AddToPath(Movement.Direction.Top).SetIsRecursive(false).Create());

            PathList.Add(new PathFactory().AddToPath(Movement.Direction.TopLeft).SetIsRecursive(false).Create());

            PathList.Add(new PathFactory().AddToPath(Movement.Direction.Left).SetIsRecursive(false).Create());

            PathList.Add(new PathFactory().AddToPath(Movement.Direction.BottomLeft).SetIsRecursive(false).Create());

            PathList.Add(new PathFactory().AddToPath(Movement.Direction.Bottom).SetIsRecursive(false).Create());

            PathList.Add(new PathFactory().AddToPath(Movement.Direction.BottomRight).SetIsRecursive(false).Create());

            PathList.Add(new PathFactory().AddToPath(Movement.Direction.Right).SetIsRecursive(false).Create());

            PathList.Add(new PathFactory().AddToPath(Movement.Direction.TopRight).SetIsRecursive(false).Create());
        }
    }
}
