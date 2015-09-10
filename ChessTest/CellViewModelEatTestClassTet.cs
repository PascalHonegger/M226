using Chess;
using Chess.Cells;
using Chess.ChessPieces;
using NUnit.Framework;

namespace ChessTest
{
    [TestFixture]
    public class CellViewModelEatTestClassTet
    {
        private Board _board;

        [Test]
        public void EatBishopPossible()
        {
            // Arrange
            _board = new Board
            {
                D4 = {CurrentChessPiece = new Bishop(false)},
                F6 = {CurrentChessPiece = new Bishop(true)}
            };
            var chessPieceToBeEaten = _board.F6.CurrentChessPiece;
            var evilChessPiece = _board.D4.CurrentChessPiece;
            // Act
            TestEat(_board.D4, _board.F6);

            // Assert
            Assert.IsNull(_board.D4.CurrentChessPiece);
            Assert.AreEqual(_board.F6.CurrentChessPiece, evilChessPiece);
            Assert.True(_board.GraveYard.Contains(chessPieceToBeEaten));
        }

        [Test]
        public void EatBishopImpossible()
        {
            // Arrange
            _board = new Board
            {
                D4 = {CurrentChessPiece = new Bishop(false)},
                E5 = {CurrentChessPiece = new Pawn(false)},
                F6 = {CurrentChessPiece = new Bishop(true)},
                
            };
            var toEat = _board.D4;
            var toBeEaten = _board.F6;
            var chessPieceToBeEaten = toBeEaten.CurrentChessPiece;
            // Act
            TestEat(toEat, toBeEaten);



            // Assert
            Assert.IsNull(toEat.CurrentChessPiece);
            Assert.AreEqual(toBeEaten.CurrentChessPiece, new Bishop(true));
            Assert.False(_board.GraveYard.Contains(chessPieceToBeEaten));
        }

        public void TestEat(CellViewModel cellToEat, CellViewModel cellToBeEaten)
        {
            cellToBeEaten.MoveToGraveyard();
            cellToBeEaten.CurrentChessPiece = cellToEat.CurrentChessPiece;
            cellToEat.CurrentChessPiece = null;
        }
    }
}
