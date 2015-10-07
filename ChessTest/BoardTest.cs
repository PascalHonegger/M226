using System.Linq;
using Chess;
using Chess.Cells;
using NUnit.Framework;

namespace ChessTest
{
	[TestFixture]
	public class BoardTest : TestBase
	{
		private Board _board;

		public override void DoSetUp()
		{
		}

		public override void DoTearDown()
		{
		}

		[Test]
		public void TestAllCellDictionairy()
		{
			
		}

		[Test]
		public void TestCellCreation()
		{
			_board = new Board(false);

			for (var number = 1; number <= 8; number++)
			{
				for (int character = 'A'; character <= 'H'; character++)
				{
					var propertyName = (char)character + number.ToString();

					var result = true;

					var cellUnderTest = (CellViewModel)GetType().GetProperty(propertyName).GetValue(this);

					foreach (var direction in cellUnderTest.Movements)
					{
						
					}

					_board.AllCells.Where(o => o.Name.Equals(tmpName));

					Assert.IsTrue(result);

					
				}
			}
		}

		[Test]
		public void TestCreateDefaultBoardTrue()
		{
			_board = new Board();
		}

		[Test]
		public void TestCreateDefaultBoardFalse()
		{
			_board = new Board(false);
		}
	}
}
