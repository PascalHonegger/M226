using System.Collections.Generic;
using System.ComponentModel;
using System.Runtime.CompilerServices;
using System.Windows.Media;
using System.Windows.Media.Imaging;
using Chess.Annotations;
using Chess.Cells;
using Chess.ChessPieces;

namespace Chess.BoardPieces.Cells
{
    public sealed class CellViewModel : INotifyPropertyChanged
    {
        private SolidColorBrush _bgc;
        private ChessPieceBase _currentChessChessPiece;
        private BitmapSource _image;
        private readonly Dictionary<Movement.Direction, CellViewModel> _movements = new Dictionary<Movement.Direction, CellViewModel>();

        public CellViewModel(ChessPieceBase currentChessChessPiece, CellViewModel top, CellViewModel topright, CellViewModel right, CellViewModel bottomright,
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

            CurrentChessPiece = currentChessChessPiece;
        }

        public void Move(Path.Path path, bool isWhite)
        {
            if (CurrentChessPiece == null)
            {
                Bgc = Brushes.Green;
            }
            else if (CurrentChessPiece.IsBlack() == isWhite || CurrentChessPiece.IsWhite() != isWhite)
            {
                Bgc = Brushes.Orange;
            }
            if(path.GetStep() != Movement.Direction.None) _movements[path.GetNextStep()].Move(path, isWhite);
        }

        public void Jump(Path.Path path, bool isWhite)
        {
            if (path.GetStep() == Movement.Direction.None)
            {
                if (CurrentChessPiece == null)
                {
                    Bgc = Brushes.Green;
                }
                else if (CurrentChessPiece.IsBlack() == isWhite || CurrentChessPiece.IsWhite() != isWhite)
                {
                    Bgc = Brushes.Orange;
                }
            }
            else
            {
                _movements[path.GetNextStep()].Jump(path, isWhite);
            }
        }

        public void Move(Path.Path path, bool isWhite, CellViewModel modelToMoveHere)
        {
            if (CurrentChessPiece != null) return;

            if (path.GetStep() == Movement.Direction.None || path.IsRecursive)
            {
                CurrentChessPiece = modelToMoveHere.CurrentChessPiece;
                modelToMoveHere.CurrentChessPiece = null;
            }
            else
            {
                _movements[path.GetNextStep()].Move(path, isWhite, modelToMoveHere);
            }
        }

        public void Jump(Path.Path path, bool isWhite, CellViewModel modelToMoveHere)
        {
            if (CurrentChessPiece.IsWhite() == isWhite || CurrentChessPiece.IsBlack() != isWhite)
            {
                return;
            }
            if (path.GetStep() != Movement.Direction.None)
            {
                _movements[path.GetNextStep()].Move(path, isWhite, modelToMoveHere);
            }
            else
            {
                CurrentChessPiece = modelToMoveHere.CurrentChessPiece;
                modelToMoveHere.CurrentChessPiece = null;
            }
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

                Image = _currentChessChessPiece?.Texture;
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
        private void OnPropertyChanged([CallerMemberName] string propertyName = null)
        {
            PropertyChanged?.Invoke(this, new PropertyChangedEventArgs(propertyName));
        }
    }
}
