using System.Collections.Generic;
using System.Windows.Media;

namespace Chess.ChessPieces
{
    public abstract class ChessPieceBase : IChessPiece
    {
        private readonly bool _colorIsWhite;

        public bool IsWhite()
        {
            return _colorIsWhite;
        }
        public bool IsBlack()
        {
            return !_colorIsWhite;
        }

        public ImageSource Texture { get; set; }

        public List<Path.Path> PathList { get; }

        protected ChessPieceBase(bool isWhite)
        {
            _colorIsWhite = isWhite;
            PathList = new List<Path.Path>();
        }
    }
}
