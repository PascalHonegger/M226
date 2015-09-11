using System.Collections.Generic;
using Chess;
using Chess.Cells;
using Chess.ChessPieces;
using Chess.Path;
using Moq;
using NUnit.Framework;

namespace ChessTest
{
    [TestFixture]
    public class CellViewModelTest
    {
        private Board _board;

        [Test]
        public void MoveEat()
        {
            var pathList = new List<Path>
            {
                new PathFactory().AddToPath(Movement.Direction.Top).SetIsRecursive(false).Create(),
                new PathFactory().AddToPath(Movement.Direction.Bottom).SetIsRecursive(false).Create()
            };

            var chessPieceMock = new Mock<ChessPieceBase>();
            chessPieceMock
                .Setup(mock => mock.IsWhite())
                .Returns(true);
            chessPieceMock
                .Setup(mock => mock.PathList)
                .Returns(pathList);

            var chessPieceMock2 = new Mock<ChessPieceBase>();
            chessPieceMock2
                .Setup(value => value.IsWhite())
                .Returns(false);

            // Arrange
            _board = new Board
            {
                D4 = { CurrentChessPiece = chessPieceMock.Object },
                D3 = { CurrentChessPiece = chessPieceMock2.Object }
            };
            var toBeEaten = _board.D3.CurrentChessPiece;
            var evilChessPiece = _board.D4.CurrentChessPiece;
            // Act
            PathTest.TestMoveEat(_board.D4, _board.D3);

            // Assert
            Assert.IsNull(_board.D4.CurrentChessPiece, "There is still a ChessPiece on the CellViewModel D4, which should have moved away");
            Assert.AreEqual(_board.D3.CurrentChessPiece, evilChessPiece, "ChessPiece 'evilChessPiece' didn't move to his new Location properly");
            Assert.True(_board.GraveYard.Contains(toBeEaten), "ChessPiece 'toBeEaten' didn't move to the Graveyard properly");
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
            PathTest.TestMoveEat(_board.D4, _board.F6);

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
            PathTest.TestMoveEat(_board.D4, _board.F6);

            // Assert
            Assert.IsNotNull(_board.E5.CurrentChessPiece);
            Assert.AreEqual(_board.F6.CurrentChessPiece, chessPieceToBeEaten);
            Assert.AreEqual(_board.D4.CurrentChessPiece, evilChessPiece);
            Assert.AreEqual(_board.GraveYard.Count, 0);
        }
    }
}
