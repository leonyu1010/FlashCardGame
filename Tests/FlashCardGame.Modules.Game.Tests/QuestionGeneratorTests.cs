using System;
using System.Collections.Generic;
using System.Text;
using FlashCardGame.Core;
using FlashCardGame.Model;
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

            var configMock = new Mock<IGameConfig>();
            configMock.Setup(configMock => configMock.MinValue).Returns(0);
            configMock.Setup(configMock => configMock.MaxValue).Returns(12);
            configMock.Setup(configMock => configMock.SelectedOperator).Returns(new ArithmeticOp((Operator)operatorIndex));

            //Act
            var questionGenerator = new QuestionGenerator(new RandomNumberGenerator(), configMock.Object);
            int expectedSize = 13 * 13;
            HashSet<string> questions = new HashSet<string>();
            int count = 0;
            while (count++ < expectedSize)
            {
                questions.Add(questionGenerator.GenerateQuestion().Question);
            }

            //Assert
            Assert.Equal(expectedSize, questions.Count);
        }

        [Fact]
        public void QuestionGeneratorTests_CreateValidPairPool_ForDivision()
        {
            //Arrange

            var configMock = new Mock<IGameConfig>();
            configMock.Setup(configMock => configMock.MinValue).Returns(0);
            configMock.Setup(configMock => configMock.MaxValue).Returns(12);
            configMock.Setup(configMock => configMock.SelectedOperator).Returns(new ArithmeticOp(Operator.Divide));

            //Act
            var questionGenerator = new QuestionGenerator(new RandomNumberGenerator(), configMock.Object);
            int expectedSize = 13 * 12;
            int poolSize = 13 * 13;
            HashSet<string> questions = new HashSet<string>();
            int count = 0;
            int divideByZero = 0;
            while (count++ < poolSize)
            {
                var question = questionGenerator.GenerateQuestion().Question;
                var denominator = question.Split('/')[1].Trim();
                if (denominator == "0")
                {
                    divideByZero++;
                }

                questions.Add(question);
            }

            //Assert
            Assert.Equal(expectedSize, questions.Count);
            Assert.Equal(0, divideByZero);
        }
    }
}