using FlashCardGame.Core;
using System;
using System.Collections.Generic;
using System.Text;

namespace FlashCardGame.Modules.Game.Service
{
    public interface IGameConfig
    {
        IOperatorClass SelectedOperator { get; set; }
        TimeSpan GameDuration { get; set; }
        int MinValue { get; set; }
        int MaxValue { get; set; }
    }
}