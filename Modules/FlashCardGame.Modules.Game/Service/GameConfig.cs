using FlashCardGame.Core;
using FlashCardGame.Model;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

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

        public List<OperatorItem> Operators { get; set; }

        private void InitOperatorList()
        {
            //the operator sequence needs to math to public enum Operator
            Operators = new List<OperatorItem>
            {
                new OperatorItem()
                {
                    Name = Operator.Plus,
                    Icon = MaterialDesignIcons.Plus,
                    Op = new ArithmeticOp(Operator.Plus)
                },
                new OperatorItem()
                {
                    Name = Operator.Minus,
                    Icon = MaterialDesignIcons.Minus,
                    Op = new ArithmeticOp(Operator.Minus)
                },
                new OperatorItem()
                {
                    Name = Operator.Multiply,
                    Icon = MaterialDesignIcons.Multiplication,
                    Op = new ArithmeticOp(Operator.Multiply)
                },
                new OperatorItem()
                {
                    Name = Operator.Divide,
                    Icon = MaterialDesignIcons.Division,
                    Op = new ArithmeticOp(Operator.Divide)
                }
            };
        }
    }
}