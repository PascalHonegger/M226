using System.Collections;
using System.Collections.Generic;
using Chess.BoardPieces.Cells;
using Chess.Cells;

namespace Chess.Path
{
    public class Path : IEnumerable<Movement.Direction>
    {
        private readonly List<Movement.Direction> _path;

        public Movement.Direction GetStep()
        {
            return _path[0];
        }

        public Movement.Direction GetNextStep()
        {
            if (!IsRecursive) _path.RemoveAt(0);
            return _path[0];
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

        public bool IsRecursive { get; set; }
    }
}