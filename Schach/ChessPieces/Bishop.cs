﻿using System;

namespace Chess.ChessPieces
{
    class Bishop : ChessPiece
    {
        public Bishop(int row, int column, bool isWhite) : base(isWhite)
        {
        }

        protected override bool PossiblePath(int row, int column)
        {
            throw new NotImplementedException();
        }

        protected override bool CanMoveTo(int row, int column)
        {
            throw new NotImplementedException();
        }

        protected override bool CanEat(int row, int column)
        {
            throw new NotImplementedException();
        }
    }
}
