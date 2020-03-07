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
            _gameConfig.SelectedOperator = new OperatorClass(Operator.Multiply);
            //Operators = new List<string>
            //{
            //    MaterialDesignIcons.Plus,
            //    MaterialDesignIcons.Minus,
            //    MaterialDesignIcons.Multiplication,
            //    MaterialDesignIcons.Division
            //};
        }

        public IOperatorClass SelectedOperator
        {
            get { return _selectedOperator; }
            set
            {
                SetProperty(ref _selectedOperator, value);
                _gameConfig.SelectedOperator = _selectedOperator;
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

        private readonly IEventAggregator _ea;
        private readonly IGameConfig _gameConfig;

        private IOperatorClass _selectedOperator;
        private DelegateCommand _startNewGameCommand;
        private bool _isZeroIncluded;

        private void ExecuteNewGame()
        {
            _ea.GetEvent<GameControlEvent>().Publish(GameControlMessage.Start);
        }
    }
}