using System.Collections.Generic;
using System.Windows.Media;

namespace Chess.ChessPieces
{
    interface IChessPiece
    {
        bool IsBlack();
        bool IsWhite();
        List<Path.Path> PathList { get; }
        ImageSource Texture { get;}
    }
}