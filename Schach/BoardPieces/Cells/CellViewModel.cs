﻿using System.Collections.Generic;
using System.ComponentModel;
using System.Runtime.CompilerServices;
using System.Windows.Controls;
using System.Windows.Media;
using System.Windows.Media.Imaging;
using Chess.Annotations;
using Chess.ChessPieces;

namespace Chess.BoardPieces.Cells
{
    public class CellViewModel : INotifyPropertyChanged
    {
        private SolidColorBrush _bgc;
        private ChessPieceBase _currentChessPiece;
        private ImageSource _image;
        private readonly Dictionary<Movement.Direction, CellViewModel> _movements = new Dictionary<Movement.Direction, CellViewModel>();

        public CellViewModel(ChessPieceBase currentChessPiece, CellViewModel top, CellViewModel topright, CellViewModel right, CellViewModel bottomright,
            CellViewModel bottom, CellViewModel bottomleft, CellViewModel left, CellViewModel topleft)
        {
            _movements.Add(Movement.Direction.Top, top);
            _movements.Add(Movement.Direction.TopRight, topright);
            _movements.Add(Movement.Direction.Right, right);
            _movements.Add(Movement.Direction.BottomRight, bottomright);
            _movements.Add(Movement.Direction.Bottom, bottom);
            _movements.Add(Movement.Direction.BottomLeft, bottomleft);
            _movements.Add(Movement.Direction.Left, left);
            _movements.Add(Movement.Direction.TopLeft, topleft);

            CurrentPieceBase = currentChessPiece;
        }

        public ChessPieceBase CurrentPieceBase
        {
            get
            {
                return _currentChessPiece;
            }
            set
            {
                _currentChessPiece = value;
                Image = _currentChessPiece?.Texture;
            }
        }

        public ImageSource Image
        {
            get { return _image; }
            private set
            {
                if (Equals(_image, value)) return;
                _image = value;
                OnPropertyChanged(nameof(Image));
            }
        }

        public SolidColorBrush Bgc
        {
            get { return _bgc; }
            set
            {
                if (Equals(_bgc, value)) return;
                _bgc = value;
                OnPropertyChanged(nameof(Bgc));
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
