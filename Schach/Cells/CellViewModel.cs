using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Linq;
using System.Runtime.CompilerServices;
using System.Windows.Media;
using System.Windows.Media.Imaging;
using Chess.Annotations;
using Chess.ChessPieces;

namespace Chess.Cells
{
    public class CellViewModel : INotifyPropertyChanged
    {
        private SolidColorBrush _bgc;
        private ChessPieceBase _currentChessPiece;
        private BitmapSource _image;
        private readonly Dictionary<Movement.Direction, CellViewModel> _movements = new Dictionary<Movement.Direction, CellViewModel>();
        private readonly Action<CellViewModel> _addToGraveyardAction;
        private static readonly SolidColorBrush Green = Brushes.Green;
        private static readonly SolidColorBrush Red = Brushes.Red;
        private static readonly SolidColorBrush Orange = Brushes.Orange;

        public CellViewModel(ChessPieceBase currentChessChessPiece, Action<CellViewModel> addToGraveyardAction)
        {
            _addToGraveyardAction = addToGraveyardAction;
            CurrentChessPiece = currentChessChessPiece;
        }

        public void CreateLink(CellViewModel top, CellViewModel topright, CellViewModel right, CellViewModel bottomright,
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
            _movements.Add(Movement.Direction.Final, null);
        }

        public CellViewModel(ChessPieceBase chessPiece)
        {
            CurrentChessPiece = chessPiece;
        }

        #region Coloring
        public void ColorizeMove(Path.Path path)
        {
            if (CurrentChessPiece != null) return;
            Bgc = Green;
            if (path.GetStep() != Movement.Direction.Final) _movements[path.GetNextStep()]?.ColorizeMove(path);
        }

        public void ColorizeEat(Path.Path path, bool isWhite)
        {
            if (CurrentChessPiece == null && path.GetStep() != Movement.Direction.Final)
            {
                _movements[path.GetNextStep()]?.ColorizeMove(path);
            }
            else if (CurrentChessPiece != null && (CurrentChessPiece.IsBlack() == isWhite || CurrentChessPiece.IsWhite() != isWhite))
            {
                Bgc = Orange;
            }
        }

        public void ColorizeJump(Path.Path path, bool isWhite)
        {
            if (path.GetStep() == Movement.Direction.Final)
            {
                if (CurrentChessPiece == null)
                {
                    Bgc = Green;
                }
                else if (CurrentChessPiece.IsBlack() == isWhite || CurrentChessPiece.IsWhite() != isWhite)
                {
                    Bgc = Orange;
                }
            }
            else
            {
                _movements[path.GetNextStep()]?.ColorizeJump(path, isWhite);
            }
        } 
        #endregion

        #region Movement
        public static bool MoveModel(CellViewModel startModel, CellViewModel endModel)
        {
            if (!startModel.FindPathTo(endModel)) return false;

            endModel.MoveToGraveyard();
            endModel.CurrentChessPiece = startModel.CurrentChessPiece;
            startModel.CurrentChessPiece = null;
            return true;
        }

        private bool FindPathTo(CellViewModel endModel)
        {
            if (CurrentChessPiece == null) return false;
            var pawn = CurrentChessPiece as Pawn;
            if (pawn != null)
            {
                return pawn.PathList.Any(path =>
                {
                    var canMoveTo = _movements[path.GetStep()]?.MoveTo(path, endModel);
                    return canMoveTo != null && (bool) canMoveTo;
                }) || pawn.EatList.Any(path =>
                {
                    var canEatTo = _movements[path.GetStep()]?.EatTo(path, endModel);
                    return canEatTo != null && (bool) canEatTo;
                });
            }
            if (CurrentChessPiece is Knight)
            {
                return CurrentChessPiece.PathList.Any(path =>
                {
                    var canJumpTo = _movements[path.GetStep()]?.JumpEatTo(path, endModel);
                    return canJumpTo != null && (bool) canJumpTo;
                });
            }
            return CurrentChessPiece != null && CurrentChessPiece.PathList.Any(path =>
            {
                var canMoveOrEat = _movements[path.GetStep()]?.MoveEatTo(path, endModel);
                return canMoveOrEat != null && (bool)canMoveOrEat;
            });
        }

        private bool MoveTo(Path.Path path, CellViewModel endModel)
        {
            if (CurrentChessPiece != null) return false;
            if (this == endModel) return true;
            return _movements[path.GetNextStep()] != null && _movements[path.GetStep()].MoveTo(path, endModel);
        }

        private bool EatTo(Path.Path path, CellViewModel endModel)
        {
            if (this == endModel && CurrentChessPiece != null) return true;
            if (CurrentChessPiece != null) return false;
            return _movements[path.GetNextStep()] != null && _movements[path.GetStep()].EatTo(path, endModel);
        }

        private bool MoveEatTo(Path.Path path, CellViewModel endModel)
        {
            if (this == endModel) return true;
            if (CurrentChessPiece != null) return false;
            return _movements[path.GetNextStep()] != null && _movements[path.GetStep()].MoveEatTo(path, endModel);
        }
        private bool JumpEatTo(Path.Path path, CellViewModel endModel)
        {
            if (this == endModel) return true;
            return _movements[path.GetNextStep()] != null && _movements[path.GetStep()].JumpEatTo(path, endModel);
        }

        #endregion

        public ChessPieceBase CurrentChessPiece
        {
            get
            {
                return _currentChessPiece;
            }
            set
            {
                _currentChessPiece = value;

                Image = _currentChessPiece?.Texture ?? Properties.Resources.blank.ToBitmapSource();
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

        public void MoveToGraveyard()
        {
            _addToGraveyardAction.Invoke(this);
            CurrentChessPiece = null;
        }

        public event PropertyChangedEventHandler PropertyChanged;

        [NotifyPropertyChangedInvocator]
        private void OnPropertyChanged([CallerMemberName] string propertyName = null)
        {
            PropertyChanged?.Invoke(this, new PropertyChangedEventArgs(propertyName));
        }
    }
}
