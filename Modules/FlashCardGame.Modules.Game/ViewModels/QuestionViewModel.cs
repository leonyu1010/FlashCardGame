using FlashCardGame.Core.Constants;
using FlashCardGame.Core.Events;
using FlashCardGame.Model;
using FlashCardGame.Modules.Game.Service;
using Prism.Commands;
using Prism.Events;
using Prism.Mvvm;
using Prism.Regions;
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
            _ea.GetEvent<GameControlEvent>().Subscribe(HandleGameControlEvent);

            IconHeight = 1;
            IconWidth = 1;

            CanStartGame = true;

            StartNewGameCommand = new DelegateCommand(ExecuteNewGame).ObservesCanExecute(() => CanStartGame);

            CheckAnswerCommand = new DelegateCommand(ExecuteCheckAnswer).ObservesCanExecute(() => IsGameRunning);

            SubmitAnswerCommand = new DelegateCommand(ExecuteSubmitAnswer).ObservesCanExecute(() => IsGameRunning);

            NextQuestionCommand = new DelegateCommand(ExecuteNextQuestion).ObservesCanExecute(() => IsGameRunning);
        }

        public GameQuestion Question
        {
            get { return _question; }
            set { SetProperty(ref _question, value); }
        }

        public int IconWidth
        {
            get { return _iconWidth; }
            set { SetProperty(ref _iconWidth, value); }
        }

        public int IconHeight
        {
            get { return _iconHeight; }
            set { SetProperty(ref _iconHeight, value); }
        }

        public string AnswerText
        {
            get { return _answerText; }
            set { SetProperty(ref _answerText, value); }
        }

        public bool IsGameRunning
        {
            get { return _isGameRunning; }
            set { SetProperty(ref _isGameRunning, value); }
        }

        public bool CanStartGame
        {
            get { return _canStartGame; }
            set { SetProperty(ref _canStartGame, value); }
        }

        public DelegateCommand StartNewGameCommand { get; private set; }
        public DelegateCommand CheckAnswerCommand { get; private set; }
        public DelegateCommand SubmitAnswerCommand { get; private set; }
        public DelegateCommand NextQuestionCommand { get; private set; }

        private readonly IQuestionGenerator _questionGenerator;
        private readonly IEventAggregator _ea;

        private GameQuestion _question;
        private string _answerText;
        private int _iconHeight;
        private int _iconWidth;
        private bool _isGameRunning;
        private bool _canStartGame;

        private void ExecuteNewGame()
        {
            _questionGenerator.Reset();

            _ea.GetEvent<GameControlEvent>().Publish(GameControlMessage.Start);

            CanStartGame = false;
        }

        private void ExecuteCheckAnswer()
        {
            bool correct = false;
            if (!string.IsNullOrWhiteSpace(AnswerText))
            {
                double answer = double.Parse(AnswerText);
                double correctAnswer = Question.OpCtx.Handler.Calculate(Question.Pair);
                correct = Math.Abs(correctAnswer - answer) < AppConstants.Tolerance;
            }
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
            Question = _questionGenerator.GenerateQuestion();
            AnswerText = "";
            IconHeight = 20;
            IconWidth = 20;
        }

        private void HandleGameControlEvent(string message)
        {
            if (message == GameControlMessage.Start)
            {
                IsGameRunning = true;
                ExecuteNextQuestion();
            }
            else if (message == GameControlMessage.Stop)
            {
                IsGameRunning = false;
                CanStartGame = true;
            }
        }
    }
}