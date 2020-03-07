using System;
using System.Collections.Generic;
using System.Text;

namespace FlashCardGame.Core
{
    public class RandomOperatorClass : IOperatorClass
    {
        public RandomOperatorClass(IRandomNumberGenerator rng)
        {
            _op = new OperatorClass((Operator)_rng.GetOneNumber(0, 4));
        }

        public string ToSign()
        {
            return _op.ToSign();
        }

        public int Calculate(NumberPair pair)
        {
            return _op.Calculate(pair);
        }

        public bool IsValid(NumberPair pair)
        {
            return _op.IsValid(pair);
        }

        private IRandomNumberGenerator _rng;
        private OperatorClass _op;
    }
}