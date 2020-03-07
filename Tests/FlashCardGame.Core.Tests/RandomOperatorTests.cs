using System;
using System.Collections.Generic;
using System.Text;
using Moq;
using Xunit;

namespace FlashCardGame.Core.Tests
{
    public class RandomOperatorTests
    {
        [Theory]
        [InlineData(0, "+")]
        [InlineData(1, "-")]
        [InlineData(2, "*")]
        [InlineData(3, "/")]
        public void RandomOperator_Create(int operatorIndex, string operatorSign)
        {
            //Arrange
            var rngMock = new Mock<IRandomNumberGenerator>();
            rngMock.Setup(rngMock => rngMock.GetOneNumber(0, 4)).Returns(operatorIndex);

            //Act
            var randOp = new RandomOperatorClass(rngMock.Object);
            var sign = randOp.ToSign();

            //Assert
            Assert.Equal(operatorSign, sign);
        }

        [Theory]
        [InlineData(1, 2, Operator.Plus, 3)]
        [InlineData(1, 2, Operator.Minus, -1)]
        [InlineData(1, 2, Operator.Multiply, 2)]
        [InlineData(1, 2, Operator.Divide, 0)]
        public void RandomOperator_Calculate(int number1, int number2, Operator op, int expectedAnswer)
        {
            //Arrange
            var rngMock = new Mock<IRandomNumberGenerator>();
            rngMock.Setup(rngMock => rngMock.GetOneNumber(0, 4)).Returns((int)op);

            //Act
            var randOp = new RandomOperatorClass(rngMock.Object);
            var actual = randOp.Calculate(new NumberPair() { Number1 = number1, Number2 = number2 });

            //Assert
            Assert.Equal(expectedAnswer, actual);
        }

        [Fact]
        public void RandomOperator_Calculate_ThrowDivideByZero()
        {
            //Arrange
            var rngMock = new Mock<IRandomNumberGenerator>();
            rngMock.Setup(rngMock => rngMock.GetOneNumber(0, 4)).Returns((int)Operator.Divide);

            //Act
            var randOp = new RandomOperatorClass(rngMock.Object);

            //Assert
            Assert.Throws<DivideByZeroException>(() => randOp.Calculate(new NumberPair() { Number1 = 1, Number2 = 0 }));
        }

        [Fact]
        public void RandomOperator_Create_ThrowUnsupportedOpertor()
        {
            //Arrange
            var rngMock = new Mock<IRandomNumberGenerator>();
            rngMock.Setup(rngMock => rngMock.GetOneNumber(0, 4)).Returns(4);

            //Act
            var randOp = new RandomOperatorClass(rngMock.Object);
            //Assert
            var ex = Assert.Throws<Exception>(() => randOp.ToSign());
            Assert.Equal("unexpected operator", ex.Message);
        }
    }
}