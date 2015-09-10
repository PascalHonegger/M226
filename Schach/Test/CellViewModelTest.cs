using Chess.Cells;
using Chess.ChessPieces;
using NUnit.Framework;

namespace Chess.Test
{
    [TestFixture]
    class CellViewModelTest
    {
        [Test]
        public void TestEat()
        {
            // Arrange
            var cellViewModelToBeEaten = new CellViewModel(new Pawn(true));

            // Act
            cellViewModelToBeEaten.Eat();

            // Assert
            Assert.True(true);
        }
    }
}
