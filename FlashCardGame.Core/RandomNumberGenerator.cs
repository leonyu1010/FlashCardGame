using System;
using System.Collections.Generic;
using System.Text;

namespace FlashCardGame.Core
{
    public class RandomNumberGenerator : IRandomNumberGenerator
    {
        public RandomNumberGenerator(int seed)
        {
            _generator = new Random(seed);
        }

        public RandomNumberGenerator()
        {
            _generator = new Random();
        }

        public int Number => _generator.Next(Minimum, Maximum);

        public int Minimum { get; set; } = 0;
        public int Maximum { get; set; } = Int32.MaxValue;

        public int GetOneNumber(int min, int max)
        {
            return _generator.Next(min, max);
        }

        private Random _generator;
    }
}