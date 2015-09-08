using System.Collections;
using System.Collections.Generic;
using Chess.BoardPieces.Cells;

namespace Chess.Path
{
    public class Path : IEnumerable<Movement.Direction>
    {
        private readonly List<Movement.Direction> _path;

        public Movement.Direction GetStep()
        {
            if (IsRecursive) return _path[0];
            var path = _path[0];
            _path.RemoveAt(0);
            return path;
        }

        public Path()
        {
            _path = new List<Movement.Direction>();
        }

        public void Add(Movement.Direction step)
        {
            _path.Add(step);
        }

        IEnumerator<Movement.Direction> IEnumerable<Movement.Direction>.GetEnumerator()
        {
            return _path.GetEnumerator();
        }

        public IEnumerator GetEnumerator()
        {
            return _path.GetEnumerator();
        }

        public bool IsRecursive { private get; set; }
    }
}