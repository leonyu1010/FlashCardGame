using FlashCardGame.Core.Constants;
using FlashCardGame.Core.Events;
using FlashCardGame.Model;
using FlashCardGame.Modules.Game.Service;
using Prism.Commands;
using Prism.Events;
using Prism.Mvvm;
using System;
using System.Collections.Generic;
using System.Linq;

namespace FlashCardGame.Modules.Game.ViewModels
{
    public class QuestionViewModel : BindableBase
    {
        public QuestionViewModel(IQuestionGenerator questionGenerator, IEventAggregator ea)
        {
            _questionGenerator = questionGenerator;
            _ea = ea;
            _questionText = "Your question is here";

            _ea.GetEvent<GameControlEvent>().Subscribe(HandleGameControlEvent);
        }

        public string QuestionText
        {
            get { return _questionText; }
            set { SetProperty(ref _questionText, value); }
        }

        public string AnswerText
        {
            get { return _answerText; }
            set { SetProperty(ref _answerText, value); }
        }

        public DelegateCommand CheckAnswerCommand => _checkAnswerCommand ?? (_checkAnswerCommand = new DelegateCommand(ExecuteCheckAnswer));

        public DelegateCommand SubmitAnswerCommand => _submitAnswerCommand ?? (_submitAnswerCommand = new DelegateCommand(ExecuteSubmitAnswer));

        public DelegateCommand NextQuestionCommand => _nextQuestionCommand ?? (_nextQuestionCommand = new DelegateCommand(ExecuteNextQuestion));

        private readonly IQuestionGenerator _questionGenerator;

        private readonly IEventAggregator _ea;

        private DelegateCommand _checkAnswerCommand;

        private DelegateCommand _submitAnswerCommand;

        private DelegateCommand _nextQuestionCommand;

        private string _questionText;

        private string _answerText;

        private GameQuestion _question;

        private void HandleGameControlEvent(string message)
        {
            if (message == GameControlMessage.Start)
            {
                _questionGenerator.Reset();
                ExecuteNextQuestion();
            }
            else if (message == GameControlMessage.Stop)
            {
            }
        }

        private void ExecuteCheckAnswer()
        {
            double answer = double.Parse(AnswerText);

            bool correct = _question.CorrectAnswer == _answerText;
            int score = correct ? 1 : -1;
            _ea.GetEvent<UpdateScoreEvent>().Publish(score);
            _ea.GetEvent<SendAnswerResultEvent>().Publish(correct);
        }

        private void ExecuteSubmitAnswer()
        {
            ExecuteCheckAnswer();
            ExecuteNextQuestion();
        }

        private void ExecuteNextQuestion()
        {
            _question = _questionGenerator.GenerateQuestion();
            QuestionText = _question.Question;
            AnswerText = "";
        }
    }
}