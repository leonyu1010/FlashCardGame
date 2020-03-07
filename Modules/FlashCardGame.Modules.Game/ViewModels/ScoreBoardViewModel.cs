using FlashCardGame.Core.Events;
using Prism.Commands;
using Prism.Events;
using Prism.Mvvm;
using System;
using System.Collections.Generic;
using System.Linq;

namespace FlashCardGame.Modules.Game.ViewModels
{
    public class ScoreBoardViewModel : BindableBase
    {
        public ScoreBoardViewModel(IEventAggregator ea)
        {
            _ea = ea;
            _ea.GetEvent<UpdateScoreEvent>().Subscribe(UpdateScore);
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

        //public MessageListViewModel(IEventAggregator ea)
        //{
        //    _ea = ea;
        //    Messages = new ObservableCollection<string>();

        //    _ea.GetEvent<MessageSentEvent>().Subscribe(MessageReceived);
        //}
    }
}