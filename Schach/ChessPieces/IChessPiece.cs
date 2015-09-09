using System.Collections.Generic;
using System.Windows.Media;
using System.Windows.Media.Imaging;

namespace Chess.ChessPieces
{
    interface IChessPiece
    {
        bool IsBlack();
        bool IsWhite();
        bool IsDead();
        bool IsAlvie();
        List<Path.Path> PathList { get; }
        BitmapSource Texture { get;}
    }
}