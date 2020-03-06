using System;

namespace FlashCardGame.Core
{
    public interface IRandomNumberGenerator
    {
        int Number { get; }
        int Minimum { get; set; }
        int Maximum { get; set; }

        int GetOneNumber(int min, int max);
    }
}