using System;
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
        public ImageSource Texture { get; set; }

        public List<Path.Path> PathList { get; }

        public bool IsBlack()
        {
            return !_colorIsWhite;
        }

        protected ChessPieceBase(bool isWhite)
        {
            _colorIsWhite = isWhite;
            PathList = new List<Path.Path>();
        }

        public bool TryMoveThere(Path.Path path)
        {
            if (!PossiblePath(path)) return false;
            
            MoveThere(path);

            return true;
        }

        public bool TryJumpThere(int row, int column)
        {
            throw new NotImplementedException();
        }

        protected abstract bool PossiblePath(Path.Path path);

        protected abstract bool CanMoveTo(int row, int column);

        protected void MoveThere(Path.Path path)
        {
            // TODO Move the Piece!
            // _piece.animate();
        }

        protected bool TryEat(Path.Path path)
        {
            if (!CanEat(path)) return false;

            Eat(path);

            return true;
        }

        protected abstract bool CanEat(Path.Path path);

        protected void Eat(Path.Path path)
        {
            // TODO Remove the Piece from the Field!
        }
    }
}
