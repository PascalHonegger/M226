﻿using System;

namespace Chess.ChessPieces
{
    class Pawn : ChessPiece
    {
        public Pawn(int row, int column)
        {
            // TODO spawn a Pawn with a good texture at the desired location!
        }

        protected override bool CanMoveThere(int row, int column)
        {
            throw new NotImplementedException();
        }

        protected override void GetEaten()
        {
            throw new NotImplementedException();
        }

        protected override void Eat()
        {
            throw new NotImplementedException();
        }

        protected override bool CanEat()
        {
            throw new NotImplementedException();
        }

        protected override bool TryEat(int rown, int column)
        {
            throw new NotImplementedException();
        }
    }
}