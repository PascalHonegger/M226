using System.Collections.Generic;
using Chess.BoardPieces.Cells;

namespace Chess.Path
{
    class PathCreator
    {
        private bool _isRecursive;
        private List<Movement.Direction> _movementList;

        public PathCreator()
        {
            _movementList = new List<Movement.Direction>();
        }

        public PathCreator AddToPath(Movement.Direction step)
        {
            _movementList.Add(step);
            return this;
        }

        public PathCreator SetIsRecursive(bool isRecursive)
        {
            _isRecursive = isRecursive;
            return this;
        }

        public Path Build()
        {
            var path = new Path(_movementList) {IsRecursive = _isRecursive};
            return path;
        }
    }
}
