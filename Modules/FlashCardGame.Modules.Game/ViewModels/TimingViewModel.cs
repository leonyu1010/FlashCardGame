using FlashCardGame.Core.Constants;
using FlashCardGame.Core.Events;
using FlashCardGame.Modules.Game.Service;
using Prism.Events;
using Prism.Mvvm;
using System;
using System.Reactive.Concurrency;
using System.Reactive.Linq;

namespace FlashCardGame.Modules.Game.ViewModels
{
    public class TimingViewModel : BindableBase
    {
        public TimingViewModel(IEventAggregator ea, IGameSetting gameConfig)
        {
            _gameConfig = gameConfig;
            SecondsRemaining = (int)_gameConfig.GameDuration.TotalSeconds;

            _ea = ea;
            _ea.GetEvent<GameControlEvent>().Subscribe(HandleGameControlEvent);
        }

        public int SecondsRemaining
        {
            get { return _secondsRemaining; }
            set { SetProperty(ref _secondsRemaining, value); }
        }

        private readonly IGameSetting _gameConfig;
        private readonly IEventAggregator _ea;
        private IDisposable _gameTimer;
        private int _secondsRemaining;

        private void HandleGameControlEvent(string message)
        {
            if (message == GameControlMessage.Start)
            {
                FireTimer();
            }
            else if (message == GameControlMessage.GameTimeout)
            {
                _gameTimer?.Dispose();
            }
        }

        private void FireTimer()
        {
            if (_gameTimer != null)
            {
                _gameTimer.Dispose();
            }

            _gameTimer = Observable.Interval(TimeSpan.FromSeconds(1), Scheduler.Default).Subscribe(OnTimerEvent);
        }

        private void OnTimerEvent(long value)
        {
            SecondsRemaining = (int)_gameConfig.GameDuration.TotalSeconds - (int)value;

            if (value == _gameConfig.GameDuration.TotalSeconds)
            {
                _ea.GetEvent<GameControlEvent>().Publish(GameControlMessage.GameTimeout);
            }
        }
    }
}