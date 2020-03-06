using FlashCardGame.Core;
using FlashCardGame.Model;
using System;
using System.Collections.Generic;
using System.Text;

namespace FlashCardGame.Modules.Game.Service
{
    public class QuestionGenerator : IQuestionGenerator
    {
        public QuestionGenerator(IRandomNumberGenerator rng, IGameConfig gameConfig)
        {
            _rng = rng;
            _gameConfig = gameConfig;
        }

        public GameQuestion GenerateQuestion()
        {
            int value1 = GetFirstNumber();
            int value2 = GetSecondNumber();

            return new GameQuestion()
            {
                Question = $"{value1} {_gameConfig.SelectedOperator.ToSign()} {value2}",
                CorrectAnswer = Calculate(value1, value2, _gameConfig.SelectedOperator)
            };
        }

        private readonly IGameConfig _gameConfig;
        private readonly IRandomNumberGenerator _rng;
        private string _newQuestion;

        private int GetSecondNumber()
        {
            return _rng.Number;
        }

        private int GetFirstNumber()
        {
            return _rng.Number;
        }

        private string Calculate(int value1, int value2, Operator currentOperator)
        {
            switch (currentOperator)
            {
                case Operator.Multiply:
                    return (value1 * value2).ToString();

                default:
                    throw new Exception("unknow Operator to Calculate");
            }
        }

        //                    if (_op == Operator.Random)
        //            {
        //                GetRandomOperator();
        //}
        private Operator GetRandomOperator()
        {
            return (Operator)_rng.GetOneNumber(0, (int)Operator.Random - 1);
        }
    }
}