using System;
using System.Collections;
using System.Collections.Generic;
using Chess.BoardPieces.Cells;

namespace Chess.ChessPieces
{
    public class Path : IEnumerable
    {
        private readonly List<Movement.Direction> _path;

        public Path(List<Movement.Direction> path)
        {
            _path = path;
        }

        public IEnumerator GetEnumerator()
        {
            return _path.GetEnumerator();
        }

        public bool IsRecursive { get; set; }

    }
}