using FlashCardGame.Core;
using System;
using System.Collections.Generic;
using System.Text;

namespace FlashCardGame.Model
{
    public class GameQuestion
    {
        public string Question { get; set; }
        public string CorrectAnswer { get; set; }
        public NumberPair Pair { get; set; }
        public OperatorItem Op { get; set; }
    }
}