using FlashCardGame.Core;
using FlashCardGame.Core.Constants;
using FlashCardGame.Core.Events;

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
    public class GameSettingViewModel : BindableBase
    {
        public GameSettingViewModel(IGameSetting gameConfig)
        {
            _gameConfig = gameConfig;

            IncludeZero = _gameConfig.MinValueInQuestion == 0;
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
            set { SetProperty(ref _operators, value); }
        }

        public bool IncludeZero
        {
            get { return _includeZero; }
            set
            {
                SetProperty(ref _includeZero, value);
                _gameConfig.MinValueInQuestion = _includeZero ? 0 : 1;
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

        private readonly IGameSetting _gameConfig;
        private bool _useRandomOp;
        private List<OperatorContext> _operators;
        private OperatorContext _selectedOp;
        private bool _includeZero;
    }
}