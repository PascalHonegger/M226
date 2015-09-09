using System;
using System.Collections.Generic;
using System.Windows.Media;
using System.Windows.Media.Imaging;

namespace Chess.ChessPieces
{
    public abstract class ChessPieceBase : IChessPiece
    {
        private readonly bool _colorIsWhite;
        private readonly bool _pieceIsDead;

   //     public List</*KAIH ANIG WAS FÜR EN SATANTYP*/> graveList  { get; };

        public bool IsWhite()
        {
            return _colorIsWhite;
        }
        public bool IsBlack()
        {
            return !_colorIsWhite;
        }

        public bool IsDead()
        {
            return _pieceIsDead;
        }

        public bool IsAlvie()
        {
            return !_pieceIsDead;
        }

        public BitmapSource Texture { get; set; }

        public List<Path.Path> PathList { get; }

        protected ChessPieceBase(bool isWhite)
        {
            _colorIsWhite = isWhite;
            PathList = new List<Path.Path>();
        }
    }
}
