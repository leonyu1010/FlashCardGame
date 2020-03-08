using FlashCardGame.Core;
using System;
using System.Collections.Generic;

namespace FlashCardGame.Modules.Game.Service
{
    public class GameConfig : IGameConfig
    {
        public GameConfig()
        {
            GameDuration = TimeSpan.FromMinutes(1);
            MinValue = 0;
            MaxValue = 12;
            InitOperatorList();
        }

        public IArithmeticOp SelectedOp { get; set; }

        public TimeSpan GameDuration { get; set; }

        public int MinValue { get; set; }

        public int MaxValue { get; set; }

        public bool UseRandomOp { get; set; }

        public List<OperatorContext> Operators { get; set; }

        private void InitOperatorList()
        {
            //the operator sequence needs to math to public enum Operator
            Operators = new List<OperatorContext>
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
                    Name = Operator.Multiply,
                    Icon = MaterialDesignIcons.Multiplication,
                    Handler = new ArithmeticOp(Operator.Multiply)
                },
                new OperatorContext()
                {
                    Name = Operator.Divide,
                    Icon = MaterialDesignIcons.Division,
                    Handler = new ArithmeticOp(Operator.Divide)
                }
            };
        }
    }
}