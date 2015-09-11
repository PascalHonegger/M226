using System.Collections.Generic;
using System.Windows.Media.Imaging;

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
