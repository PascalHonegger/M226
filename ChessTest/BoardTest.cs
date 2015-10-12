using System;
using System.Collections.Generic;
using System.Linq;
using Chess;
using Chess.Cells;
using NUnit.Framework;

namespace ChessTest
{
	[TestFixture]
	public class BoardTest : TestBase
	{
		private IBoard _board;

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
		public async void TestCellCreation()
		{
			_board = new Board();
			await _board.CreateValues(false);

			for (var number = 1; number <= 8; number++)
			{
				for (int character = 'A'; character <= 'H'; character++)
				{
					var cellUnderTest = (CellViewModel) _board.GetType().GetProperty((char) character + number.ToString()).GetValue(_board);

					foreach (var kvp in cellUnderTest.Movements.Where(kvp => kvp.Value != null))
					{
						var rightPropertyName = "";
						switch (kvp.Key)
						{
							case Movement.Direction.Top:
								rightPropertyName = (char) character + number++.ToString();
								break;
							case Movement.Direction.TopLeft:
								rightPropertyName = (char) character-- + number++.ToString();
								break;
							case Movement.Direction.Left:
								rightPropertyName = (char)character++ + number.ToString();
								break;
							case Movement.Direction.BottomLeft:
								rightPropertyName = (char)character-- + number--.ToString();
								break;
							case Movement.Direction.Bottom:
								rightPropertyName = (char)character + number--.ToString();
								break;
							case Movement.Direction.BottomRight:
								rightPropertyName = (char)character++ + number--.ToString();
								break;
							case Movement.Direction.Right:
								rightPropertyName = (char)character++ + number.ToString();
								break;
							case Movement.Direction.TopRight:
								rightPropertyName = (char)character++ + number++.ToString();
								break;
							case Movement.Direction.Final:
								break;
							default:
								throw new ArgumentOutOfRangeException();
						}
						Assert.That(kvp.Value.Name, Is.EqualTo(rightPropertyName));
					}
				}
			}
		}

		[Test]
		public async void TestCreateDefaultBoardFalse()
		{
			_board = new Board();
			await _board.CreateValues(false);
		}

		[Test]
		public async void TestCreateDefaultBoardTrue()
		{
			_board = new Board();
			await _board.CreateValues();
		}
	}
}