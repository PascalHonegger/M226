using System.Collections.Generic;
using Chess.BoardPieces.Cells;

namespace Chess.Path
{
    class PathCreator
    {
        private bool IsRecursive;
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
            IsRecursive = isRecursive;
            return this;
        }

        public ChessPieces.Path Build()
        {
            var path = new ChessPieces.Path(_movementList) {IsRecursive = IsRecursive};
            return path;
        }
    }
}
