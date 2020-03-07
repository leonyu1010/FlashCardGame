using FlashCardGame.Core;
using FlashCardGame.Core.Constants;
using FlashCardGame.Core.Events;
using FlashCardGame.Model;
using FlashCardGame.Modules.Game.Service;
using Prism.Commands;
using Prism.Events;
using Prism.Mvvm;
using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Linq;

namespace FlashCardGame.Modules.Game.ViewModels
{
    public class GameViewModel : BindableBase
    {
        public GameViewModel(IGameConfig gameConfig, IEventAggregator ea)
        {
            _ea = ea;
            _gameConfig = gameConfig;

            _isZeroIncluded = true;

            Operators = _gameConfig.Operators;
            SelectedOp = Operators[2];
        }

        public OperatorContext SelectedOp
        {
            get { return _selectedOp; }
            set
            {
                SetProperty(ref _selectedOp, value);
                _gameConfig.SelectedOp = _selectedOp.Handler;
            }
        }

        public List<OperatorContext> Operators
        {
            get { return _operators; }
            set
            {
                SetProperty(ref _operators, value);
            }
        }

        public DelegateCommand StartNewGameCommand =>
            _startNewGameCommand ?? (_startNewGameCommand = new DelegateCommand(ExecuteNewGame));

        public bool IsZeroIncluded
        {
            get { return _isZeroIncluded; }
            set
            {
                SetProperty(ref _isZeroIncluded, value);
                _gameConfig.MinValue = _isZeroIncluded ? 0 : 1;
            }
        }

        public bool UseRandomOp
        {
            get { return _useRandomOp; }
            set
            {
                SetProperty(ref _useRandomOp, value);
                _gameConfig.UseRandomOp = _useRandomOp;
            }
        }

        private readonly IEventAggregator _ea;

        private readonly IGameConfig _gameConfig;

        private bool _useRandomOp;

        private List<OperatorContext> _operators;

        private OperatorContext _selectedOp;

        private DelegateCommand _startNewGameCommand;

        private bool _isZeroIncluded;

        private void ExecuteNewGame()
        {
            _ea.GetEvent<GameControlEvent>().Publish(GameControlMessage.Start);
        }
    }
}