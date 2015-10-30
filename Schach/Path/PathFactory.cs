using Chess.Cells;

namespace Chess.Path
{
	/// <summary>
	/// The Factory used to create an instanze of the Path
	/// </summary>
	public class PathFactory
	{
		private Path _movementList;

		/// <summary>
		/// Standard Constructor
		/// </summary>
		public PathFactory()
		{
			_movementList = new Path();
		}

		/// <summary>
		/// 
		/// </summary>
		/// <param name="step"></param>
		/// <returns>this</returns>
		public PathFactory AddToPath(Movement.Direction step)
		{
			_movementList.Add(step);
			return this;
		}

		/// <summary>
		/// Sets the Recursivness of the Path. Recursive means it will go on until it reaches a wall
		/// </summary>
		/// <param name="isRecursive"></param>
		/// <returns>this</returns>
		public PathFactory SetIsRecursive(bool isRecursive)
		{
			_movementList.IsRecursive = isRecursive;
			return this;
		}

		/// <summary>
		/// Creates the Path
		/// </summary>
		/// 
		/// <returns>this</returns>
		public Path Create()
		{
			if (!_movementList.IsRecursive)
			{
				_movementList.Add(Movement.Direction.Final);
			}

			var result = _movementList;

			_movementList = new Path();

			return result;
		}
	}
}