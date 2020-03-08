using System;
using System.Collections.Generic;
using System.Text;
using FlashCardGame.Core.Constants;
using FlashCardGame.Core.Events;
using FlashCardGame.Modules.Game.Service;
using FlashCardGame.Modules.Game.ViewModels;
using Moq;
using Prism.Events;
using Xunit;
using FlashCardGame.Modules.Game.Tests.Common;
using FlashCardGame.Core;

namespace FlashCardGame.Modules.Game.Tests
{
    public class QuestionViewModelTests
    {
        [Fact]
        public void Creation_CanStartGame()
        {
            QuestionViewModel sut = CreateQuestionViewModel();

            Assert.True(sut.CanStartGame);
        }

        [Fact]
        public void Creation_ShouldDisableQuestionButtons()
        {
            QuestionViewModel sut = CreateQuestionViewModel();

            Assert.False(sut.SubmitAnswerCommand.CanExecute());
            Assert.False(sut.NextQuestionCommand.CanExecute());
        }

        [Fact]
        public void StartGame_ShouldPublishGameStartEvent()
        {
            QuestionViewModel sut = CreateQuestionViewModel();

            sut.StartNewGameCommand.Execute();

            _gameControlEventMock.Verify(game => game.Publish(GameControlMessage.Start), Times.Exactly(1));
        }

        [Fact]
        public void StartGame_SetCorrectButtonStatus()
        {
            QuestionViewModel sut = CreateQuestionViewModel();

            sut.StartNewGameCommand.Execute();
            Assert.False(sut.StartNewGameCommand.CanExecute());

            // HandleGameControlEvent enables these three buttons.
            // TODO need to setup eventMock to have callback to HandleGameControlEvent
            //Assert.True(sut.SubmitAnswerCommand.CanExecute());
            //Assert.True(sut.NextQuestionCommand.CanExecute());
        }

        [Theory]
        [InlineData(null)]
        [InlineData("")]
        [InlineData(" ")]
        [InlineData("   ")]
        public void SubmitAnswer_GradeEmptyAnswer(string answer)
        {
            QuestionViewModel sut = CreateQuestionViewModel();
            sut.Answer = answer;
            sut.IsGameRunning = true;

            sut.SubmitAnswerCommand.Execute();

            _updateScoreEvent.Verify(e => e.Publish(-1), Times.Exactly(1));
            _sendAnswerResultEvent.Verify(e => e.Publish(false), Times.Exactly(1));
        }

        [Theory]
        [InlineData("unexpected")]
        public void SubmitAnswer_GradeNonNumericAnswer_ThrowFormatException(string answer)
        {
            QuestionViewModel sut = CreateQuestionViewModel();
            sut.Answer = answer;
            sut.IsGameRunning = true;

            Assert.Throws<FormatException>(() => sut.SubmitAnswerCommand.Execute());
        }

        [Theory]
        [InlineData("1", 1, 1, Operator.Multiplication)]
        [InlineData("-1", 0, 1, Operator.Minus)]
        [InlineData("0.5", 1, 2, Operator.Division)]
        public void SubmitAnswer_GradeCorrectAnsower(string answer, int number1, int number2, Operator op)
        {
            QuestionViewModel sut = CreateQuestionViewModelForGrading(answer, number1, number2, op);
            sut.SubmitAnswerCommand.Execute();
            _updateScoreEvent.Verify(e => e.Publish(1), Times.Exactly(1));
            _sendAnswerResultEvent.Verify(e => e.Publish(true), Times.Exactly(1));
        }

        [Theory]
        [InlineData("1.0", 1, 1, Operator.Multiplication)]
        [InlineData("1.00", 1, 1, Operator.Multiplication)]
        [InlineData("-1.0", 0, 1, Operator.Minus)]
        [InlineData("-1.00", 0, 1, Operator.Minus)]
        [InlineData("0.33", 1, 3, Operator.Division)]
        public void SubmitAnswer_GradeCorrectAnswer_WithDecimal(string answer, int number1, int number2, Operator op)
        {
            QuestionViewModel sut = CreateQuestionViewModelForGrading(answer, number1, number2, op);

            sut.SubmitAnswerCommand.Execute();

            _updateScoreEvent.Verify(e => e.Publish(1), Times.Exactly(1));
            _sendAnswerResultEvent.Verify(e => e.Publish(true), Times.Exactly(1));
        }

        [Theory]
        [InlineData("0.32", 1, 3, Operator.Division)]
        [InlineData("0.34", 1, 3, Operator.Division)]
        public void SubmitAnswer_GradeAnswer_WithinTolerance(string answer, int number1, int number2, Operator op)
        {
            QuestionViewModel sut = CreateQuestionViewModelForGrading(answer, number1, number2, op);

            sut.SubmitAnswerCommand.Execute();

            _updateScoreEvent.Verify(e => e.Publish(1), Times.Exactly(1));
            _sendAnswerResultEvent.Verify(e => e.Publish(true), Times.Exactly(1));
        }

        [Theory]
        [InlineData("0.31", 1, 3, Operator.Division)]
        [InlineData("0.35", 1, 3, Operator.Division)]
        public void SubmitAnswer_GradeAnswer_OutsideTolerance(string answer, int number1, int number2, Operator op)
        {
            QuestionViewModel sut = CreateQuestionViewModelForGrading(answer, number1, number2, op);

            sut.SubmitAnswerCommand.Execute();

            _updateScoreEvent.Verify(e => e.Publish(-1), Times.Exactly(1));
            _sendAnswerResultEvent.Verify(e => e.Publish(false), Times.Exactly(1));
        }

        [Fact]
        public void SubmitAnswer_GradeCorrectAnswer_SmallerThanTolerance()
        {
            int number1 = 1;
            int number2 = 3;
            //set up answer that is closer to target than tolerance
            double value = (((double)number1 / number2) - AppConstants.Tolerance + AppConstants.Tolerance / 10);
            string answer = value.ToString("F2");

            QuestionViewModel sut = CreateQuestionViewModelForGrading(answer, number1, number2, Operator.Division);

            sut.SubmitAnswerCommand.Execute();

            _updateScoreEvent.Verify(e => e.Publish(1), Times.Exactly(1));
            _sendAnswerResultEvent.Verify(e => e.Publish(true), Times.Exactly(1));
        }

        [Fact(Skip = "F2 format")]
        public void SubmitAnswer_GradeWrongAnswer_EqualTolerance()
        {
            int number1 = 1;
            int number2 = 3;
            //set up answer that is different from target by tolerance
            double value = (((double)number1 / number2) - AppConstants.Tolerance);
            string answer = value.ToString("F2");

            //TODO test failed due to F2 format the answer to 0.32. Given tolerance = 0.15,  it is graded correct.
            QuestionViewModel sut = CreateQuestionViewModelForGrading(answer, number1, number2, Operator.Division);

            sut.SubmitAnswerCommand.Execute();

            _updateScoreEvent.Verify(e => e.Publish(-1), Times.Exactly(1));
            _sendAnswerResultEvent.Verify(e => e.Publish(false), Times.Exactly(1));
        }

        [Fact(Skip = "F2 format")]
        public void SubmitAnswer_GradeWrongAnswer_BiggerThanTolerance()
        {
            int number1 = 1;
            int number2 = 3;
            //set up answer that is far from target than tolerance
            double value = (((double)number1 / number2) - AppConstants.Tolerance - AppConstants.Tolerance / 10);
            string answer = value.ToString("F2");

            //TODO test failed due to F2 format the answer to 0.32. Given tolerance = 0.15,  it is graded correct.
            QuestionViewModel sut = CreateQuestionViewModelForGrading(answer, number1, number2, Operator.Division);

            sut.SubmitAnswerCommand.Execute();

            _updateScoreEvent.Verify(e => e.Publish(-1), Times.Exactly(1));
            _sendAnswerResultEvent.Verify(e => e.Publish(false), Times.Exactly(1));
        }

        [Theory]
        [InlineData("1", 1, 3, Operator.Division)]
        public void SubmitAnswer_GradeWrongAnswer(string answer, int number1, int number2, Operator op)
        {
            QuestionViewModel sut = CreateQuestionViewModelForGrading(answer, number1, number2, op);

            sut.SubmitAnswerCommand.Execute();

            _updateScoreEvent.Verify(e => e.Publish(-1), Times.Exactly(1));
            _sendAnswerResultEvent.Verify(e => e.Publish(false), Times.Exactly(1));
        }

        [Theory]
        [InlineData("1", 1, 0, Operator.Division)]
        public void SubmitAnswer_DivideByZero(string answer, int number1, int number2, Operator op)
        {
            QuestionViewModel sut = CreateQuestionViewModelForGrading(answer, number1, number2, op);

            Assert.Throws<DivideByZeroException>(() => sut.SubmitAnswerCommand.Execute());
        }

        [Fact]
        public void NextQuestion_ShouldNotGradeAnswer()
        {
            var sut = CreateQuestionViewModel();

            sut.IsGameRunning = true;

            sut.NextQuestionCommand.Execute();

            Assert.Equal(_question, sut.Question);
            Assert.Equal(string.Empty, sut.Answer);
        }

        private Mock<IQuestionGenerator> _qGenMock = new Mock<IQuestionGenerator>();

        private Mock<IEventAggregator> _eaMock = new Mock<IEventAggregator>();

        private Mock<GameControlEvent> _gameControlEventMock = new Mock<GameControlEvent>();

        private Mock<UpdateScoreEvent> _updateScoreEvent = new Mock<UpdateScoreEvent>();

        private Mock<SendAnswerResultEvent> _sendAnswerResultEvent = new Mock<SendAnswerResultEvent>();

        private GameQuestion _question;

        private QuestionViewModel CreateQuestionViewModelForGrading(string answer, int number1, int number2, Operator op)
        {
            var settingMock = Helper.SetupConfigMock(op, 0, 12);
            QuestionViewModel sut = CreateQuestionViewModel();
            sut.Answer = answer;
            sut.IsGameRunning = true;
            sut.Question = new GameQuestion
            {
                Pair = new NumberPair
                {
                    Number1 = number1,
                    Number2 = number2
                },
                OpCtx = settingMock.Object.Operators[(int)op]
            };
            return sut;
        }

        private QuestionViewModel CreateQuestionViewModel()
        {
            var settingMock = Helper.SetupConfigMock(Operator.Division, 0, 12);
            _question = new GameQuestion
            {
                Pair = new NumberPair { Number1 = 1, Number2 = 1 },
                OpCtx = settingMock.Object.Operators[0]
            };

            _eaMock.Setup(ea => ea.GetEvent<GameControlEvent>()).Returns(_gameControlEventMock.Object);
            _eaMock.Setup(ea => ea.GetEvent<UpdateScoreEvent>()).Returns(_updateScoreEvent.Object);
            _eaMock.Setup(ea => ea.GetEvent<SendAnswerResultEvent>()).Returns(_sendAnswerResultEvent.Object);

            _qGenMock.Setup(q => q.GenerateQuestion()).Returns(_question);

            var sut = new QuestionViewModel(_qGenMock.Object, _eaMock.Object);
            return sut;
        }
    }
}