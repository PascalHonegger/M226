using System;
using Chess;
using Chess.Cells;
using Chess.ChessPieces;
using NUnit.Framework;

namespace ChessTest
{
    [TestFixture]
    public class CellViewModelTest
    {
        private Board _board;

        [Test]
        public void TestEat()
        {
            // Arrange
            _board = new Board
            {
                D4 = { CurrentChessPiece = new Bishop(false) },
                F6 = { CurrentChessPiece = new Bishop(true) }
            };
            var chessPieceToBeEaten = _board.F6.CurrentChessPiece;
            var evilChessPiece = _board.D4.CurrentChessPiece;
            // Act
            TestCellViewModelMovement.TestMoveEat(_board.D4, _board.F6);

            // Assert
            Assert.IsNull(_board.D4.CurrentChessPiece);
            Assert.AreEqual(_board.F6.CurrentChessPiece, evilChessPiece);
            Assert.True(_board.GraveYard.Contains(chessPieceToBeEaten));
        }

        [Test]
        public void TestMove()
        {
            // Arrange
            _board = new Board
            {
                D4 = { CurrentChessPiece = new Bishop(false) },
                E5 = { CurrentChessPiece = new Pawn(false) },
                F6 = { CurrentChessPiece = new Bishop(true) }
            };
            var chessPieceToBeEaten = _board.F6.CurrentChessPiece;
            var evilChessPiece = _board.D4.CurrentChessPiece;
            // Act
            TestCellViewModelMovement.TestMoveEat(_board.D4, _board.F6);

            // Assert
            Assert.IsNotNull(_board.E5.CurrentChessPiece);
            Assert.AreEqual(_board.F6.CurrentChessPiece, chessPieceToBeEaten);
            Assert.AreEqual(_board.D4.CurrentChessPiece, evilChessPiece);
            Assert.AreEqual(_board.GraveYard.Count, 0);
        }

        [Test]
        public void TestPathFinding()
        {
            // Arrange
            _board = new Board
            {
                D4 = { CurrentChessPiece = new Bishop(false) },
                E5 = { CurrentChessPiece = new Pawn(false) },
                F6 = { CurrentChessPiece = new Bishop(true) }
            };
            var chessPieceToBeEaten = _board.F6.CurrentChessPiece;
            var evilChessPiece = _board.D4.CurrentChessPiece;
            // Act
            TestCellViewModelMovement.TestMoveEat(_board.D4, _board.F6);

            // Assert
            Assert.IsNotNull(_board.E5.CurrentChessPiece);
            Assert.AreEqual(_board.F6.CurrentChessPiece, chessPieceToBeEaten);
            Assert.AreEqual(_board.D4.CurrentChessPiece, evilChessPiece);
            Assert.AreEqual(_board.GraveYard.Count, 0);
        }
    }
}
