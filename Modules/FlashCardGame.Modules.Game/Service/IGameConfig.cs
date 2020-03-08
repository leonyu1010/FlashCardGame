using FlashCardGame.Core;
using System;
using System.Collections.Generic;

namespace FlashCardGame.Modules.Game.Service
{
    public interface IGameSetting
    {
        IArithmeticOp SelectedOp { get; set; }
        List<OperatorContext> Operators { get; set; }
        TimeSpan GameDuration { get; set; }
        int MinValueInQuestion { get; set; }
        int MaxValueInQuestion { get; set; }
        bool UseRandomOp { get; set; }
    }
}