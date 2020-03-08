using FlashCardGame.Core;
using System;
using System.Collections.Generic;
using System.Text;

namespace FlashCardGame.Core
{
    public class GameQuestion
    {
        public NumberPair Pair { get; set; }
        public OperatorContext OpCtx { get; set; }

        public override string ToString()
        {
            return $"{Pair.Number1} {OpCtx.Name.ToSign()} {Pair.Number2}";
        }
    }
}