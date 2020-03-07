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

            InitOperatorSelection();

            SelectedOp = Operators[2];
        }

        public OperatorItem SelectedOp
        {
            get { return _selectedOp; }
            set
            {
                SetProperty(ref _selectedOp, value);
                _gameConfig.SelectedOp = _selectedOp.Op;
            }
        }

        public List<OperatorItem> Operators
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

        private List<OperatorItem> _operators;

        private OperatorItem _selectedOp;

        private DelegateCommand _startNewGameCommand;

        private bool _isZeroIncluded;

        private void InitOperatorSelection()
        {
            Operators = new List<OperatorItem>
            {
                new OperatorItem()
                {
                    Name = Operator.Plus,
                    Icon = MaterialDesignIcons.Plus,
                    Op = new ArithmeticOp(Operator.Plus)
                },
                new OperatorItem()
                {
                    Name = Operator.Minus,
                    Icon = MaterialDesignIcons.Minus,
                    Op = new ArithmeticOp(Operator.Minus)
                },
                new OperatorItem()
                {
                    Name = Operator.Multiply,
                    Icon = MaterialDesignIcons.Multiplication,
                    Op = new ArithmeticOp(Operator.Multiply)
                },
                new OperatorItem()
                {
                    Name = Operator.Divide,
                    Icon = MaterialDesignIcons.Division,
                    Op = new ArithmeticOp(Operator.Divide)
                }
            };
        }

        private void ExecuteNewGame()
        {
            _ea.GetEvent<GameControlEvent>().Publish(GameControlMessage.Start);
        }
    }
}