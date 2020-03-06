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
        private readonly IRandomNumberGenerator _rng;
        public GameConfig(IRandomNumberGenerator rng)
        {
            _rng = rng;
            GameDuration = TimeSpan.FromMinutes(1);
            MaxValue = 12;
        }

        public Operator SelectedOperator { get; set; }
        public TimeSpan GameDuration { get; set; }
        public int MinValue { get; set; }
        public int MaxValue { get; set; }
    }
}