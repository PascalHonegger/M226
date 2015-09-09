using System;
using System.Runtime;
using System.Windows.Media.Imaging;
using Chess.BoardPieces.Cells;
using Chess.Path;

namespace Chess.ChessPieces
{
    class Bishop : ChessPieceBase
    {
        public Bishop(bool isWhite) : base(isWhite)
        {
            Texture = isWhite
                ? Properties.Resources.WhiteBishop.ToBitmapSource()
                : Properties.Resources.BlackBishop.ToBitmapSource();

            var path = new PathFactory().AddToPath(Movement.Direction.TopLeft).SetIsRecursive(true).Create();
            PathList.Add(path);

            path = new PathFactory().AddToPath(Movement.Direction.TopRight).SetIsRecursive(true).Create();
            PathList.Add(path);

            path = new PathFactory().AddToPath(Movement.Direction.BottomLeft).SetIsRecursive(true).Create();
            PathList.Add(path);

            path = new PathFactory().AddToPath(Movement.Direction.BottomRight).SetIsRecursive(true).Create();
            PathList.Add(path);
        }
    }
}



