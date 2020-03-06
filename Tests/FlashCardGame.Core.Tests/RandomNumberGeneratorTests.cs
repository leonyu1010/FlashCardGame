using System;
using Xunit;
using Moq;

namespace FlashCardGame.Core.Tests
{
    public class RandomNumberGeneratorTests
    {
        [Fact]
        public void RandomNumberGeneratorTests_Default_Range()
        {
            //Arrange
            var rng = new RandomNumberGenerator();

            //Act
            var min = rng.Minimum;
            var max = rng.Maximum;

            //Assert
            Assert.Equal(0, min);
            Assert.Equal(Int32.MaxValue, max);
        }
    }
}