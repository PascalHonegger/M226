﻿using System.Collections.Generic;
using Chess;
using Chess.Cells;
using Chess.ChessPieces;
using Chess.Path;
using NUnit.Framework;

namespace ChessTest
{
	[TestFixture]
	public class PathTest : TestBase
	{
		[SetUp]
		public override void DoSetUp()
		{
			_board = new Board(false);
		}

		private Board _board;

		public override void DoTearDown()
		{
		}

		[Test]
		public void Bishop()
		{
			// Arrange
			_board.A3.CurrentChessPiece = new Bishop(true);
			_board.D4.CurrentChessPiece = new Bishop(false);
			_board.G6.CurrentChessPiece = new Bishop(true);
			_board.A6.CurrentChessPiece = new Bishop(false);

			// Act
			_board.CalculatePossibleSteps();

			// Assert
			Assert.IsTrue(CellViewModel.MoveModel(_board.A3, _board.D6));
			Assert.IsTrue(CellViewModel.MoveModel(_board.D4, _board.B6));
			Assert.IsTrue(CellViewModel.MoveModel(_board.G6, _board.E4));
			Assert.IsTrue(CellViewModel.MoveModel(_board.A6, _board.C4));
		}

		[Test]
		public void BlackPawn()
		{
			// Arrange
			_board.A6.CurrentChessPiece = new Pawn(false);
			_board.B6.CurrentChessPiece = new Pawn(false);
			_board.D6.CurrentChessPiece = new Pawn(false);
			_board.E6.CurrentChessPiece = new Pawn(false);
			_board.C5.CurrentChessPiece = new Pawn(true);
			_board.F5.CurrentChessPiece = new Pawn(true);

			//Act
			_board.CalculatePossibleSteps();

			// Assert
			Assert.IsTrue(CellViewModel.MoveModel(_board.A6, _board.A5));
			Assert.IsFalse(CellViewModel.MoveModel(_board.A5, _board.A3));
			Assert.IsTrue(CellViewModel.MoveModel(_board.B6, _board.B4));
			Assert.IsTrue(CellViewModel.MoveModel(_board.D6, _board.C5));
			Assert.IsTrue(CellViewModel.MoveModel(_board.E6, _board.F5));
		}

		[Test]
		public void Knight()
		{
			// Arrange
			_board.A6.CurrentChessPiece = new Knight(false);
			_board.H6.CurrentChessPiece = new Knight(false);
			_board.A3.CurrentChessPiece = new Knight(true);
			_board.H3.CurrentChessPiece = new Knight(true);
			_board.E3.CurrentChessPiece = new Knight(true);
			_board.E6.CurrentChessPiece = new Knight(true);
			_board.D3.CurrentChessPiece = new Knight(true);
			_board.D6.CurrentChessPiece = new Knight(true);

			// Act
			_board.CalculatePossibleSteps();

			// Assert
			Assert.IsTrue(CellViewModel.MoveModel(_board.A6, _board.B4));
			Assert.IsTrue(CellViewModel.MoveModel(_board.H6, _board.G4));
			Assert.IsTrue(CellViewModel.MoveModel(_board.A3, _board.B5));
			Assert.IsTrue(CellViewModel.MoveModel(_board.H3, _board.G5));
			Assert.IsTrue(CellViewModel.MoveModel(_board.E3, _board.C4));
			Assert.IsTrue(CellViewModel.MoveModel(_board.E6, _board.C5));
			Assert.IsTrue(CellViewModel.MoveModel(_board.D3, _board.F4));
			Assert.IsTrue(CellViewModel.MoveModel(_board.D6, _board.F5));
		}

		[Test]
		public void PathFactory()
		{
			// Arrange
			var correctPathList = new List<Movement.Direction>
			{
				Movement.Direction.Top,
				Movement.Direction.TopLeft,
				Movement.Direction.Final
			};

			// Act
			var createdPathList =
				new PathFactory().AddToPath(Movement.Direction.Top).AddToPath(Movement.Direction.TopLeft).Create(true);

			// Assert
			Assert.AreEqual(correctPathList, createdPathList,
				"The Path created via the PathCreator doesn't match the hand-made Path");
		}

		[Test]
		public void Queen()
		{
			// Arrange
			_board.A3.CurrentChessPiece = new Queen(false);
			_board.A4.CurrentChessPiece = new Queen(false);
			_board.A5.CurrentChessPiece = new Queen(false);
			_board.B6.CurrentChessPiece = new Queen(false);
			_board.G3.CurrentChessPiece = new Queen(true);
			_board.H4.CurrentChessPiece = new Queen(true);
			_board.H5.CurrentChessPiece = new Queen(true);
			_board.H6.CurrentChessPiece = new Queen(true);

			// Act
			_board.CalculatePossibleSteps();

			// Assert
			Assert.IsTrue(CellViewModel.MoveModel(_board.A3, _board.C5));
			Assert.IsTrue(CellViewModel.MoveModel(_board.A5, _board.C3));
			Assert.IsTrue(CellViewModel.MoveModel(_board.A4, _board.C4));
			Assert.IsTrue(CellViewModel.MoveModel(_board.B6, _board.B3));
			Assert.IsTrue(CellViewModel.MoveModel(_board.H4, _board.F6));
			Assert.IsTrue(CellViewModel.MoveModel(_board.H6, _board.F4));
			Assert.IsTrue(CellViewModel.MoveModel(_board.H5, _board.F5));
			Assert.IsTrue(CellViewModel.MoveModel(_board.G3, _board.G6));
		}

		[Test]
		public void Rook()
		{
			// Arrange
			_board.C5.CurrentChessPiece = new Rook(false);
			_board.D5.CurrentChessPiece = new Rook(false);
			_board.A3.CurrentChessPiece = new Rook(true);
			_board.G6.CurrentChessPiece = new Rook(true);

			// Act
			_board.CalculatePossibleSteps();

			// Assert
			Assert.IsTrue(CellViewModel.MoveModel(_board.C5, _board.A5));
			Assert.IsTrue(CellViewModel.MoveModel(_board.D5, _board.E5));
			Assert.IsTrue(CellViewModel.MoveModel(_board.A3, _board.A4));
			Assert.IsTrue(CellViewModel.MoveModel(_board.G6, _board.G3));
		}

		[Test]
		public void WhitePawn()
		{
			// Arrange
			_board.A3.CurrentChessPiece = new Pawn(true);
			_board.B3.CurrentChessPiece = new Pawn(true);
			_board.E5.CurrentChessPiece = new Pawn(true);
			_board.G4.CurrentChessPiece = new Pawn(true);
			_board.D6.CurrentChessPiece = new Pawn(false);
			_board.H5.CurrentChessPiece = new Pawn(false);

			//Act
			_board.CalculatePossibleSteps();

			//Assert
			Assert.IsTrue(CellViewModel.MoveModel(_board.A3, _board.A4));
			Assert.IsFalse(CellViewModel.MoveModel(_board.A4, _board.A6));
			Assert.IsTrue(CellViewModel.MoveModel(_board.B3, _board.B5));
			Assert.IsTrue(CellViewModel.MoveModel(_board.E5, _board.D6));
			Assert.IsTrue(CellViewModel.MoveModel(_board.G4, _board.H5));
		}
	}
}