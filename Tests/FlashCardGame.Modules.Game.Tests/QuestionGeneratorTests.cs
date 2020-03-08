using System.Collections.Generic;
using FlashCardGame.Core;
using FlashCardGame.Modules.Game.Service;
using FlashCardGame.Modules.Game.Tests.Common;
using Moq;
using Xunit;

namespace FlashCardGame.Modules.Game.Tests
{
    public class QuestionGeneratorTests
    {
        [Theory]
        [InlineData(0, 0, 12)]
        [InlineData(1, 0, 12)]
        [InlineData(2, 0, 12)]
        [InlineData(0, 1, 12)]
        [InlineData(1, 1, 12)]
        [InlineData(2, 1, 12)]
        public void QuestionGeneratorTests_PlusMinusMultiplication(int operatorIndex, int min, int max)
        {
            //Arrange

            Mock<IGameSetting> configMock = Helper.SetupConfigMock(operatorIndex, min, max);

            //Act
            var questionGenerator = new QuestionGenerator(new RandomNumberGenerator(), configMock.Object);
            int expectedQuestionCount = (max - min + 1) * (max - min + 1);
            HashSet<string> questions = new HashSet<string>();
            int count = 0;
            while (count++ < expectedQuestionCount)
            {
                questions.Add(questionGenerator.GenerateQuestion().ToString());
            }

            //Assert
            Assert.Equal(expectedQuestionCount, questions.Count);
        }

        [Theory]
        [InlineData(3, 0, 12, 13 * 12)]
        [InlineData(3, 1, 12, 12 * 12)]
        public void QuestionGeneratorTests_Division(int operatorIndex, int min, int max, int expectedQuestionCount)
        {
            //Arrange
            var configMock = Helper.SetupConfigMock(operatorIndex, min, max);

            //Act
            var questionGenerator = new QuestionGenerator(new RandomNumberGenerator(), configMock.Object);
            int poolSize = (max - min + 1) * (max - min + 1);

            HashSet<string> questions = new HashSet<string>();

            int count = 0;
            int divideByZero = 0;
            while (count < poolSize)
            {
                var question = questionGenerator.GenerateQuestion();
                ++count;

                var denominator = question.Pair.Number2;
                if (denominator == 0)
                {
                    ++divideByZero;
                }

                questions.Add(question.ToString());
            }

            //Assert
            Assert.Equal(expectedQuestionCount, questions.Count);
            Assert.Equal(0, divideByZero);
        }
    }
}