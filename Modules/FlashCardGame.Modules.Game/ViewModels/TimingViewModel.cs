using FlashCardGame.Core.Constants;
using FlashCardGame.Core.Events;
using FlashCardGame.Modules.Game.Service;
using Prism.Commands;
using Prism.Events;
using Prism.Mvvm;
using Prism.Regions;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Reactive.Concurrency;
using System.Reactive.Linq;

namespace FlashCardGame.Modules.Game.ViewModels
{
    public class TimingViewModel : BindableBase
    {
        public TimingViewModel(IEventAggregator ea, IGameConfig gameConfig)
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

        private readonly IRegionManager _regionManager;
        private readonly IGameConfig _gameConfig;
        private readonly IEventAggregator _ea;

        private IDisposable _gameTimer;
        private int _secondsRemaining;

        private void HandleGameControlEvent(string message)
        {
            if (message == GameControlMessage.Start)
            {
                if (_gameTimer != null)
                {
                    _gameTimer.Dispose();
                }
                FireTimer();
            }
            else if (message == GameControlMessage.Stop)
            {
                _gameTimer?.Dispose();
            }
        }

        private void FireTimer()
        {
            _gameTimer = Observable.Interval(TimeSpan.FromSeconds(1), Scheduler.Default).Subscribe(OnTimerEvent);
        }

        private void OnTimerEvent(long value)
        {
            SecondsRemaining = (int)_gameConfig.GameDuration.TotalSeconds - (int)value;

            if (value == _gameConfig.GameDuration.TotalSeconds)
            {
                _ea.GetEvent<GameControlEvent>().Publish(GameControlMessage.Stop);
            }
        }
    }
}