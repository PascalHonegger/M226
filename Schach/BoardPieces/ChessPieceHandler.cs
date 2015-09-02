using System.Collections;
using System.Collections.Generic;
using System.Windows.Controls;
using Chess.ChessPieces;

namespace Chess.BoardPieces
{
    class ChessPieceHandler : IEnumerable<ChessPiece>
    {
        private List<ChessPiece> _livingChessPieces;

        public ChessPieceHandler()
        {
            _livingChessPieces = new List<ChessPiece>
            {
                new Bishop(0, 0, true),
                new Bishop(1, 0, true)
            };
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
