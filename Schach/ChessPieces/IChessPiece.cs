using System.Collections;
using System.Collections.Generic;
using System.Windows.Documents;

namespace Chess.ChessPieces
{
    interface IChessPiece
    {
        bool IsBlack();
        bool IsWhite();
        List<Path> PathList { get; }
    }
}