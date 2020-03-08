using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace FlashCardGame.Core
{
    public class NumberPair
    {
        public int Number1 { get; set; }
        public int Number2 { get; set; }

        public override string ToString()
        {
            return $"({Number1},{Number2})";
        }
    }
}