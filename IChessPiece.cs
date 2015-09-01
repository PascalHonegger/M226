using System;

interface IChessPiece
{
    public bool tryMoveThere(int row, int collumn);

    public bool canMoveThere(int row, int collumn);

    public void moveThere(int row, int collumn);

    public void getEaten();

    public void eat();

    public bool canEat();

    public bool tryEat(int rown, int collumn);
}
