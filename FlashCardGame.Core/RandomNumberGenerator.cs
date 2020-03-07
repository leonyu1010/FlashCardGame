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

        public int Number => _generator.Next(_minimum, _maximum);

        public int Minimum { get { return _minimum; } set => _minimum = value; }
        public int Maximum { get { return _maximum; } set => _maximum = value; }

        public int GetOneNumber(int min, int max)
        {
            return _generator.Next(min, max);
        }

        private Random _generator;
        private int _minimum = 0;
        private int _maximum = Int32.MaxValue;
    }
}