namespace Chess.ChessPieces
{
    abstract class ChessPiece
    {
        protected bool isWhite;

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
        protected abstract void GetEaten();
        protected abstract void Eat();
        protected abstract bool CanEat();
        protected abstract bool TryEat(int rown, int column);
    }
}
