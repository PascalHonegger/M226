using System.Collections.Generic;
using System.Linq;
using Chess.ChessPieces;

namespace Chess.BoardPieces
{
    internal class Board
    {
        public List<ChessPiece> ChessPieceList = new List<ChessPiece>();

        public ChessPiece GetChessPiece(int row, int column)
        {
            return ChessPieceList.First(piece => piece.Row == row && piece.Column == column);
        }

        public void StartGame()
        {
            FillChessPieceList();

            // TODO Add all ChessPiece-Creation.
            // TODO Make all Pieces Visible.
        }

        private void FillChessPieceList()
        {
            // Black Column 1
            ChessPieceList.Add(new Rook(0, 0, false));
            ChessPieceList.Add(new Knight(1, 0, false));
            ChessPieceList.Add(new Bishop(2, 0, false));
            ChessPieceList.Add(new Queen(3, 0, false));
            ChessPieceList.Add(new King(4, 0, false));
            ChessPieceList.Add(new Bishop(5, 0, false));
            ChessPieceList.Add(new Knight(6, 0, false));
            ChessPieceList.Add(new Rook(7, 0, false));
            // Black Column 2
            ChessPieceList.Add(new Pawn(0, 1, false));
            ChessPieceList.Add(new Pawn(1, 1, false));
            ChessPieceList.Add(new Pawn(2, 1, false));
            ChessPieceList.Add(new Pawn(3, 1, false));
            ChessPieceList.Add(new Pawn(4, 1, false));
            ChessPieceList.Add(new Pawn(5, 1, false));
            ChessPieceList.Add(new Pawn(6, 1, false));
            ChessPieceList.Add(new Pawn(7, 1, false));

            // White Column 6
            ChessPieceList.Add(new Pawn(0, 1, true));
            ChessPieceList.Add(new Pawn(1, 1, true));
            ChessPieceList.Add(new Pawn(2, 1, true));
            ChessPieceList.Add(new Pawn(3, 1, true));
            ChessPieceList.Add(new Pawn(4, 1, true));
            ChessPieceList.Add(new Pawn(5, 1, true));
            ChessPieceList.Add(new Pawn(6, 1, true));
            ChessPieceList.Add(new Pawn(7, 1, true));
            // White Column 7
            ChessPieceList.Add(new Rook(0, 7, true));
            ChessPieceList.Add(new Knight(1, 7, true));
            ChessPieceList.Add(new Bishop(2, 7, true));
            ChessPieceList.Add(new Queen(3, 7, true));
            ChessPieceList.Add(new King(4, 7, true));
            ChessPieceList.Add(new Bishop(5, 7, true));
            ChessPieceList.Add(new Knight(6, 7, true));
            ChessPieceList.Add(new Rook(7, 7, true));
        }
    }
}
