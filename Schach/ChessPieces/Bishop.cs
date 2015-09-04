using System;
using System.Drawing;
using System.Windows.Media.Imaging;

namespace Chess.ChessPieces
{
    class Bishop : ChessPiece
    {
        public Bishop(bool isWhite) : base(isWhite)
        {
            //Chess.Properties.Resources.blackbishop;
            /*var uriSource = new Uri("Resources/BlackKing.png", UriKind.Relative);
            var b = new BitmapImage(uriSource);
            Texture = isWhite ? null : b.StreamSource;*/
        }

        protected override bool PossiblePath(Path path)
        {
            throw new NotImplementedException();
        }

        protected override bool CanMoveTo(int row, int column)
        {
            throw new NotImplementedException();
        }

        protected override bool CanEat(Path path)
        {
            throw new NotImplementedException();
        }
    }
}
