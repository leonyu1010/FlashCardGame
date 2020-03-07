using System;
using System.Collections.Generic;
using System.Text;

namespace FlashCardGame.Core
{
    public class RandomArithmeticOp : IArithmeticOp
    {
        public RandomArithmeticOp(IRandomNumberGenerator rng)
        {
            _op = new ArithmeticOp((Operator)rng.GetOneNumber(0, 4));
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

        public double Divide(double numerator, double denominator)
        {
            return _op.Divide(numerator, denominator);
        }

        private readonly ArithmeticOp _op;
    }
}