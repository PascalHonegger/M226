using System.Linq;
using Chess.ChessPieces;

namespace Chess.BoardPieces
{
    internal class Board
    {
        public ChessPieceHandler ChessPieceList;

        public ChessPiece GetChessPiece(int row, int column)
        {
            return ChessPieceList.First(piece => piece.Row == row && piece.Column == column);

            /*new Canvas().Children.Cast<ContentControl>()
                    .First(child => Grid.GetRow(child) == row && Grid.GetColumn(child) == column); */
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
