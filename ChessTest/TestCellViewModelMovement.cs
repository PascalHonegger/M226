using System.Linq;
using System.Runtime.InteropServices;
using Chess;
using Chess.Cells;
using Chess.ChessPieces;
using NUnit.Framework;

namespace ChessTest
{
    [TestFixture]
    public class TestCellViewModelMovement
    {
        private Board _board;

        [Test]
        public void Bishop()
        {
            // Arrange
            _board = new Board
            {
                A3 = { CurrentChessPiece = new Bishop(false)},
                D4 = {CurrentChessPiece = new Bishop(false)},
                G6 = {CurrentChessPiece = new Bishop(true)},
                A6 = { CurrentChessPiece = new Bishop(true)}
            };
            var a3 = _board.A3.CurrentChessPiece;
            var d4 = _board.D4.CurrentChessPiece;
            var g6 = _board.G6.CurrentChessPiece;
            var a6 = _board.A6.CurrentChessPiece;
            
            // Act
            TestEat(_board.A3, _board.D6);
            TestEat(_board.D4, _board.B6);
            TestEat(_board.G6, _board.E4);
            TestEat(_board.A6, _board.C4);

            // Assert
            Assert.IsNull(_board.A3.CurrentChessPiece);
            Assert.IsNull(_board.D4.CurrentChessPiece);
            Assert.IsNull(_board.G6.CurrentChessPiece);
            Assert.IsNull(_board.A6.CurrentChessPiece);
            Assert.AreEqual(_board.D6.CurrentChessPiece, a3);
            Assert.AreEqual(_board.B6.CurrentChessPiece, d4);
            Assert.AreEqual(_board.E4.CurrentChessPiece, g6);
            Assert.AreEqual(_board.C4.CurrentChessPiece, a6);
        }

        [Test]
        public void Knight()
        {
            // Arrange
            _board = new Board
            {
                F4 = { CurrentChessPiece = new Knight(false) },
                E4 = { CurrentChessPiece = new Knight(false) },
                A4 = { CurrentChessPiece = new Knight(true) },
                D4 = { CurrentChessPiece = new Knight(true) }
            };

            var f4 = _board.F4.CurrentChessPiece;
            var e4 = _board.E4.CurrentChessPiece;
            var a4 = _board.A4.CurrentChessPiece;
            var d4 = _board.D4.CurrentChessPiece;

            // Act
            TestEat(_board.F4, _board.D5);
            TestEat(_board.E4, _board.C5);
            TestEat(_board.A4, _board.C3);
            TestEat(_board.D4, _board.F5);

            // Assert
            Assert.IsNull(_board.F4.CurrentChessPiece);
            Assert.IsNull(_board.E4.CurrentChessPiece);
            Assert.IsNull(_board.A4.CurrentChessPiece);
            Assert.IsNull(_board.D4.CurrentChessPiece);
            Assert.AreEqual(_board.D5.CurrentChessPiece, f4);
            Assert.AreEqual(_board.C5.CurrentChessPiece, e4);
            Assert.AreEqual(_board.C3.CurrentChessPiece, a4);
            Assert.AreEqual(_board.F5.CurrentChessPiece, d4);
        }

        public static void TestEat(CellViewModel cellToEat, CellViewModel cellToBeEaten)
        {
            CellViewModel.MoveModel(cellToEat, cellToBeEaten);
        }
    }
}
