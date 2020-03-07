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
        public GameConfig(IRandomNumberGenerator rng)
        {
            _rng = rng;
            GameDuration = TimeSpan.FromMinutes(1);
            MaxValue = 12;
        }

        public IOperatorClass SelectedOperator { get; set; }
        public TimeSpan GameDuration { get; set; }
        public int MinValue { get; set; }
        public int MaxValue { get; set; }
        private readonly IRandomNumberGenerator _rng;
    }
}