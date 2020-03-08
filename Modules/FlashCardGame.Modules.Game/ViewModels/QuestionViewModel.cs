using FlashCardGame.Core;
using FlashCardGame.Core.Constants;
using FlashCardGame.Core.Events;
using FlashCardGame.Modules.Game.Service;
using Prism.Commands;
using Prism.Events;
using Prism.Mvvm;
using System;

namespace FlashCardGame.Modules.Game.ViewModels
{
    public class QuestionViewModel : BindableBase
    {
        public QuestionViewModel(IQuestionGenerator questionGenerator, IEventAggregator ea)
        {
            _questionGenerator = questionGenerator;
            _ea = ea;
            _ea.GetEvent<GameControlEvent>().Subscribe(HandleGameControlEvent);
            Icon = MaterialDesignIcons.PointingDown;
            CanStartGame = true;
            StartNewGameCommand = new DelegateCommand(ExecuteNewGame).ObservesCanExecute(() => CanStartGame);
            //CheckAnswerCommand = new DelegateCommand(ExecuteCheckAnswer).ObservesCanExecute(() => IsGameRunning);
            SubmitAnswerCommand = new DelegateCommand(ExecuteSubmitAnswer).ObservesCanExecute(() => IsGameRunning);
            NextQuestionCommand = new DelegateCommand(ExecuteNextQuestion).ObservesCanExecute(() => IsGameRunning);
        }

        public string Icon
        {
            get { return _icon; }
            set { SetProperty(ref _icon, value); }
        }

        public GameQuestion Question
        {
            get { return _question; }
            set { SetProperty(ref _question, value); }
        }

        public string Answer
        {
            get { return _answer; }
            set { SetProperty(ref _answer, value); }
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

        //public DelegateCommand CheckAnswerCommand { get; private set; }
        public DelegateCommand SubmitAnswerCommand { get; private set; }

        public DelegateCommand NextQuestionCommand { get; private set; }

        private readonly IQuestionGenerator _questionGenerator;
        private readonly IEventAggregator _ea;
        private GameQuestion _question;
        private string _answer;
        private string _icon;
        private bool _isGameRunning;
        private bool _canStartGame;

        private void ExecuteNewGame()
        {
            FileLogger.Singleton.Information("GameStart");
            _ea.GetEvent<GameControlEvent>().Publish(GameControlMessage.Start);
            CanStartGame = false;
        }

        private void ExecuteCheckAnswer()
        {
            bool correct = false;
            if (!string.IsNullOrWhiteSpace(Answer))
            {
                double answer = double.Parse(Answer);
                double correctAnswer = Question.OpCtx.Handler.Calculate(Question.Pair);
                correct = Math.Abs(correctAnswer - answer) < AppConstants.Tolerance;
                if (!correct)
                {
                    FileLogger.Singleton.Information("WrongAnswer [{question} = {answer}]", Question, answer);
                }
            }
            else
            {
                FileLogger.Singleton.Information("NoAnswer [{question}]", Question);
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
            Icon = Question.OpCtx.Icon;
            Answer = "";
            FileLogger.Singleton.Debug("Question [{question}]", Question);
        }

        private void HandleGameControlEvent(string message)
        {
            if (message == GameControlMessage.Start)
            {
                IsGameRunning = true;
                ExecuteNextQuestion();
            }
            else if (message == GameControlMessage.GameTimeout)
            {
                IsGameRunning = false;
                CanStartGame = true;
            }
        }
    }
}