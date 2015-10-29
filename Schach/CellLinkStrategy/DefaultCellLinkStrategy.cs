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

			/*A8.CreateLink(null, null, B8, B7, A7, null, null, null);
				B8.CreateLink(null, null, C8, C7, B7, A7, A8, null);
				C8.CreateLink(null, null, D8, D7, C7, B7, B8, null);
				D8.CreateLink(null, null, E8, E7, D7, C7, C8, null);
				E8.CreateLink(null, null, F8, F7, E7, D7, D8, null);
				F8.CreateLink(null, null, G8, G7, F7, E7, E8, null);
				G8.CreateLink(null, null, H8, H7, G7, F7, F8, null);
				H8.CreateLink(null, null, null, null, H7, G7, G8, null);

				A7.CreateLink(A8, B8, B7, B6, A6, null, null, null);
				B7.CreateLink(B8, C8, C7, C6, B6, A6, A7, A8);
				C7.CreateLink(C8, D8, D7, D6, C6, B6, B7, B8);
				D7.CreateLink(D8, E8, E7, E6, D6, C6, C7, C8);
				E7.CreateLink(E8, F8, F7, F6, E6, D6, D7, D8);
				F7.CreateLink(F8, G8, G7, G6, F6, E6, E7, E8);
				G7.CreateLink(G8, H8, H7, H6, G6, F6, F7, F8);
				H7.CreateLink(H8, null, null, null, H6, G6, G7, G8);

				A6.CreateLink(A7, B7, B6, B5, A5, null, null, null);
				B6.CreateLink(B7, C7, C6, C5, B5, A5, A6, A7);
				C6.CreateLink(C7, D7, D6, D5, C5, B5, B6, B7);
				D6.CreateLink(D7, E7, E6, E5, D5, C5, C6, C7);
				E6.CreateLink(E7, F7, F6, F5, E5, D5, D6, D7);
				F6.CreateLink(F7, G7, G6, G5, F5, E5, E6, E7);
				G6.CreateLink(G7, H7, H6, H5, G5, F5, F6, F7);
				H6.CreateLink(H7, null, null, null, H5, G5, G6, G7);

				A5.CreateLink(A6, B6, B5, B4, A4, null, null, null);
				B5.CreateLink(B6, C6, C5, C4, B4, A4, A5, A6);
				C5.CreateLink(C6, D6, D5, D4, C4, B4, B5, B6);
				D5.CreateLink(D6, E6, E5, E4, D4, C4, C5, C6);
				E5.CreateLink(E6, F6, F5, F4, E4, D4, D5, D6);
				F5.CreateLink(F6, G6, G5, G4, F4, E4, E5, E6);
				G5.CreateLink(G6, H6, H5, H4, G4, F4, F5, F6);
				H5.CreateLink(H6, null, null, null, H4, G4, G5, G6);

				A4.CreateLink(A5, B5, B4, B3, A3, null, null, null);
				B4.CreateLink(B5, C5, C4, C3, B3, A3, A4, A5);
				C4.CreateLink(C5, D5, D4, D3, C3, B3, B4, B5);
				D4.CreateLink(D5, E5, E4, E3, D3, C3, C4, C5);
				E4.CreateLink(E5, F5, F4, F3, E3, D3, D4, D5);
				F4.CreateLink(F5, G5, G4, G3, F3, E3, E4, E5);
				G4.CreateLink(G5, H5, H4, H3, G3, F3, F4, F5);
				H4.CreateLink(H5, null, null, null, H3, G3, G4, G5);

				A3.CreateLink(A4, B4, B3, B2, A2, null, null, null);
				B3.CreateLink(B4, C4, C3, C2, B2, A2, A3, A4);
				C3.CreateLink(C4, D4, D3, D2, C2, B2, B3, B4);
				D3.CreateLink(D4, E4, E3, E2, D2, C2, C3, C4);
				E3.CreateLink(E4, F4, F3, F2, E2, D2, D3, D4);
				F3.CreateLink(F4, G4, G3, G2, F2, E2, E3, E4);
				G3.CreateLink(G4, H4, H3, H2, G2, F2, F3, F4);
				H3.CreateLink(H4, null, null, null, H2, G2, G3, G4);

				A2.CreateLink(A3, B3, B2, B1, A1, null, null, null);
				B2.CreateLink(B3, C3, C2, C1, B1, A1, A2, A3);
				C2.CreateLink(C3, D3, D2, D1, C1, B1, B2, B3);
				D2.CreateLink(D3, E3, E2, E1, D1, C1, C2, C3);
				E2.CreateLink(E3, F3, F2, F1, E1, D1, D2, D3);
				F2.CreateLink(F3, G3, G2, G1, F1, E1, E2, E3);
				G2.CreateLink(G3, H3, H2, H1, G1, F1, F2, F3);
				H2.CreateLink(H3, null, null, null, H1, G1, G2, G3);

				A1.CreateLink(A2, B2, B1, null, null, null, null, null);
				B1.CreateLink(B2, C2, C1, null, null, null, A1, A2);
				C1.CreateLink(C2, D2, D1, null, null, null, B1, B2);
				D1.CreateLink(D2, E2, E1, null, null, null, C1, C2);
				E1.CreateLink(E2, F2, F1, null, null, null, D1, D2);
				F1.CreateLink(F2, G2, G1, null, null, null, E1, E2);
				G1.CreateLink(G2, H2, H1, null, null, null, F1, F2);
				H1.CreateLink(H2, null, null, null, null, null, G1, G2);*/
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