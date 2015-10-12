using System;
using System.Collections;
using System.Collections.Generic;
using System.Linq;
using Chess.Cells;

namespace Chess.Path
{
	public class Path : IEnumerable<Movement.Direction>, ICloneable
	{
		private readonly List<Movement.Direction> _path;

		public Path()
		{
			_path = new List<Movement.Direction>();
		}

		public bool IsRecursive { get; set; }

		public bool IsWhite { get; set; }

		public CellViewModel StartCell { get; set; }

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

		public override bool Equals(object obj)
		{
			var other = obj as Path;

			if (other?.IsRecursive != IsRecursive || !Equals(other.StartCell, StartCell))
			{
				return false;
			}

			for (var i = 0; i < _path.Count; i++)
			{
				if (!_path[i].Equals(other._path[i]))
				{
					return false;
				}
			}

			return other.Count() == this.Count();
		}

		public override int GetHashCode()
		{
			return _path.GetHashCode();
		}

		public object Clone()
		{
			var factory = new PathFactory();

			foreach (var direction in _path.Where(p => !p.Equals(Movement.Direction.Final)))
			{
				factory.AddToPath(direction);
			}

			factory.SetIsRecursive(IsRecursive);

			return factory.Create(IsWhite);
		}

		public Path ClonePath()
		{
			return Clone() as Path;
		}
	}
}