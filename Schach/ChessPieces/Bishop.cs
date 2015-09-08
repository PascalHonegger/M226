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

            var path = new PathCreator().AddToPath(Movement.Direction.TopLeft).SetIsRecursive(true).Build();
            PathList.Add(path);

            path = new PathCreator().AddToPath(Movement.Direction.TopRight).SetIsRecursive(true).Build();
            PathList.Add(path);

            path = new PathCreator().AddToPath(Movement.Direction.BottomLeft).SetIsRecursive(true).Build();
            PathList.Add(path);

            path = new PathCreator().AddToPath(Movement.Direction.BottomRight).SetIsRecursive(true).Build();
            PathList.Add(path);
        }
    }
}


//Texture = isWhite 
               // ? Properties.Resources.WhiteBishop.ToBitmapSource()
            //    : Properties.Resources.BlackBishop.ToBitmapSource();
