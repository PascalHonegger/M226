using Chess.Annotations;
using Chess.ChessPieces;
using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Linq;
using System.Runtime.CompilerServices;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Media.Imaging;

namespace Chess.GraveYard
{
    public sealed class GraveYardViewModel : INotifyPropertyChanged
    {
        private ChessPieceBase _currentChessChessPiece;
        private BitmapSource _image;

        public event PropertyChangedEventHandler PropertyChanged;

        public GraveYardViewModel(ChessPieceBase currentChessChessPiece, bool isDead)
        {

        }

        private ChessPieceBase CurrentChessPiece
        {
            get
            {
                return _currentChessChessPiece;
            }
            set
            {
                _currentChessChessPiece = value;
                if(_currentChessChessPiece.IsDead())
                {
                    Image = _currentChessChessPiece?.Texture;
                }
                
            }
        }

        public BitmapSource Image
        {
            get { return _image; }
            private set
            {
                if (Equals(_image, value) || value == null) return;
                _image = value;
                OnPropertyChanged(nameof(Image));
            }
        }

 //       public event PropertyChangedEventHandler PropertyChanged;

        [NotifyPropertyChangedInvocator]
        private void OnPropertyChanged([CallerMemberName] string propertyName = null)
        {
            PropertyChanged?.Invoke(this, new PropertyChangedEventArgs(propertyName));
        }
    }
}
