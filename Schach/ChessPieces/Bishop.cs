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
                ? new BitmapImage(new Uri("Resources/WhiteBishop.png"))
                : new BitmapImage(new Uri("C:\\Users\\A610222\\Source\\Repos\\M226\\Schach\\Resources\\BlackBishop.png"));

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
