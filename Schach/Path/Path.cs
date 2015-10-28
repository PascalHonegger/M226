using System;
using System.Collections;
using System.Collections.Generic;
using System.Linq;
using Chess.Cells;
using Chess.ChessPieces;

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

			if (_path.Where((t, i) => !t.Equals(other._path[i])).Any())
			{
				return false;
			}

			return other.Count() == this.Count();
		}

		public override int GetHashCode()
		{
			return _path.GetHashCode();
		}

		public object Clone()
		{
			foreach (var direction in _path.Where(p => !p.Equals(Movement.Direction.Final)))
			{
				ChessPieceBase.PathFactory.AddToPath(direction);
			}

			ChessPieceBase.PathFactory.SetIsRecursive(IsRecursive);

			return ChessPieceBase.PathFactory.Create(IsWhite);
		}

		public Path ClonePath()
		{
			return Clone() as Path;
		}
	}
}