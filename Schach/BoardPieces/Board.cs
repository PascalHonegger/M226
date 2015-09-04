using Chess.BoardPieces.Cells;
using Chess.ChessPieces;

namespace Chess.BoardPieces
{
    public class Board
    {
        public int DimensionsChessBoard { get; } = 500;
        public Board()
        {
            A8 = new CellViewModel(new Rook(false), null, null, B8, B7, A7, null, null, null);
            B8 = new CellViewModel(new Knight(false), null, null, C8, C7, B7, A7, A8, null);
            C8 =  new CellViewModel(new Bishop(false), null, null, D8, D7, C7, B7, B8, null);
        }

        public CellViewModel A8 { get; private set;  }
        public CellViewModel B8 { get; private set; }
        public CellViewModel C8 { get; private set; }
        public CellViewModel D8 { get; private set; }
        public CellViewModel E8 { get; private set; }
        public CellViewModel F8 { get; private set; }
        public CellViewModel G8 { get; private set; }
        public CellViewModel H8 { get; private set; }
        public CellViewModel A7 { get; private set; }
        public CellViewModel B7 { get; private set; }
        public CellViewModel C7 { get; private set; }
        public CellViewModel D7 { get; private set; }
        public CellViewModel E7 { get; private set; }
        public CellViewModel F7 { get; private set; }
        public CellViewModel G7 { get; private set; }
        public CellViewModel H7 { get; private set; }

        public void StartGame()
        {

            // TODO Add all ChessPiece-Creation.
            // TODO Make all Pieces Visible.
        }

        public void ResetChessBoard()
        {
            
        }
    }
}
