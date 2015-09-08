using System;
using System.Windows.Media.Imaging;

namespace Chess.ChessPieces
{
    class Bishop : ChessPieceBase
    {
        public Bishop(bool isWhite) : base(isWhite)
        {
            
        }
    }
}


//Texture = isWhite 
               // ? Properties.Resources.WhiteBishop.ToBitmapSource()
            //    : Properties.Resources.BlackBishop.ToBitmapSource();
