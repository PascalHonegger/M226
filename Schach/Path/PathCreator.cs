using System.Collections.Generic;
using Chess.BoardPieces.Cells;

namespace Chess.Path
{
    class PathCreator
    {
        private bool IsRecursive;
        private List<Movement.Direction> movementList;
        public PathCreator AddToPath(Movement.Direction step)
        {
            movementList.Add(step);
            return this;
        }

        public PathCreator SetIsRecursive(bool isRecursive)
        {
            IsRecursive = isRecursive;
            return this;
        }

        public ChessPieces.Path Build()
        {
            var path = new ChessPieces.Path(movementList) {IsRecursive = IsRecursive};
            return path;
        }
    }
}
