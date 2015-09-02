using System;
using System.Linq;
using System.Windows.Controls;
using Chess.ChessPieces;

namespace Chess.BoardPieces
{
    internal class Board
    {
        public ChessPiece GetChessPiece(int row, int column)
        {
            return
                new Canvas().Children.Cast<ContentControl>()
                    .First(child => Grid.GetRow(child) == row && Grid.GetColumn(child) == column);
        }

        protected static void StartGame()
        {
            // TODO Add all ChessPiece-Creation.
            // TODO Make all Pieces Visible.
        }

        public bool PlaceTaken(int row, int column, bool isWhite)
        {
            return GetChessPiece(row, column) == null || GetChessPiece(row, column).IsWhite() && isWhite ||
                   GetChessPiece(row, column).IsBlack() && !isWhite || false;
        }
    }
}
