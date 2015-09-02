using System;

namespace Chess.ChessPieces
{
    class Knight : ChessPiece
    {
        public Knight(int row, int column, bool isWhite) : base(row, column, isWhite)
        {
        }

        protected override bool CanMoveThere(int row, int column)
        {
            throw new NotImplementedException();
        }

        protected override bool CanEat(int row, int column)
        {
            throw new NotImplementedException();
        }
    }
}
