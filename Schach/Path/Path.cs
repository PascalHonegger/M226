using System.Collections;
using System.Collections.Generic;
using Chess.BoardPieces.Cells;

namespace Chess.Path
{
    public class Path : IEnumerable<Movement.Direction>
    {
        private readonly List<Movement.Direction> _path;

        private Movement.Direction Step
        {
            get
            {
                if (IsRecursive) return _path[0];
                var path = _path[0];
                _path.RemoveAt(0);
                return path;
            }
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
            // TODO Fix this!
            foreach (Movement.Direction direction in _path)
            {
                // Lets check for end of list (its bad code since we used arrays)
                if (_path == null) // this wont work is T is not a nullable type
                {
                    break;
                }

                // Return the current element and then on next function call 
                // resume from next element rather than starting all over again;
                yield return _path;
            }
        }

        public IEnumerator GetEnumerator()
        {
            return _path.GetEnumerator();
        }

        public bool IsRecursive { get; set; }
    }
}