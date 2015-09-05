using System;
using System.Windows.Media.Imaging;

namespace Chess.ChessPieces
{
    class Rook : ChessPieceBase
    {
        public Rook(bool isWhite) : base(isWhite)
        {
            Texture = new BitmapImage(new Uri("C:\\Git\\M226\\Schach\\Resources\\BlackBishop.png"));
        }

        protected override bool PossiblePath(Path.Path path)
        {
            throw new NotImplementedException();
        }

        protected override bool CanMoveTo(int row, int column)
        {
            throw new NotImplementedException();
        }

        protected override bool CanEat(Path.Path path)
        {
            throw new NotImplementedException();
        }
    }
}
