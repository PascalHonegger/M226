using System.Collections;
using System.Collections.Generic;
using System.Windows.Controls;
using Chess.ChessPieces;

namespace Chess.BoardPieces
{
    class ChessPieceHandler : IEnumerable<ChessPiece>
    {
        List<ChessPiece> _livingChessPieces = new List<ChessPiece>();

        public ChessPieceHandler()
        {
            _livingChessPieces.Add(new Bishop(1, 1, true));
            var blackBishop1 = new ContentControl();
        }

        public IEnumerator<ChessPiece> GetEnumerator()
        {
            return _livingChessPieces.GetEnumerator();
        }

        IEnumerator IEnumerable.GetEnumerator()
        {
            return GetEnumerator();
        }
    }
}
