﻿using System;

namespace Chess.ChessPieces
{
    class King : ChessPieceBase
    {
        public King(bool isWhite) : base(isWhite)
        {
            // TODO Implement PathConfigurator
            // PathList.Add(new PathConfigurator().AddToPath(direction).AddToPath().isrecursive()).create();
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
