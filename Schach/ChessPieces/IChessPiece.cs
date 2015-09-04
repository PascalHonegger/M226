namespace Chess.ChessPieces
{
    interface IChessPiece
    {
        bool IsBlack();
        bool IsWhite();
        bool TryMoveThere(int row, int column);
        bool TryJumpThere(int row, int column);
    }
}