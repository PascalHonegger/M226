namespace Chess.Classes
{
    interface IChessPiece
    {
        bool TryMoveThere(int row, int collumn);

        bool CanMoveThere(int row, int collumn);

        void MoveThere(int row, int collumn);

        void GetEaten();

        void Eat();

        bool CanEat();

        bool TryEat(int rown, int collumn);
    }
}
