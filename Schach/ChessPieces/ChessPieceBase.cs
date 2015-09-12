using System.Collections.Generic;
using System.Diagnostics.Eventing.Reader;
using System.Windows.Controls;
using System.Windows.Media.Imaging;
using Chess.Cells;
using Chess.Path;

namespace Chess.ChessPieces
{
    public abstract class ChessPieceBase : IChessPiece
    {
        private readonly bool _colorIsWhite;

        public virtual bool IsWhite()
        {
            return _colorIsWhite;
        }
        public virtual bool IsBlack()
        {
            return !_colorIsWhite;
        }

        private bool _didMove;

        public bool DidMove
        {
            get { return _didMove; }
            set
            {
                if (value == DidMove) return;
                if (this is Pawn)
                {
                    PathList.RemoveAt(0);
                }
                _didMove = value;
            }
        }

        public BitmapSource Texture { get; set; }

        public virtual List<Path.Path> PathList { get; }

        protected ChessPieceBase(bool isWhite)
        {
            _colorIsWhite = isWhite;
            PathList = new List<Path.Path>();
        }

        // Used for Mock-Testing
        protected ChessPieceBase()
        {
            
        }
    }
}
