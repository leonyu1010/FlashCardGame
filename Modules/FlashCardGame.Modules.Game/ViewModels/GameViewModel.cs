using FlashCardGame.Core;
using FlashCardGame.Model;
using FlashCardGame.Modules.Game.Service;
using Prism.Commands;
using Prism.Mvvm;
using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Linq;

namespace FlashCardGame.Modules.Game.ViewModels
{
    public class GameViewModel : BindableBase
    {
        public GameViewModel(IQuestionGenerator questionGenerator, IGameConfig gameConfig)
        {
            _gameConfig = gameConfig;

            _questionGenerator = questionGenerator;
            _isZeroIncluded = false;
            //Operators = new List<string>
            //{
            //    MaterialDesignIcons.Plus,
            //    MaterialDesignIcons.Minus,
            //    MaterialDesignIcons.Multiplication,
            //    MaterialDesignIcons.Division
            //};

            Operators = new ObservableCollection<string>
            {
                Operator.Plus.ToSign(),
                Operator.Minus.ToSign(),
                Operator.Multiply.ToSign(),
                Operator.Divide.ToSign(),
                Operator.Random.ToSign(),
            };
        }

        public ObservableCollection<string> Operators { get; private set; }

        public string SelectedOperator
        {
            get { return _selectedOperator; }
            set
            {
                SetProperty(ref _selectedOperator, value);
                _gameConfig.SelectedOperator = OperatorExtensions.FromSign(_selectedOperator);
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

        private readonly IGameConfig _gameConfig;
        private readonly IQuestionGenerator _questionGenerator;
        private string _selectedOperator;
        private DelegateCommand _startNewGameCommand;
        private bool _isZeroIncluded;

        private void ExecuteNewGame()
        {
            var op = OperatorExtensions.FromSign(SelectedOperator);
        }
    }
}