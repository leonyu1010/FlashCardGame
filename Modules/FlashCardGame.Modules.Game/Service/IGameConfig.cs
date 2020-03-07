using FlashCardGame.Core;
using FlashCardGame.Model;
using System;
using System.Collections.Generic;
using System.Text;

namespace FlashCardGame.Modules.Game.Service
{
    public interface IGameConfig
    {
        IArithmeticOp SelectedOp { get; set; }
        List<OperatorContext> Operators { get; set; }
        TimeSpan GameDuration { get; set; }
        int MinValue { get; set; }
        int MaxValue { get; set; }
        bool UseRandomOp { get; set; }
    }
}