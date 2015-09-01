using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Chess.ChessPieces
{
    class Bishop : ChessPiece
    {
        public Bishop(int row, int column, bool isWhite) : base(row, column, isWhite)
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
