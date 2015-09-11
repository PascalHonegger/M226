using Chess.Cells;
using Chess.Path;

namespace Chess.ChessPieces
{
    public class Knight : ChessPieceBase
    {
        public Knight(bool isWhite) : base(isWhite)
        {
            Texture = isWhite
                ? Properties.Resources.WhiteKnight.ToBitmapSource()
                : Properties.Resources.BlackKnight.ToBitmapSource();

            PathList.Add(
                new PathFactory().AddToPath
                    (Movement.Direction.Top).AddToPath
                    (Movement.Direction.TopLeft).SetIsRecursive(false).Create());

            PathList.Add(
                new PathFactory().AddToPath
                    (Movement.Direction.Top).AddToPath
                    (Movement.Direction.TopRight).SetIsRecursive(false).Create());

            PathList.Add(
                new PathFactory().AddToPath
                    (Movement.Direction.Bottom).AddToPath
                    (Movement.Direction.BottomRight).SetIsRecursive(false).Create());

            PathList.Add(
                new PathFactory().AddToPath
                    (Movement.Direction.Bottom).AddToPath
                    (Movement.Direction.BottomLeft).SetIsRecursive(false).Create());
            PathList.Add(
                new PathFactory().AddToPath
                    (Movement.Direction.Left).AddToPath
                    (Movement.Direction.TopLeft).SetIsRecursive(false).Create());
            PathList.Add(
                new PathFactory().AddToPath
                    (Movement.Direction.Left).AddToPath
                    (Movement.Direction.BottomLeft).SetIsRecursive(false).Create());
            PathList.Add(
                new PathFactory().AddToPath
                    (Movement.Direction.Right).AddToPath
                    (Movement.Direction.TopRight).SetIsRecursive(false).Create());
            PathList.Add(
                new PathFactory().AddToPath
                    (Movement.Direction.Right).AddToPath
                    (Movement.Direction.BottomRight).SetIsRecursive(false).Create());
        }
    }
}
