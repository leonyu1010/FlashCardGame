using System;
using System.Collections.Generic;
using System.Text;
using Xunit;
using Moq;

namespace FlashCardGame.Core.Tests
{
    public class ArithmeticOpTests
    {
        [Fact]
        public void ArithmeticOpTests_DivideByZero()
        {
            var op = new ArithmeticOp(Operator.Divide);

            Assert.Throws<DivideByZeroException>(() => op.Divide(1, 1e-7));
        }

        [Fact]
        public void ArithmeticOpTests_Divide_BiggerThanEps()
        {
            var op = new ArithmeticOp(Operator.Divide);
            var denominator = 0.0001;
            var result = op.Divide(1, denominator);

            Assert.Equal(1 / denominator, result);
        }
    }
}