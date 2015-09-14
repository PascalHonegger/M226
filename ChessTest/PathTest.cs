using System.Collections.Generic;
using Chess;
using Chess.Cells;
using Chess.ChessPieces;
using NUnit.Framework;
using Chess.Path;

namespace ChessTest
{
    [TestFixture]
    public class PathTest
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
            CellViewModel.MoveModel(_board.A3, _board.D6);
            CellViewModel.MoveModel(_board.D4, _board.B6);
            CellViewModel.MoveModel(_board.G6, _board.E4);
            CellViewModel.MoveModel(_board.A6, _board.C4);

            // Assert
            Assert.IsNull(_board.A3.CurrentChessPiece, "TOPRIGHT TOPRIGHT TOPRIGHT Didn't remove ChessPiece");
            Assert.IsNull(_board.D4.CurrentChessPiece, "TOPLEFT TOPLEFT Didn't remove ChessPiece");
            Assert.IsNull(_board.G6.CurrentChessPiece, "BOTTOMLEFT BOTTOMLEFT Didn't remove ChessPiece");
            Assert.IsNull(_board.A6.CurrentChessPiece, "BOTTOMRIGHT BOTTOMRIGHT Didn't remove ChessPiece");
            Assert.AreEqual(_board.D6.CurrentChessPiece, a3, "TOPRIGHT TOPRIGHT TOPRIGHT Didn't remove ChessPiece");
            Assert.AreEqual(_board.B6.CurrentChessPiece, d4, "TOPLEFT TOPLEFT Didn't remove ChessPiece");
            Assert.AreEqual(_board.E4.CurrentChessPiece, g6, "BOTTOMLEFT BOTTOMLEFT Didn't remove ChessPiece");
            Assert.AreEqual(_board.C4.CurrentChessPiece, a6, "BOTTOMRIGHT BOTTOMRIGHT Didn't remove ChessPiece");
        }

        [Test]
        public void Knight()
        {
            // Arrange
            _board = new Board
            {
                A6 = { CurrentChessPiece = new Knight(false) },
                H6 = { CurrentChessPiece = new Knight(false) },
                A3 = { CurrentChessPiece = new Knight(true) },
                H3 = { CurrentChessPiece = new Knight(true) },
                E3 = { CurrentChessPiece = new Knight(true) },
                E6 = { CurrentChessPiece = new Knight(true) },
                D3 = { CurrentChessPiece = new Knight(true) },
                D6 = { CurrentChessPiece = new Knight(true) }
            };

            var a6 = _board.A6.CurrentChessPiece;
            var h6 = _board.H6.CurrentChessPiece;
            var a3 = _board.A3.CurrentChessPiece;
            var h3 = _board.H3.CurrentChessPiece;
            var e3 = _board.E3.CurrentChessPiece;
            var e6 = _board.E6.CurrentChessPiece;
            var d3 = _board.D3.CurrentChessPiece;
            var d6 = _board.D6.CurrentChessPiece;


            // Act
            CellViewModel.MoveModel(_board.A6, _board.B4);
            CellViewModel.MoveModel(_board.H6, _board.G4);
            CellViewModel.MoveModel(_board.A3, _board.B5);
            CellViewModel.MoveModel(_board.H3, _board.G5);
            CellViewModel.MoveModel(_board.E3, _board.C4);
            CellViewModel.MoveModel(_board.E6, _board.C5);
            CellViewModel.MoveModel(_board.D3, _board.F4);
            CellViewModel.MoveModel(_board.D6, _board.F5);

            // Assert
            Assert.IsNull(_board.A6.CurrentChessPiece, "BOTTOM BOTTOMRIGHT Didn't remove ChessPiece");
            Assert.IsNull(_board.H6.CurrentChessPiece, "BOTTOM BOTTOMLEFT Didn't remove ChessPiece");
            Assert.IsNull(_board.A3.CurrentChessPiece, "TOP TOPRIGHT Didn't remove ChessPiece");
            Assert.IsNull(_board.H3.CurrentChessPiece, "TOP TOPLEFT Didn't remove ChessPiece");
            Assert.IsNull(_board.E3.CurrentChessPiece, "LEFT TOPLEFT Didn't remove ChessPiece");
            Assert.IsNull(_board.E6.CurrentChessPiece, "LEFT BOTTOMLEFT Didn't remove ChessPiece");
            Assert.IsNull(_board.D3.CurrentChessPiece, "RIGHT TOPRIGHT Didn't remove ChessPiece");
            Assert.IsNull(_board.D6.CurrentChessPiece, "RIGHT BOTTOMRIGHT Didn't remove ChessPiece");
            Assert.AreEqual(_board.B4.CurrentChessPiece, a6, "BOTTOM BOTTOMRIGHT Didn't add ChessPiece");
            Assert.AreEqual(_board.G4.CurrentChessPiece, h6, "BOTTOM BOTTOMLEFT Didn't add ChessPiece");
            Assert.AreEqual(_board.B5.CurrentChessPiece, a3, "TOP TOPRIGHT Didn't add ChessPiece");
            Assert.AreEqual(_board.G5.CurrentChessPiece, h3, "TOP TOPLEFT Didn't add ChessPiece");
            Assert.AreEqual(_board.C4.CurrentChessPiece, e3, "LEFT TOPLEFT Didn't add ChessPiece");
            Assert.AreEqual(_board.C5.CurrentChessPiece, e6, "LEFT BOTTOMLEFT Didn't add ChessPiece");
            Assert.AreEqual(_board.F4.CurrentChessPiece, d3, "RIGHT TOPRIGHT Didn't add ChessPiece");
            Assert.AreEqual(_board.F5.CurrentChessPiece, d6, "RIGHT BOTTOMRIGHT Didn't add ChessPiece");
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
                H6 = {CurrentChessPiece = new Queen(true)}
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
            CellViewModel.MoveModel(_board.A3, _board.C5);
            CellViewModel.MoveModel(_board.A5, _board.C3);
            CellViewModel.MoveModel(_board.A4, _board.C4);
            CellViewModel.MoveModel(_board.B6, _board.B3);
            CellViewModel.MoveModel(_board.H4, _board.F6);
            CellViewModel.MoveModel(_board.H6, _board.F4);
            CellViewModel.MoveModel(_board.H5, _board.F5);
            CellViewModel.MoveModel(_board.G3, _board.G6);

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

        [Test]
        public void BlackPawn()
        {
            // Arrange
            _board = new Board
            {
                A6 = {CurrentChessPiece = new Pawn(false)},
                B6 = {CurrentChessPiece = new Pawn(false)},
                D6 = {CurrentChessPiece = new Pawn(false)},
                E6 = {CurrentChessPiece = new Pawn(false)},
                C5 = {CurrentChessPiece = new Pawn(true)},
                F5 = {CurrentChessPiece = new Pawn(true)}
            };

            var a6 = _board.A6.CurrentChessPiece;
            var b6 = _board.B6.CurrentChessPiece;
            var d6 = _board.D6.CurrentChessPiece;
            var e6 = _board.E6.CurrentChessPiece;

            //Act
            CellViewModel.MoveModel(_board.A6, _board.A5);
            CellViewModel.MoveModel(_board.A5, _board.A3);
            CellViewModel.MoveModel(_board.B6, _board.B4);
            CellViewModel.MoveModel(_board.D6, _board.C5);
            CellViewModel.MoveModel(_board.E6, _board.F5);

            //Assert
            Assert.IsNull(_board.A3.CurrentChessPiece, "BOTTOM BOTTOM did move the ChessPiece, but shouldn't have because DidMove was true");
            Assert.IsNull(_board.A6.CurrentChessPiece, "BOTTOM Didn't remove ChessPiece");
            Assert.IsNull(_board.B6.CurrentChessPiece, "TOP TOP didn't move the ChessPiece, but should have because DidMove was false");
            Assert.IsNull(_board.D6.CurrentChessPiece, "BOTTOMLEFT Didn't remove ChessPiece");
            Assert.IsNull(_board.E6.CurrentChessPiece, "BOTTOMRIGHT Didn't remove ChessPiece");
            Assert.AreEqual(_board.A5.CurrentChessPiece, a6, "BOTTOM Didn't add ChessPiece");
            Assert.AreEqual(_board.B4.CurrentChessPiece, b6, "TOP TOP Didn't add ChessPiece");
            Assert.AreEqual(_board.C5.CurrentChessPiece, d6, "TOPLEFT Didn't add ChessPiece");
            Assert.AreEqual(_board.F5.CurrentChessPiece, e6, "TOPRIGHT Didn't add ChessPiece");
        }

        [Test]
        public void WhitePawn()
        {
            // Arrange
            _board = new Board
            {
                A3 = { CurrentChessPiece = new Pawn(true) },
                B3 = { CurrentChessPiece = new Pawn(true) },
                E5 = { CurrentChessPiece = new Pawn(true) },
                G4 = { CurrentChessPiece = new Pawn(true) },
                D6 = { CurrentChessPiece = new Pawn(false) },
                H5 = { CurrentChessPiece = new Pawn(false) }
            };

            var a3 = _board.A3.CurrentChessPiece;
            var b3 = _board.B3.CurrentChessPiece;
            var e5 = _board.E5.CurrentChessPiece;
            var g4 = _board.G4.CurrentChessPiece;

            //Act
            CellViewModel.MoveModel(_board.A3, _board.A4);
            CellViewModel.MoveModel(_board.A4, _board.A6);
            CellViewModel.MoveModel(_board.B3, _board.B5);
            CellViewModel.MoveModel(_board.E5, _board.D6);
            CellViewModel.MoveModel(_board.G4, _board.H5);

            //Assert
            Assert.IsNull(_board.A6.CurrentChessPiece, "TOP TOP did move the ChessPiece, but shouldn't have because DidMove was true");
            Assert.IsNull(_board.A3.CurrentChessPiece, "TOP Didn't remove ChessPiece");
            Assert.IsNull(_board.B3.CurrentChessPiece, "TOP TOP didn't move the ChessPiece, but should have because DidMove was false");
            Assert.IsNull(_board.E5.CurrentChessPiece, "TOPLEFT remove ChessPiece");
            Assert.IsNull(_board.G4.CurrentChessPiece, "TOPRIGHT Didn't remove ChessPiece");
            Assert.AreEqual(_board.A4.CurrentChessPiece, a3, "TOP Didn't add ChessPiece");
            Assert.AreEqual(_board.B5.CurrentChessPiece, b3, "TOP TOP Didn't add ChessPiece");
            Assert.AreEqual(_board.D6.CurrentChessPiece, e5, "TOPLEFT Didn't add ChessPiece");
            Assert.AreEqual(_board.H5.CurrentChessPiece, g4, "TOPRIGHT Didn't add ChessPiece");
        }

        [Test]
        public void Rook()
        {
            // Arrange
            _board = new Board
            {
                C5 = { CurrentChessPiece = new Rook(false) },
                D5 = { CurrentChessPiece = new Rook(false) },
                A3 = { CurrentChessPiece = new Rook(true) },
                G6 = { CurrentChessPiece = new Rook(true) }
            };

            var c5 = _board.C5.CurrentChessPiece;
            var d5 = _board.D5.CurrentChessPiece;
            var a3 = _board.A3.CurrentChessPiece;
            var g6 = _board.G6.CurrentChessPiece;

            //Act
            CellViewModel.MoveModel(_board.C5, _board.A5);
            CellViewModel.MoveModel(_board.D5, _board.E5);
            CellViewModel.MoveModel(_board.A3, _board.A4);
            CellViewModel.MoveModel(_board.G6, _board.G3);

            //Assert
            Assert.IsNull(_board.C5.CurrentChessPiece, "LEFT LEFT Didn't remove ChessPiece");
            Assert.IsNull(_board.D5.CurrentChessPiece, "RIGHT Didn't remove ChessPiece");
            Assert.IsNull(_board.A3.CurrentChessPiece, "TOP Didn't remove ChessPiece");
            Assert.IsNull(_board.G6.CurrentChessPiece, "BOTTOM BOTTOM BOTTOM Didn't remove ChessPiece");
            Assert.AreEqual(_board.A5.CurrentChessPiece, c5, "LEFT LEFT Didn't remove ChessPiece");
            Assert.AreEqual(_board.E5.CurrentChessPiece, d5, "RIGHT Didn't remove ChessPiece");
            Assert.AreEqual(_board.A4.CurrentChessPiece, a3, "TOP Didn't remove ChessPiece");
            Assert.AreEqual(_board.G3.CurrentChessPiece, g6, "BOTTOM BOTTOM BOTTOM Didn't remove ChessPiece");
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
                new PathFactory().AddToPath(Movement.Direction.Top).AddToPath(Movement.Direction.TopLeft).Create();

            // Assert
            Assert.AreEqual(correctPathList, createdPathList, "The Path created via the PathCreator doesn't match the hand-made Path");
        }
    }
}
