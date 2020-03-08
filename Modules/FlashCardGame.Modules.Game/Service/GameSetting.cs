using FlashCardGame.Core;
using System;
using System.Collections.Generic;

namespace FlashCardGame.Modules.Game.Service
{
    public class GameSetting : IGameSetting
    {
        public GameSetting()
        {
            GameDuration = TimeSpan.FromMinutes(1);
            MinValueInQuestion = 0;
            MaxValueInQuestion = 12;
            InitOperatorList();
        }

        public IArithmeticOp SelectedOp { get; set; }

        public TimeSpan GameDuration { get; set; }

        public int MinValueInQuestion { get; set; }

        public int MaxValueInQuestion { get; set; }

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
            };
        }
    }
}