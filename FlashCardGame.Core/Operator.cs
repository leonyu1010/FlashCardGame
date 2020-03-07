using System;
using System.Collections.Generic;
using System.Text;

namespace FlashCardGame.Core
{
    public enum Operator
    {
        Plus = 0,
        Minus = 1,
        Multiply = 2,
        Divide = 3
    }

    public static class OperatorExtensions
    {
        public static string ToSign(this Operator op)
        {
            switch (op)
            {
                case Operator.Plus: return "+";
                case Operator.Minus: return "-";
                case Operator.Multiply: return "x";
                case Operator.Divide: return "/";
                default: throw new ArgumentOutOfRangeException("operator");
            }
        }

        //    public static Operator FromSign(string sign)
        //    {
        //        switch (sign)
        //        {
        //            case "+": return Operator.Plus;
        //            case "-": return Operator.Minus;
        //            case "x": return Operator.Multiply;
        //            case "/": return Operator.Divide;
        //            case "try my luck": return Operator.Random;
        //            default: throw new ArgumentOutOfRangeException("operator-sign");
        //        }
        //    }
    }
}