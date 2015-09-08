using Chess.BoardPieces.Cells;

namespace Chess.ChessPieces
{
    class Knight : ChessPieceBase
    {
        public Knight(bool isWhite) : base(isWhite)
        {
            Texture = isWhite
                ? Properties.Resources.WhiteKnight.ToBitmapSource()
                : Properties.Resources.BlackKnight.ToBitmapSource();
        }
    }
}
