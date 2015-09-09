using Chess.BoardPieces.Cells;

namespace Chess.Path
{
    class PathFactory
    {
        private readonly Path _movementList;

        public PathFactory()
        {
            _movementList = new Path();
        }

        public PathFactory AddToPath(Movement.Direction step)
        {
            _movementList.Add(step);
            return this;
        }

        public PathFactory SetIsRecursive(bool isRecursive)
        {
            _movementList.IsRecursive = isRecursive;
            return this;
        }

        public Path Create()
        {
            if (!_movementList.IsRecursive) _movementList.Add(Movement.Direction.None);
            return _movementList;
        }
    }
}
