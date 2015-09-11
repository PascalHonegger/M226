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

        public bool DidMove
        {
            get { return DidMove; }
            set
            {
                if (value == DidMove) return;
                if (this is Pawn && !DidMove)
                {
                    Path.Path path;
                    if (IsWhite())
                    {
                        path =
                            new PathFactory().AddToPath(Movement.Direction.Top)
                                .AddToPath(Movement.Direction.Top)
                                .SetIsRecursive(false)
                                .Create();
                    }
                    else
                    {
                        path =
                           new PathFactory().AddToPath(Movement.Direction.Bottom)
                               .AddToPath(Movement.Direction.Bottom)
                               .SetIsRecursive(false)
                               .Create();
                    }
                    
                    PathList.Remove(path);
                }
                DidMove = value;
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
