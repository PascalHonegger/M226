using System;

namespace Chess.ChessPieces
{
    class Queen : ChessPiece
    {
        public Queen(bool isWhite) : base(isWhite)
        {
        }

        protected override bool PossiblePath(Path path)
        {
            throw new NotImplementedException();
        }

        protected override bool CanMoveTo(int row, int column)
        {
            throw new NotImplementedException();
        }

        protected override bool CanEat(Path path)
        {
            throw new NotImplementedException();
        }
    }
}
