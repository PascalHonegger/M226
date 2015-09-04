using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Drawing;
using System.Runtime.CompilerServices;
using System.Runtime.Remoting.Messaging;
using System.Windows.Media;
using System.Windows.Media.Imaging;
using Chess.Annotations;
using Chess.ChessPieces;

namespace Chess.BoardPieces.Cells
{
    public class CellViewModel : INotifyPropertyChanged
    {
        private SolidColorBrush _bgc;
        private ChessPiece _currentChessPiece;
        private Image _bgi;
        private readonly Dictionary<Movement.Direction, CellViewModel> _movements = new Dictionary<Movement.Direction, CellViewModel>();

        public CellViewModel(ChessPiece currentChessPiece, CellViewModel top, CellViewModel topright, CellViewModel right, CellViewModel bottomright,
            CellViewModel bottom, CellViewModel bottomleft, CellViewModel left, CellViewModel topleft)
        {
            _currentChessPiece = currentChessPiece;
            _movements.Add(Movement.Direction.Top, top);
            _movements.Add(Movement.Direction.TopRight, topright);
            _movements.Add(Movement.Direction.Right, right);
            _movements.Add(Movement.Direction.BottomRight, bottomright);
            _movements.Add(Movement.Direction.Bottom, bottom);
            _movements.Add(Movement.Direction.BottomLeft, bottomleft);
            _movements.Add(Movement.Direction.Left, left);
            _movements.Add(Movement.Direction.TopLeft, topleft);

        }

        public ImageSource Image
        {
            get { return Image; }
            private set
            {
                if (Equals(Image, value)) return;
                Image = value;
                OnPropertyChanged(nameof(Image));
            }
        }

        public SolidColorBrush Bgc
        {
            get { return _bgc; }
            set
            {
                if (_bgc == value) return;
                _bgc = value;
                OnPropertyChanged(nameof(Bgc));
            }
        }

        public Image Bgi
        {
            get {return _bgi; }

            set {
                if (_bgi == value) return;
                _bgi = value;
                OnPropertyChanged(nameof(Bgi));
            }
        }

        public event PropertyChangedEventHandler PropertyChanged;

        [NotifyPropertyChangedInvocator]
        protected virtual void OnPropertyChanged([CallerMemberName] string propertyName = null)
        {
            PropertyChanged?.Invoke(this, new PropertyChangedEventArgs(propertyName));
        }
    }
}
