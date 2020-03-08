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
        public void ArithmeticOpTests_DivideByZero()
        {
            var op = new ArithmeticOp(Operator.Division);

            Assert.Throws<DivideByZeroException>(() => op.Divide(1, AppConstants.Tolerance));
        }

        [Fact]
        public void ArithmeticOpTests_Divide_BiggerThanEps()
        {
            var op = new ArithmeticOp(Operator.Division);
            var denominator = AppConstants.Tolerance + 0.1;
            var result = op.Divide(1, denominator);

            Assert.Equal(1 / denominator, result);
        }
    }
}