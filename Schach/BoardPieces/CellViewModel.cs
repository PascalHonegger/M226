using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Drawing;
using System.IO;
using System.Runtime.CompilerServices;
using System.Windows.Media;
using Chess.Annotations;
using Chess.ChessPieces;
using Brushes = System.Windows.Media.Brushes;

namespace Chess.BoardPieces
{
    public class CellViewModel : INotifyPropertyChanged
    {
        private SolidColorBrush _bgc;
        private ChessPiece _currentChessPiece;
        private Image _bgi;
        private Dictionary<BoardAroundMe, CellViewModel> movements = new Dictionary<BoardAroundMe, CellViewModel>();

        private enum BoardAroundMe
        {
            Top,
            TopRight,
            Right,
            BottomRight,
            Bottom,
            BottomLeft,
            Left,
            TopLeft
        }

        public CellViewModel(CellViewModel top, CellViewModel topright, CellViewModel right, CellViewModel bottomright,
            CellViewModel bottom, CellViewModel bottomleft, CellViewModel left, CellViewModel topleft)
        {
            Bgc = Brushes.Green;
            movements.Add(BoardAroundMe.Top, top);
            movements.Add(BoardAroundMe.TopRight, topright);
            movements.Add(BoardAroundMe.Right, right);
            movements.Add(BoardAroundMe.BottomRight, bottomright);
            movements.Add(BoardAroundMe.Bottom, bottom);
            movements.Add(BoardAroundMe.BottomLeft, bottomleft);
            movements.Add(BoardAroundMe.Left, left);
            movements.Add(BoardAroundMe.TopLeft, topleft);

        }
        public CellViewModel(ChessPiece currentChessPiece)
        {
            this._currentChessPiece = currentChessPiece;
            Bgc = Brushes.Green;
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
