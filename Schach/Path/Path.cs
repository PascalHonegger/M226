using System.Collections;
using System.Collections.Generic;
using Chess.BoardPieces.Cells;

namespace Chess.Path
{
    public class Path : IEnumerable
    {
        private readonly List<Movement.Direction> _path;

        public Path(List<Movement.Direction> path)
        {
            _path = path;
        }

        public void StepTaken()
        {
            _path.RemoveAt(0);
        }

        public List<Movement.Direction> Get()
        {
            return _path;
        }

        public IEnumerator GetEnumerator()
        {
            return _path.GetEnumerator();
        }

        public bool IsRecursive { get; set; }

    }
}