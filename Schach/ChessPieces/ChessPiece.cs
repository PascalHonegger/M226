namespace Chess.ChessPieces
{
    abstract class ChessPiece
    {
        protected bool ColorIsWhite;

        protected bool IsWhite()
        {
            return ColorIsWhite;
        }

        protected bool IsBlack()
        {
            return !ColorIsWhite;
        }

        protected ChessPiece(int row, int column, bool isWhite)
        {
            ColorIsWhite = isWhite;
        }

        protected bool TryMoveThere(int row, int column)
        {
            if (!CanMoveThere(row, column)) return false;
            
            MoveThere(row, column);

            return true;
        }
        protected abstract bool CanMoveThere(int row, int column);

        protected void MoveThere(int row, int column)
        {
            // TODO Move the Piece!
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