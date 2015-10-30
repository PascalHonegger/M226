using System;
using System.Collections.Generic;
using System.Linq;
using Chess.Annotations;
using Chess.Cells;

namespace Chess.CellLinkStrategy
{
	/// <summary>
	/// Default CellLinkStrategy class. Throws an Exception, if the cellViewModel-Input is invalid
	/// </summary>
	internal class DefaultCellLinkStrategy : ICellLinkStrategy
	{
		private List<CellViewModel> _referenceCellList;

		public bool LinkCellViewModel([NotNull]CellViewModel cellViewModel, List<CellViewModel> referenceCellList)
		{
			_referenceCellList = referenceCellList;
			if (cellViewModel == null)
			{
				throw new NotImplementedException("CellViewModel can't be null!");
			}
			
			var newMovements = new Dictionary<Movement.Direction, CellViewModel>();

			foreach (Movement.Direction direction in Enum.GetValues(typeof (Movement.Direction)))
			{
				newMovements.Add(direction, GetCellViewModel(cellViewModel, direction));
			}

			cellViewModel.CreateLink(newMovements);

			return true;
		}

		private CellViewModel GetCellViewModel([NotNull]CellViewModel cellViewModel, Movement.Direction direction)
		{
			string name;

			switch (direction)
			{
				case Movement.Direction.Top:
					name = string.Concat((cellViewModel.Name.First()), (char)(cellViewModel.Name.Last() + 1));
                    break;
				case Movement.Direction.TopRight:
					name = string.Concat((char)(cellViewModel.Name.First() + 1), (char)(cellViewModel.Name.Last() + 1));
					break;
				case Movement.Direction.Right:
					name = string.Concat((char)(cellViewModel.Name.First() + 1), cellViewModel.Name.Last());
					break;
				case Movement.Direction.BottomRight:
					name = string.Concat((char)(cellViewModel.Name.First() + 1), (char)(cellViewModel.Name.Last() - 1));
					break;
				case Movement.Direction.Bottom:
					name = string.Concat(cellViewModel.Name.First(), (char)(cellViewModel.Name.Last() - 1));
					break;
				case Movement.Direction.BottomLeft:
					name = string.Concat((char)(cellViewModel.Name.First() - 1), (char)(cellViewModel.Name.Last() - 1));
					break;
				case Movement.Direction.Left:
					name = string.Concat((char)(cellViewModel.Name.First() - 1), (cellViewModel.Name.Last()));
					break;
				case Movement.Direction.TopLeft:
					name = string.Concat((char)(cellViewModel.Name.First() - 1), (char)(cellViewModel.Name.Last() + 1));
					break;
				case Movement.Direction.Final:
					return null;
				default:
					throw new ArgumentOutOfRangeException(nameof(direction), direction, null);
			}

			var letter = name.First();

			var number = name.Last();

			if (number > '8' || number < '1' || letter > 'H' || letter < 'A')
			{
				return null;
			}

			return _referenceCellList.First(c => Equals(c.Name, name)); ;
		}
	}
}