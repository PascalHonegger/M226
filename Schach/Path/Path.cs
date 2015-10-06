using System.Collections;
using System.Collections.Generic;
using System.Linq;
using Chess.Cells;

namespace Chess.Path
{
	public class Path : IEnumerable<Movement.Direction>
	{
		private readonly List<Movement.Direction> _path;

		public Path()
		{
			_path = new List<Movement.Direction>();
		}

		public bool IsRecursive { get; set; }

		IEnumerator<Movement.Direction> IEnumerable<Movement.Direction>.GetEnumerator()
		{
			return _path.GetEnumerator();
		}

		public IEnumerator GetEnumerator()
		{
			return _path.GetEnumerator();
		}

		public Movement.Direction GetNextStep()
		{
			if (!IsRecursive)
			{
				_path.RemoveAt(0);
			}
			return _path[0];
		}

		public void Add(Movement.Direction step)
		{
			_path.Add(step);
		}

		public Movement.Direction GetStep()
		{
			return _path[0];
		}

		public Path Clone()
		{
			var factory = new PathFactory();

			foreach (var direction in _path.Where(p => !p.Equals(Movement.Direction.Final)))
			{
				factory.AddToPath(direction);
			}

			factory.SetIsRecursive(IsRecursive);

			return factory.Create();

			// return (Path)MemberwiseClone();
		}

		public override bool Equals(object obj)
		{
			var other = obj as Path;

			if (other == null)
			{
				return false;
			}

			if (other.IsRecursive != IsRecursive)
			{
				return false;
			}

			if (other.Count() != this.Count())
			{
				return false;
			}

			return true;
		}

		public override int GetHashCode()
		{
			return _path.GetHashCode();
		}
	}
}