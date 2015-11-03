using System;
using System.Collections;
using System.Collections.Generic;
using System.Linq;
using Chess.Cells;
using Chess.ChessPieces;

namespace Chess.Path
{
	/// <summary>
	/// The general DTO used for the Path-Find-Logic
	/// </summary>
	public class Path : IEnumerable<Movement.Direction>, ICloneable
	{
		private readonly List<Movement.Direction> _path;

		/// <summary>
		/// Standard Constrktor
		/// </summary>
		public Path()
		{
			_path = new List<Movement.Direction>();
		}

		/// <summary>
		/// A recursive 
		/// </summary>
		public bool IsRecursive { get; set; }

		/// <summary>
		/// The Cell which started the Pathfinding-Call
		/// </summary>
		public CellViewModel StartCell { get; set; }

		IEnumerator<Movement.Direction> IEnumerable<Movement.Direction>.GetEnumerator()
		{
			return _path.GetEnumerator();
		}

		/// <summary>
		/// Returns an enumerator that iterates through a collection.
		/// </summary>
		/// <returns>
		/// An <see cref="T:System.Collections.IEnumerator"/> object that can be used to iterate through the collection.
		/// </returns>
		/// <filterpriority>2</filterpriority>
		public IEnumerator GetEnumerator()
		{
			return _path.GetEnumerator();
		}

		/// <summary>
		/// Get the next Step and remove the current one if the Path isn't recursive
		/// </summary>
		/// <returns></returns>
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

		/// <summary>
		/// Serves as the default hash function. 
		/// </summary>
		/// <returns>
		/// A hash code for the current object.
		/// </returns>
		/// <filterpriority>2</filterpriority>
		public override int GetHashCode()
		{
			return _path.GetHashCode();
		}

		/// <summary>
		/// Creates a new object that is a copy of the current instance.
		/// </summary>
		/// <returns>
		/// A new object that is a copy of this instance.
		/// </returns>
		/// <filterpriority>2</filterpriority>
		public object Clone()
		{
			foreach (var direction in _path.Where(p => !p.Equals(Movement.Direction.Final)))
			{
				ChessPieceBase.PathFactory.AddToPath(direction);
			}

			ChessPieceBase.PathFactory.SetIsRecursive(IsRecursive);

			return ChessPieceBase.PathFactory.Create();
		}

		/// <summary>
		/// Casts the Clone() to a Path
		/// </summary>
		/// <returns></returns>
		public Path ClonePath()
		{
			return Clone() as Path;
		}
	}
}