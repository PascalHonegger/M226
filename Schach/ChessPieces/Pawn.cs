using System;
using System.Collections.Generic;
using System.Windows.Documents;
using System.Windows.Forms.VisualStyles;
using Chess.BoardPieces;

namespace Chess.ChessPieces
{
    class Pawn : ChessPiece
    {
        public Pawn(bool isWhite) : base(isWhite)
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
