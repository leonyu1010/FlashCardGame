﻿using System;
using System.Collections.Generic;
using System.Text;

namespace FlashCardGame.Core
{
    public class ArithmeticOp : IArithmeticOp
    {
        public ArithmeticOp(Operator op)
        {
            Name = op;
        }

        public Operator Name { get; }

        public int Calculate(NumberPair pair)
        {
            int number1 = pair.Number1;
            int number2 = pair.Number2;

            switch (Name)
            {
                case Operator.Plus:
                    return number1 + number2;

                case Operator.Minus:
                    return number1 - number2;

                case Operator.Multiply:
                    return number1 * number2;

                case Operator.Divide:
                    {
                        if (number2 == 0)
                        {
                            throw new DivideByZeroException();
                        }
                        return number1 / number2;
                    }
                default:
                    throw new Exception("unexpected operator");
            }
        }

        public string ToSign()
        {
            switch (Name)
            {
                case Operator.Plus:
                    return "+";

                case Operator.Minus:
                    return "-";

                case Operator.Multiply:
                    return "*";

                case Operator.Divide:
                    return "/";

                default:
                    throw new Exception("unexpected operator");
            }
        }

        public bool IsValid(NumberPair pair)
        {
            if (Name == Operator.Divide && pair.Number2 == 0)
            {
                return false;
            }
            return true;
        }

        public double Divide(double numerator, double denominator)
        {
            if (Math.Abs(denominator) < eps)
            {
                throw new DivideByZeroException();
            }
            return numerator / denominator;
        }

        private readonly double eps = 1e-6;
    }
}