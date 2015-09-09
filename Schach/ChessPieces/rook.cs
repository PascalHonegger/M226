using Chess.BoardPieces.Cells;

namespace Chess.ChessPieces
{
    class Rook : ChessPieceBase
    {
        public Rook(bool isWhite) : base(isWhite)
        {
            Texture = isWhite 
                ? Properties.Resources.WhiteRook.ToBitmapSource()
                : Properties.Resources.BlackRook.ToBitmapSource();
        }
    }
}
