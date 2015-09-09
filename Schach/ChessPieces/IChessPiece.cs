using System.Collections.Generic;
using System.Windows.Media.Imaging;

namespace Chess.ChessPieces
{
    interface IChessPiece
    {
        bool IsBlack();
        bool IsWhite();
        List<Path.Path> PathList { get; }
        BitmapSource Texture { get;}
    }
}