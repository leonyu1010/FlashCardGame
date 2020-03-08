using FlashCardGame.Core;
using System;
using System.Collections.Generic;
using System.Text;

namespace FlashCardGame.Core
{
    public class OperatorContext
    {
        public IArithmeticOp Handler { get; set; }
        public Operator Name { get; set; }
        public string Icon { get; set; }

        public override string ToString()
        {
            return Name.ToString();
        }
    }
}