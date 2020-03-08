using System;
using System.Collections.Generic;
using System.Text;
using Xunit;
using Moq;
using FlashCardGame.Core.Constants;

namespace FlashCardGame.Core.Tests
{
    public class ArithmeticOpTests
    {
        [Fact]
        public void ArithmeticOpTests_Divide_SmallThanTolerance_ShouldThrow()
        {
            var op = new ArithmeticOp(Operator.Division);
            var denominator = AppConstants.Tolerance - 1e-5;
            Assert.Throws<DivideByZeroException>(() => op.Divide(1, denominator));
        }

        [Fact]
        public void ArithmeticOpTests_Divide_EqualTolerance_Pass()
        {
            var op = new ArithmeticOp(Operator.Division);
            var denominator = AppConstants.Tolerance;
            var result = op.Divide(1, denominator);

            Assert.Equal(1 / denominator, result);
        }

        [Fact]
        public void ArithmeticOpTests_Divide_BiggerThanTolerance_Pass()
        {
            var op = new ArithmeticOp(Operator.Division);
            var denominator = AppConstants.Tolerance + 1e-5;
            var result = op.Divide(1, denominator);

            Assert.Equal(1 / denominator, result);
        }
    }
}