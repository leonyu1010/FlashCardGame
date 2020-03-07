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

            var result = op.Divide(1, 0.2);

            Assert.Equal(1 / 0.2, result);
        }
    }
}