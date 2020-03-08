using FlashCardGame.Core.Constants;
using FlashCardGame.Core.Events;
using FlashCardGame.Modules.Game.Service;
using Prism.Events;
using Prism.Mvvm;

namespace FlashCardGame.Modules.Game.ViewModels
{
    public class ScoreBoardViewModel : BindableBase
    {
        public ScoreBoardViewModel(IEventAggregator ea)
        {
            _ea = ea;
            _ea.GetEvent<UpdateScoreEvent>().Subscribe(UpdateScore);
            _ea.GetEvent<GameControlEvent>().Subscribe(HandleGameControlMessage);
        }

        public int TotalScore
        {
            get { return _totalScore; }
            set { SetProperty(ref _totalScore, value); }
        }

        public int CorrectCount
        {
            get { return _correctCount; }
            set { SetProperty(ref _correctCount, value); }
        }

        public int WrongCount
        {
            get { return _wrongCount; }
            set { SetProperty(ref _wrongCount, value); }
        }

        private readonly IEventAggregator _ea;
        private int _totalScore;
        private int _correctCount;
        private int _wrongCount;

        private void HandleGameControlMessage(string message)
        {
            if (message == GameControlMessage.Start)
            {
                TotalScore = 0;
                CorrectCount = 0;
                WrongCount = 0;
            }
            else if (message == GameControlMessage.GameTimeout)
            {
                FileLogger.Singleton.Information("GameResult score [{score}] correct [{correct}] wrong [{wrong}]", TotalScore, CorrectCount, WrongCount);
            }
        }

        private void UpdateScore(int score)
        {
            TotalScore += score;
            if (score > 0)
            {
                CorrectCount += 1;
            }
            else
            {
                WrongCount += 1;
            }
        }
    }
}