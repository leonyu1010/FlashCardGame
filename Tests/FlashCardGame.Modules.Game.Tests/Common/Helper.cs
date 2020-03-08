using System;
using System.Collections.Generic;
using System.Text;
using FlashCardGame.Core;
using FlashCardGame.Modules.Game.Service;
using Moq;

namespace FlashCardGame.Modules.Game.Tests.Common
{
    public static class Helper
    {
        public static Mock<IGameSetting> SetupConfigMock(Operator op, int min, int max)
        {
            var configMock = new Mock<IGameSetting>();
            configMock.Setup(configMock => configMock.MinValueInQuestion).Returns(min);
            configMock.Setup(configMock => configMock.MaxValueInQuestion).Returns(max);
            configMock.Setup(configMock => configMock.SelectedOp).Returns(new ArithmeticOp(op));
            configMock.Setup(configMock => configMock.Operators).Returns(new List<OperatorContext>
            {
                new OperatorContext()
                {
                    Name = Operator.Plus,
                    Icon = MaterialDesignIcons.Plus,
                    Handler = new ArithmeticOp(Operator.Plus)
                },
                new OperatorContext()
                {
                    Name = Operator.Minus,
                    Icon = MaterialDesignIcons.Minus,
                    Handler = new ArithmeticOp(Operator.Minus)
                },
                new OperatorContext()
                {
                    Name = Operator.Multiplication,
                    Icon = MaterialDesignIcons.Multiplication,
                    Handler = new ArithmeticOp(Operator.Multiplication)
                },
                new OperatorContext()
                {
                    Name = Operator.Division,
                    Icon = MaterialDesignIcons.Division,
                    Handler = new ArithmeticOp(Operator.Division)
                }
            });
            return configMock;
        }

        public static Mock<IGameSetting> SetupConfigMock(int operatorIndex, int min, int max)
        {
            return SetupConfigMock((Operator)operatorIndex, min, max);
        }
    }
}