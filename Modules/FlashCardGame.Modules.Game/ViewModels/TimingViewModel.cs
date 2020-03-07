using FlashCardGame.Core.Constants;
using FlashCardGame.Core.Events;
using FlashCardGame.Modules.Game.Service;
using Prism.Commands;
using Prism.Events;
using Prism.Mvvm;
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

            _ea = ea;
            _ea.GetEvent<GameControlEvent>().Subscribe(HandleGameControlEvent);
        }

        public string TimeLeft
        {
            get
            {
                return _timeleft;
            }
            set
            {
                SetProperty(ref _timeleft, value);
            }
        }

        private readonly IGameConfig _gameConfig;
        private readonly IEventAggregator _ea;

        private string _timeleft;
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
            _secondsRemaining = (int)_gameConfig.GameDuration.TotalSeconds - (int)value;
            UpdateUI();

            if (value == _gameConfig.GameDuration.TotalSeconds)
            {
                _ea.GetEvent<GameControlEvent>().Publish(GameControlMessage.Stop);
            }
        }

        private void UpdateUI()
        {
            TimeLeft = $"you have {_secondsRemaining} seconds left";
        }
    }
}