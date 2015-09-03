namespace Chess.ChessPieces
{
    interface IChessPiece
    {
        int Column { get; }
        int Row { get; }

        bool IsBlack();
        bool IsWhite();
        bool TryMoveThere(int row, int column);
    }
}