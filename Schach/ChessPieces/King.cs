using Chess.BoardPieces.Cells;

namespace Chess.ChessPieces
{
    class King : ChessPieceBase
    {
        public King(bool isWhite) : base(isWhite)
        {
            Texture = isWhite
                ? Properties.Resources.WhiteKing.ToBitmapSource()
                : Properties.Resources.BlackKing.ToBitmapSource();
        }
    }
}
