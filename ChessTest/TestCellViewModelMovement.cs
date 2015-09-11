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
            TestMoveEat(_board.A3, _board.D6);
            TestMoveEat(_board.D4, _board.B6);
            TestMoveEat(_board.G6, _board.E4);
            TestMoveEat(_board.A6, _board.C4);

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
            TestMoveEat(_board.F4, _board.D5);
            TestMoveEat(_board.E4, _board.C5);
            TestMoveEat(_board.A4, _board.C3);
            TestMoveEat(_board.D4, _board.F5);

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

        [Test]
        public void Queen()
        {
            // Arrange
            _board = new Board
            {
                A3 = {CurrentChessPiece = new Queen(false)},
                A4 = {CurrentChessPiece = new Queen(false)},
                A5 = {CurrentChessPiece = new Queen(false)},
                B6 = {CurrentChessPiece = new Queen(false)},
                G3 = {CurrentChessPiece = new Queen(true)},
                H4 = {CurrentChessPiece = new Queen(true)},
                H5 = {CurrentChessPiece = new Queen(true)},
                H6 = {CurrentChessPiece = new Queen(true)},
            };

            var a3 = _board.A3.CurrentChessPiece;
            var a4 = _board.A4.CurrentChessPiece;
            var a5 = _board.A5.CurrentChessPiece;
            var b6 = _board.B6.CurrentChessPiece;
            var g3 = _board.G3.CurrentChessPiece;
            var h4 = _board.H4.CurrentChessPiece;
            var h5 = _board.H5.CurrentChessPiece;
            var h6 = _board.H6.CurrentChessPiece;

            // Act
            TestMoveEat(_board.A3, _board.C5);
            TestMoveEat(_board.A5, _board.C3);
            TestMoveEat(_board.A4, _board.C4);
            TestMoveEat(_board.B6, _board.B3);
            TestMoveEat(_board.H4, _board.F6);
            TestMoveEat(_board.H6, _board.F4);
            TestMoveEat(_board.H5, _board.F5);
            TestMoveEat(_board.G3, _board.G6);

            // Assert
            Assert.IsNull(_board.A3.CurrentChessPiece, "TOPRIGHT TOPRIGHT Didn't remove ChessPiece");
            Assert.IsNull(_board.A5.CurrentChessPiece, "BOTTOMRIGHT BOTTOMRIGHT Didn't remove ChessPiece");
            Assert.IsNull(_board.A4.CurrentChessPiece, "RIGHT RIGHT Didn't remove ChessPiece");
            Assert.IsNull(_board.B6.CurrentChessPiece, "BOTTOM BOTTOM BOTTOM Didn't remove ChessPiece");
            Assert.IsNull(_board.H4.CurrentChessPiece, "TOPLEFT TOPLEFT  Didn't remove ChessPiece");
            Assert.IsNull(_board.H6.CurrentChessPiece, "BOTTOMLEFT BOTTOMLEFT  Didn't remove ChessPiece");
            Assert.IsNull(_board.H5.CurrentChessPiece, "LEFT LEFT Didn't remove ChessPiece");
            Assert.IsNull(_board.G3.CurrentChessPiece, "TOP TOP TOP  Didn't remove ChessPiece");
            Assert.AreEqual(_board.C5.CurrentChessPiece, a3, "TOPRIGHT TOPRIGHT Didn't add ChessPiece");
            Assert.AreEqual(_board.C3.CurrentChessPiece, a5, "BOTTOMRIGHT BOTTOMRIGHT Didn't add ChessPiece");
            Assert.AreEqual(_board.C4.CurrentChessPiece, a4, "RIGHT RIGHT Didn't add ChessPiece");
            Assert.AreEqual(_board.B3.CurrentChessPiece, b6, "BOTTOM BOTTOM BOTTOM Didn't add ChessPiece");
            Assert.AreEqual(_board.F6.CurrentChessPiece, h4, "TOPLEFT TOPLEFT  Didn't add ChessPiece");
            Assert.AreEqual(_board.F4.CurrentChessPiece, h6, "BOTTOMLEFT BOTTOMLEFT  Didn't add ChessPiece");
            Assert.AreEqual(_board.F5.CurrentChessPiece, h5, "LEFT LEFT  Didn't add ChessPiece");
            Assert.AreEqual(_board.G6.CurrentChessPiece, g3, "TOP TOP TOP Didn't add ChessPiece");
        }

        public static void TestMoveEat(CellViewModel cellToEat, CellViewModel cellToBeEaten)
        {
            CellViewModel.MoveModel(cellToEat, cellToBeEaten);
        }
    }
}
