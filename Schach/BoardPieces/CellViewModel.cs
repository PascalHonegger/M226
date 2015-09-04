using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Drawing;
using System.Linq;
using System.Runtime.CompilerServices;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Controls;
using System.Windows.Media;
using Chess.Annotations;
using Chess.ChessPieces;
using Brush = System.Windows.Media.Brush;
using Brushes = System.Windows.Media.Brushes;
using Color = System.Drawing.Color;

namespace Chess.BoardPieces
{
    public class CellViewModel : INotifyPropertyChanged
    {
        private SolidColorBrush _bgc;
        private ChessPiece currentChessPiece;

        public CellViewModel()
        {
            Bgc = Brushes.Green;
        }
        public CellViewModel(ChessPiece currentChessPiece)
        {
            this.currentChessPiece = currentChessPiece;
            Bgc = Brushes.Green;
        }

        public SolidColorBrush Bgc
        {
            get { return _bgc; }
            set
            {
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
