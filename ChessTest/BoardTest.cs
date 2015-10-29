using System;
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

		[SetUp]
		public void OnSetup()
		{
			_board = new Board
			{
				ComputerIsEnabled = false
			};
		}

		[Test]
		public async void TestAllCellDictionairy()
		{
			Assert.That(_board.AllCells, Is.Empty);

			await _board.CreateValues();

			Assert.That(_board.AllCells.Count, Is.EqualTo(64));
		}

		[Test]
		public async void TestCellCreation()
		{
			await _board.CreateValues(false);

			for (var number = 1; number <= 8; number++)
			{
				for (int character = 'A'; character <= 'H'; character++)
				{
					var cellUnderTest = (CellViewModel) _board.GetType().GetProperty((char)character + number.ToString()).GetValue(_board);

					foreach (var kvp in cellUnderTest.Movements.Where(kvp => kvp.Value != null))
					{
						var rightPropertyName = "";
						switch (kvp.Key)
						{
							case Movement.Direction.Top:
								rightPropertyName = (char)character + (number + 1).ToString();
								break;
							case Movement.Direction.TopLeft:
								rightPropertyName = (char)(character-1) + (number+1).ToString();
								break;
							case Movement.Direction.Left:
								rightPropertyName = (char)(character-1) + number.ToString();
								break;
							case Movement.Direction.BottomLeft:
								rightPropertyName = (char)(character-1) + (number-1).ToString();
								break;
							case Movement.Direction.Bottom:
								rightPropertyName = (char)character + (number-1).ToString();
								break;
							case Movement.Direction.BottomRight:
								rightPropertyName = (char)(character+1) + (number-1).ToString();
								break;
							case Movement.Direction.Right:
								rightPropertyName = (char)(character+1) + number.ToString();
								break;
							case Movement.Direction.TopRight:
								rightPropertyName = (char)(character+1) + (number+1).ToString();
								break;
							case Movement.Direction.Final:
								break;
							default:
								throw new ArgumentOutOfRangeException();
						}
						var tmp = kvp.Value.Name.Equals(rightPropertyName) || string.IsNullOrEmpty(kvp.Value.Name);

						Assert.That(tmp);
					}
				}
			}
		}

		[Test]
		public async void TestCreateDefaultBoardFalse()
		{
			await _board.CreateValues(false);

			Assert.That(_board.AllCells.Count, Is.EqualTo(64));

			foreach (var cell in _board.AllCells)
			{
				Assert.That(cell.CurrentChessPiece, Is.Null);
			}
		}

		[Test]
		public async void TestCreateDefaultBoardTrue()
		{
			//TODO Finish Test
			await _board.CreateValues();

			Assert.That(_board.AllCells.Count, Is.EqualTo(64));
			foreach (var cell in _board.AllCells.Where(cell => !cell.Name.Substring(1).Equals("1") && !cell.Name.Substring(1).Equals("2") && !cell.Name.Substring(1).Equals("7") && !cell.Name.Substring(1).Equals("8")))
			{
				Assert.That(cell.CurrentChessPiece, Is.Null);
			}
		}

		[Test]
		public async void NextTurnChangesTurn()
		{
			await _board.CreateValues();
			await _board.StartRound();
			// White starts
			Assert.That(_board.WhiteTurn, Is.True);
			await _board.NextTurn();
			Assert.That(_board.WhiteTurn, Is.False);
			Assert.That(_board.AllPossibleSteps.Any());
		}

		[Test]
		public async void ClickChangesSelectionAndColorizesPaths()
		{
			await _board.CreateValues();
			await _board.StartRound();
			//await _board.CellViewModelOnMouseDown(_mouseButtonMock.Object, _board.AllCells.First());
			
		}

		[Test]
		public async void ClickMovesModel()
		{
			
		}

		[Test]
		public async void RightClickMarksColor()
		{

		}
	}
}