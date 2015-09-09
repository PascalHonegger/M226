using Chess.BoardPieces.Cells;

namespace Chess.ChessPieces
{
    class Queen : ChessPieceBase
    {
        public Queen(bool isWhite) : base(isWhite)
        {
            Texture = isWhite
                ? Properties.Resources.WhiteQueen.ToBitmapSource()
                : Properties.Resources.BlackQueen.ToBitmapSource();
        }
    }
}
