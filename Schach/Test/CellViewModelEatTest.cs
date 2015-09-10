﻿using Chess.Cells;
using Chess.ChessPieces;
using NUnit.Framework;

namespace Chess.Test
{
    [TestFixture]
    class CellViewModelEatTest
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
            var chessPieceToBeEaten = _board.D4.CurrentChessPiece;
            
            // Act
            TestEat(_board.D4, _board.F6);

            // Assert
            Assert.IsNull(_board.D4.CurrentChessPiece);
            Assert.AreEqual(_board.F6.CurrentChessPiece, new Bishop(true));
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

        }
    }
}
