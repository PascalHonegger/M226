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
        public void EatPossible()
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
            var d3 = _board.D3.CurrentChessPiece;
            var d4 = _board.D4.CurrentChessPiece;
            // Act
            PathTest.TestMoveEat(_board.D4, _board.D3);

            // Assert
            Assert.IsNull(_board.D4.CurrentChessPiece, "There is still a ChessPiece on the CellViewModel D4, which should have moved away");
            Assert.AreEqual(_board.D3.CurrentChessPiece, d4, "ChessPiece 'evilChessPiece' didn't move to his new Location properly");
            Assert.True(_board.GraveYard.Contains(d3), "ChessPiece 'toBeEaten' didn't move to the Graveyard properly");
        }

        [Test]
        public void EatImpossible()
        {
            var pathList = new List<Path>
            {
                new PathFactory().AddToPath(Movement.Direction.Top).SetIsRecursive(false).Create(),
                new PathFactory().AddToPath(Movement.Direction.Bottom).SetIsRecursive(true).Create(),
                new PathFactory().AddToPath(Movement.Direction.Left).SetIsRecursive(true).Create()
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
                D5 = {CurrentChessPiece = chessPieceMock.Object},
                D4 = {CurrentChessPiece = chessPieceMock2.Object},
                D3 = {CurrentChessPiece = chessPieceMock2.Object},
                B5 = {CurrentChessPiece = chessPieceMock2.Object},
                B3 = {CurrentChessPiece = chessPieceMock2.Object}
            };
            var d5 = _board.D5.CurrentChessPiece;
            var d4 = _board.D4.CurrentChessPiece;
            var d3 = _board.D3.CurrentChessPiece;
            var b5 = _board.B5.CurrentChessPiece;
            var b3 = _board.B3.CurrentChessPiece;

            // Act
            PathTest.TestMoveEat(_board.D5, _board.D3);
            PathTest.TestMoveEat(_board.D5, _board.D3);
            PathTest.TestMoveEat(_board.D5, _board.B5);

            // Assert
            Assert.IsNull(_board.D5.CurrentChessPiece, "There is still a ChessPiece on the CellViewModel D5, which should have moved away");
            Assert.AreEqual(_board.D4.CurrentChessPiece, d4, "ChessPiece 'd4' shouldn't be remove");
            Assert.AreEqual(_board.D3.CurrentChessPiece, d3, "ChessPiece 'd3' shouldn't be removed");
            Assert.AreEqual(_board.B3.CurrentChessPiece, b3, "ChessPiece 'b3' did get eaten, but there was no valid Path");
            Assert.AreEqual(_board.B5.CurrentChessPiece, d5, "ChessPiece 'd5' didn't move to the CellViewModel 'B5'");
            Assert.AreEqual(_board.GraveYard.Count, 1, "There where more ChessPieces in the Graveyard than expected");
            Assert.True(_board.GraveYard.Contains(b5), "ChessPiece 'b5' wasn't in the Graveyard, but should have been there");
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
