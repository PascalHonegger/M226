using System.Windows.Media;

namespace Chess.ChessPieces
{
    public abstract class ChessPiece : IChessPiece
    {
        private readonly bool _colorIsWhite;
        private readonly ImageSource _texture;

        public bool IsWhite()
        {
            return _colorIsWhite;
        }

        public bool IsBlack()
        {
            return !_colorIsWhite;
        }

        protected ChessPiece(bool isWhite, ImageSource texture)
        {
            _colorIsWhite = isWhite;
            _texture = texture;
        }

        public bool TryMoveThere(int row, int column)
        {
            if (!PossiblePath(row, column)) return false;
            
            MoveThere(row, column);

            return true;
        }

        public bool TryJumpThere(int row, int column)
        {
            throw new System.NotImplementedException();
        }

        protected abstract bool PossiblePath(int row, int column);

        protected abstract bool CanMoveTo(int row, int column);

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
