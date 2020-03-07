using FlashCardGame.Core;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace FlashCardGame.Modules.Game.Service
{
    public class ArithmeticOpFactory
    {
        public ArithmeticOpFactory(IRandomNumberGenerator rng)
        {
            _rng = rng;
        }

        public IArithmeticOp Create(int index)
        {
            return new ArithmeticOp((Operator)index);
        }

        public IArithmeticOp CreateRand()
        {
            return new RandomArithmeticOp(_rng);
        }

        private readonly IRandomNumberGenerator _rng;
    }
}