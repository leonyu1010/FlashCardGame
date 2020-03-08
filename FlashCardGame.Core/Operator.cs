using System;
using System.Collections.Generic;
using System.Text;

namespace FlashCardGame.Core
{
    public enum Operator
    {
        Plus = 0,
        Minus = 1,
        Multiplication = 2,
        Division = 3
    }

    public static class OperatorExtensions
    {
        public static string ToSign(this Operator op)
        {
            switch (op)
            {
                case Operator.Plus: return "+";
                case Operator.Minus: return "-";
                case Operator.Multiplication: return "x";
                case Operator.Division: return "/";
                default: throw new ArgumentOutOfRangeException("operator");
            }
        }
    }
}