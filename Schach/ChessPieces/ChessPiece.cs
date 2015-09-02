using System.Windows.Controls;

namespace Chess.ChessPieces
{
    abstract class ChessPiece
    {
        private readonly bool _colorIsWhite;
        private ContentControl _piece;
        
        public bool IsWhite()
        {
            return _colorIsWhite;
        }

        public bool IsBlack()
        {
            return !_colorIsWhite;
        }

        protected ChessPiece(int row, int column, bool isWhite, ContentControl piece)
        {
            _colorIsWhite = isWhite;
            _piece = piece;
        }

        public bool TryMoveThere(int row, int column)
        {
            if (!CanMoveThere(row, column)) return false;
            
            MoveThere(row, column);

            return true;
        }
        public abstract bool CanMoveThere(int row, int column);

        protected void MoveThere(int row, int column)
        {
            // TODO Move the Piece!
            // _piece.animate();
        }

        protected bool TryEat(int row, int column)
        {
            if (!CanEat(row, column)) return false;

            Eat(row, column);

            return true;
        }

        protected abstract bool CanEat(int row, int column);

        protected void Eat(int row, int column)
        {
            // TODO Remove the Piece from the Field!
            MoveThere(row, column);
        }
    }
}
