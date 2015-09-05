using System;
using System.Windows.Media.Imaging;

namespace Chess.ChessPieces
{
    class Bishop : ChessPieceBase
    {
        public Bishop(bool isWhite) : base(isWhite)
        {
            // TODO Texture = isWhite ? new BitmapImage(new Uri("Resources/WhiteBishop.png")) : new BitmapImage(new Uri("C:\\Users\\A610222\\Source\\Repos\\M226\\Schach\\Resources\\BlackBishop.png"));
        }
    }
}
