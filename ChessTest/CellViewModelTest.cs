﻿using System.Collections.Generic;
using System.Linq;
using Chess;
using Chess.Cells;
using Chess.ChessPieces;
using Chess.Path;
using Moq;
using NUnit.Framework;

namespace ChessTest
{
	[TestFixture]
	public class CellViewModelTest : TestBase
	{
		private Board _board;
		private Mock<ChessPieceBase> _whiteChessPieceMock;
		private Mock<ChessPieceBase> _blackChessPieceMock;

		public override void DoSetUp()
		{
			_blackChessPieceMock = new Mock<ChessPieceBase>();
			_blackChessPieceMock
				.Setup(mock => mock.IsWhite())
				.Returns(false);
			_blackChessPieceMock
				.Setup(mock => mock.IsBlack())
				.Returns(true);

			_whiteChessPieceMock = new Mock<ChessPieceBase>();
			_whiteChessPieceMock
				.Setup(mock => mock.IsWhite())
				.Returns(true);
			_whiteChessPieceMock
				.Setup(mock => mock.IsBlack())
				.Returns(false);
		}

		public override void DoTearDown()
		{
		}

		private static void CompareBoards(IBoard board1, IBoard board2)
		{
			Assert.That(board1.GraveYard, Is.EqualTo(board2.GraveYard));


			foreach (var keyvalue1 in board1.AllCells)
			{
				foreach (var keyvalue2 in board2.AllCells.Where(keyvalue2 => keyvalue1 == keyvalue2))
				{
					Assert.That(keyvalue1, Is.EqualTo(keyvalue2), "Property {0} war nicht gleich!", keyvalue1.Name);
				}
			}
		}

		[Test]
		public void EatImpossible()
		{
			// Arrange
			var pathList = new List<Path>
			{
				new PathFactory().AddToPath(Movement.Direction.Top).SetIsRecursive(false).Create(true),
				new PathFactory().AddToPath(Movement.Direction.Bottom).SetIsRecursive(true).Create(true),
				new PathFactory().AddToPath(Movement.Direction.Left).SetIsRecursive(true).Create(true)
			};

			var chessPieceMock = new Mock<ChessPieceBase>();
			chessPieceMock
				.Setup(mock => mock.IsWhite())
				.Returns(true);
			chessPieceMock
				.Setup(mock => mock.PathList)
				.Returns(pathList);

			_board = new Board(false)
			{
				D5 = {CurrentChessPiece = chessPieceMock.Object},
				D4 = {CurrentChessPiece = _blackChessPieceMock.Object},
				D3 = {CurrentChessPiece = _blackChessPieceMock.Object},
				B5 = {CurrentChessPiece = _blackChessPieceMock.Object},
				B3 = {CurrentChessPiece = _blackChessPieceMock.Object}
			};
			var d5 = _board.D5.CurrentChessPiece;
			var d4 = _board.D4.CurrentChessPiece;
			var d3 = _board.D3.CurrentChessPiece;
			var b5 = _board.B5.CurrentChessPiece;
			var b3 = _board.B3.CurrentChessPiece;

			// Act
			_board.CalculatePossibleSteps();
			CellViewModel.MoveModel(_board.D5, _board.D3);
			CellViewModel.MoveModel(_board.D5, _board.D3);
			CellViewModel.MoveModel(_board.D5, _board.B5);

			// Assert
			Assert.IsNull(_board.D5.CurrentChessPiece,
				"There is still a ChessPiece on the CellViewModel D5, which should have moved away");
			Assert.AreEqual(_board.D4.CurrentChessPiece, d4, "ChessPiece 'd4' shouldn't be removed");
			Assert.AreEqual(_board.D3.CurrentChessPiece, d3, "ChessPiece 'd3' shouldn't be removed");
			Assert.AreEqual(_board.B3.CurrentChessPiece, b3, "ChessPiece 'b3' did get eaten, but there was no valid Path");
			Assert.AreEqual(_board.B5.CurrentChessPiece, d5, "ChessPiece 'd5' didn't move to the CellViewModel 'B5'");
			Assert.AreEqual(_board.GraveYard.Count, 1, "There where more ChessPieces in the Graveyard than expected");
			Assert.True(_board.GraveYard.Contains(b5), "ChessPiece 'b5' wasn't in the Graveyard, but should have been there");
		}

		[Test]
		public void EatPossible()
		{
			// Arrange
			var pathList = new List<Path>
			{
				new PathFactory().AddToPath(Movement.Direction.Top).SetIsRecursive(false).Create(true),
				new PathFactory().AddToPath(Movement.Direction.Bottom).SetIsRecursive(false).Create(true)
			};

			var unitUnderTest = new Mock<ChessPieceBase>();
			unitUnderTest
				.Setup(mock => mock.IsWhite())
				.Returns(true);
			unitUnderTest
				.Setup(mock => mock.IsBlack())
				.Returns(false);
			unitUnderTest
				.Setup(mock => mock.PathList)
				.Returns(pathList);

			_board = new Board(false)
			{
				D4 = {CurrentChessPiece = unitUnderTest.Object},
				D3 = {CurrentChessPiece = _blackChessPieceMock.Object}
			};
			var d3 = _board.D3.CurrentChessPiece;
			var d4 = _board.D4.CurrentChessPiece;

			// Act
			_board.CalculatePossibleSteps();
			CellViewModel.MoveModel(_board.D4, _board.D3);

			// Assert
			Assert.IsNull(_board.D4.CurrentChessPiece,
				"There is still a ChessPiece on the CellViewModel D4, which should have moved away");
			Assert.AreEqual(_board.D3.CurrentChessPiece, d4, "ChessPiece 'd4' didn't move to his new Location properly");
			Assert.True(_board.GraveYard.Contains(d3), "ChessPiece 'toBeEaten' didn't move to the Graveyard properly");
		}

		[Test]
		public void MoveImpossible()
		{
			var pathList = new List<Path>
			{
				new PathFactory().AddToPath(Movement.Direction.Bottom).SetIsRecursive(true).Create(true),
				new PathFactory().AddToPath(Movement.Direction.Left).SetIsRecursive(true).Create(true)
			};

			var unitUnderTest = new Mock<ChessPieceBase>();
			unitUnderTest
				.Setup(mock => mock.IsWhite())
				.Returns(true);
			unitUnderTest
				.Setup(mock => mock.IsBlack())
				.Returns(false);
			unitUnderTest
				.Setup(mock => mock.PathList)
				.Returns(pathList);

			// Arrange
			_board = new Board(false)
			{
				D5 = {CurrentChessPiece = unitUnderTest.Object},
				D4 = {CurrentChessPiece = unitUnderTest.Object}
			};
			var d5 = _board.D5.CurrentChessPiece;
			var d4 = _board.D4.CurrentChessPiece;

			// Act
			_board.CalculatePossibleSteps();
			CellViewModel.MoveModel(_board.D5, _board.D3);
			CellViewModel.MoveModel(_board.D5, _board.B3);
			CellViewModel.MoveModel(_board.D5, _board.B5);

			// Assert
			Assert.IsNull(_board.D5.CurrentChessPiece,
				"There is still a ChessPiece on the CellViewModel D5, which should have moved away");
			Assert.AreEqual(_board.D4.CurrentChessPiece, d4, "ChessPiece 'd4' shouldn't be removed");
			Assert.IsNull(_board.D3.CurrentChessPiece,
				"ChessPiece 'd5' did move to the CellViewModel 'D3', but there was no valid Path");
			Assert.IsNull(_board.B3.CurrentChessPiece,
				"ChessPiece 'd5' did move to the CellViewModel 'B3', but there was no valid Path");
			Assert.AreEqual(_board.B5.CurrentChessPiece, d5, "ChessPiece 'd5' didn't move to the CellViewModel 'B5'");
			Assert.AreEqual(_board.GraveYard.Count, 0, "There where more ChessPieces in the Graveyard than expected");
		}

		[Test]
		public void MovePossible()
		{
			var pathList = new List<Path>
			{
				new PathFactory().AddToPath(Movement.Direction.Top).SetIsRecursive(false).Create(true),
				new PathFactory().AddToPath(Movement.Direction.Bottom).SetIsRecursive(false).Create(true),
				new PathFactory().AddToPath(Movement.Direction.Right)
					.AddToPath(Movement.Direction.Right)
					.AddToPath(Movement.Direction.Right)
					.AddToPath(Movement.Direction.Right)
					.AddToPath(Movement.Direction.Top)
					.SetIsRecursive(false)
					.Create(true)
			};

			var chessPieceMock = new Mock<ChessPieceBase>();
			chessPieceMock
				.Setup(mock => mock.IsWhite())
				.Returns(true);
			chessPieceMock
				.Setup(mock => mock.PathList)
				.Returns(pathList);

			// Arrange
			_board = new Board(false)
			{
				D4 = {CurrentChessPiece = chessPieceMock.Object}
			};
			var d4 = _board.D4.CurrentChessPiece;

			// Act
			_board.CalculatePossibleSteps();
			CellViewModel.MoveModel(_board.D4, _board.H5);

			// Assert
			Assert.IsNull(_board.D4.CurrentChessPiece,
				"There is still a ChessPiece on the CellViewModel D4, which should have moved away");
			Assert.AreEqual(_board.H5.CurrentChessPiece, d4, "ChessPiece 'd4' didn't move to his new Location properly");
		}

		[Test]
		public void TestPathFinding()
		{
			//TODO this

			// Arrange


			// Act


			// Assert

			Assert.IsTrue(true);
		}
	}
}