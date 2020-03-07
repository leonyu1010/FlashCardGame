using FlashCardGame.Core;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace FlashCardGame.Modules.Game.Service
{
    public class OperatorClassFactory
    {
        public OperatorClassFactory(IRandomNumberGenerator rng)
        {
            _rng = rng;
        }

        public IOperatorClass Create(int index)
        {
            return new OperatorClass((Operator)index);
        }

        public IOperatorClass CreateRand()
        {
            return new RandomOperatorClass(_rng);
        }

        private readonly IRandomNumberGenerator _rng;
    }
}