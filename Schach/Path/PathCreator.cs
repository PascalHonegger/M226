using System.Collections.Generic;
using Chess.BoardPieces.Cells;

namespace Chess.Path
{
    class PathCreator
    {
        private bool _isRecursive;
        private Path _movementList;

        public PathCreator()
        {
            _movementList = new Path();
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
            return _movementList;
        }
    }
}
