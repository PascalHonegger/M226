using System;

namespace Chess.ChessPieces
{
    class Knight : ChessPieceBase
    {
        public Knight(bool isWhite) : base(isWhite)
        {
        }

        protected override bool PossiblePath(Path.Path path)
        {
            throw new NotImplementedException();
        }

        protected override bool CanMoveTo(int row, int column)
        {
            throw new NotImplementedException();
        }

        protected override bool CanEat(Path.Path path)
        {
            throw new NotImplementedException();
        }
    }
}
