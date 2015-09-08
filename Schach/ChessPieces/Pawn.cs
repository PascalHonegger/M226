using Chess.BoardPieces.Cells;

namespace Chess.ChessPieces
{
    class Pawn : ChessPieceBase
    {
        public Pawn(bool isWhite) : base(isWhite)
        {
            Texture = isWhite
                ? Properties.Resources.WhitePawn.ToBitmapSource()
                : Properties.Resources.BlackPawn.ToBitmapSource();
        }
    }
}
