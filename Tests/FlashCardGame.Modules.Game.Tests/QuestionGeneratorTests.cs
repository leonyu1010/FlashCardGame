using System.Collections.Generic;
using FlashCardGame.Core;
using FlashCardGame.Modules.Game.Service;
using Moq;
using Xunit;

namespace FlashCardGame.Modules.Game.Tests
{
    public class QuestionGeneratorTests
    {
        [Theory]
        [InlineData(0)]
        [InlineData(1)]
        [InlineData(2)]
        public void QuestionGeneratorTests_CreateValidPairPool(int operatorIndex)
        {
            //Arrange

            Mock<IGameSetting> configMock = SetupConfigMock(operatorIndex);

            //Act
            var questionGenerator = new QuestionGenerator(new RandomNumberGenerator(), configMock.Object);
            int expectedSize = 13 * 13;
            HashSet<string> questions = new HashSet<string>();
            int count = 0;
            while (count++ < expectedSize)
            {
                questions.Add(questionGenerator.GenerateQuestion().ToString());
            }

            //Assert
            Assert.Equal(expectedSize, questions.Count);
        }

        [Fact]
        public void QuestionGeneratorTests_CreateValidPairPool_ForDivision()
        {
            //Arrange
            var configMock = SetupConfigMock((int)Operator.Division);

            //Act
            var questionGenerator = new QuestionGenerator(new RandomNumberGenerator(), configMock.Object);
            int expectedSize = 13 * 12;
            int poolSize = 13 * 13;
            HashSet<string> questions = new HashSet<string>();
            int count = 0;
            int divideByZero = 0;
            while (count++ < poolSize)
            {
                var question = questionGenerator.GenerateQuestion();
                var denominator = question.Pair.Number2;
                if (denominator == 0)
                {
                    ++divideByZero;
                }

                questions.Add(question.ToString());
            }

            //Assert
            Assert.Equal(expectedSize, questions.Count);
            Assert.Equal(0, divideByZero);
        }

        private static Mock<IGameSetting> SetupConfigMock(int operatorIndex)
        {
            var configMock = new Mock<IGameSetting>();
            configMock.Setup(configMock => configMock.MinValueInQuestion).Returns(0);
            configMock.Setup(configMock => configMock.MaxValueInQuestion).Returns(12);
            configMock.Setup(configMock => configMock.SelectedOp).Returns(new ArithmeticOp((Operator)operatorIndex));
            configMock.Setup(configMock => configMock.Operators).Returns(new List<OperatorContext>
            {
                new OperatorContext()
                {
                    Name = Operator.Plus,
                    Icon = MaterialDesignIcons.Plus,
                    Handler = new ArithmeticOp(Operator.Plus)
                },
                new OperatorContext()
                {
                    Name = Operator.Minus,
                    Icon = MaterialDesignIcons.Minus,
                    Handler = new ArithmeticOp(Operator.Minus)
                },
                new OperatorContext()
                {
                    Name = Operator.Multiplication,
                    Icon = MaterialDesignIcons.Multiplication,
                    Handler = new ArithmeticOp(Operator.Multiplication)
                },
                new OperatorContext()
                {
                    Name = Operator.Division,
                    Icon = MaterialDesignIcons.Division,
                    Handler = new ArithmeticOp(Operator.Division)
                }
            });
            return configMock;
        }
    }
}