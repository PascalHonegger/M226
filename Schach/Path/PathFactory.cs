using Chess.Cells;

namespace Chess.Path
{
	public class PathFactory
	{
		private Path _movementList;

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

		public PathFactory AddStartCell(CellViewModel startCell)
		{
			_movementList.StartCell = startCell;
			return this;
		}

		public Path Create(bool isWhite)
		{
			if (!_movementList.IsRecursive)
			{
				_movementList.Add(Movement.Direction.Final);
			}

			_movementList.IsWhite = isWhite;

			var result = _movementList;

			_movementList = new Path();

			return result;
		}
	}
}