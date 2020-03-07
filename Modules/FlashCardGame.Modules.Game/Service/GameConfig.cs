using FlashCardGame.Core;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace FlashCardGame.Modules.Game.Service
{
    public class GameConfig : IGameConfig
    {
        public GameConfig()
        {
            GameDuration = TimeSpan.FromMinutes(1);
            MinValue = 0;
            MaxValue = 12;
        }

        public IArithmeticOp SelectedOp { get; set; }
        public TimeSpan GameDuration { get; set; }
        public int MinValue { get; set; }
        public int MaxValue { get; set; }
        public bool UseRandomOp { get; set; }
    }
}