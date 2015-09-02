using System.Linq;
using Chess.ChessPieces;

namespace Chess.BoardPieces
{
    internal class Board
    {
        public ChessPieceHandler ChessPieceList = new ChessPieceHandler();

        public ChessPiece GetChessPiece(int row, int column)
        {
            return ChessPieceList.First(piece => piece.Row == row && piece.Column == column);
        }

        public void StartGame()
        {
            ChessPieceList = new ChessPieceHandler();
            // TODO Add all ChessPiece-Creation.
            // TODO Make all Pieces Visible.
        }

        public bool PlaceTaken(int row, int column, bool isWhite)
        {
            return GetChessPiece(row, column) == null || GetChessPiece(row, column).IsWhite() && isWhite ||
                   GetChessPiece(row, column).IsBlack() && !isWhite;
        }
    }
}
