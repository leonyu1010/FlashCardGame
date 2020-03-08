using FlashCardGame.Core;
using System;
using System.Collections.Generic;
using System.Linq;

namespace FlashCardGame.Modules.Game.Service
{
    public class QuestionGenerator : IQuestionGenerator
    {
        public QuestionGenerator(IRandomNumberGenerator rng, IGameSetting gameConfig)
        {
            _rng = rng;
            _gameConfig = gameConfig;

            Reset();
        }

        public GameQuestion GenerateQuestion()
        {
            var numOfOperator = Enum.GetNames(typeof(Operator)).Length;
            var op = _gameConfig.UseRandomOp ?
                new ArithmeticOp((Operator)_rng.GetOneNumber(0, numOfOperator))
                : _gameConfig.SelectedOp;

            NumberPair pair;
            int loop = 0;
            while (true)
            {
                pair = _pool[_indexOfNextPair];
                UpdateIndexOfNextPair();
                if (IsPairValid(pair, op))
                {
                    break;
                }

                ++loop;
                if (loop == _pool.Count)
                {
                    throw new Exception("cannot find a valid number pair");
                }
            }

            return new GameQuestion()
            {
                Pair = pair,
                OpCtx = _gameConfig.Operators[(int)op.Name]
            };
        }

        private readonly IGameSetting _gameConfig;

        private readonly IRandomNumberGenerator _rng;

        private List<NumberPair> _pool;

        private int _indexOfNextPair;

        private void Reset()
        {
            CreatePoolOfAllNumberPairs();
            ShufflePool();
            _indexOfNextPair = 0;
        }

        private bool IsPairValid(NumberPair pair, IArithmeticOp op)
        {
            if (_gameConfig.MinValueInQuestion != 0)
            {
                if (pair.Number1 == 0 || pair.Number2 == 0)
                {
                    return false;
                }
            }

            return op.IsValid(pair);
        }

        private void CreatePoolOfAllNumberPairs()
        {
            _pool = new List<NumberPair>();
            int min = _gameConfig.MinValueInQuestion;
            int max = _gameConfig.MaxValueInQuestion;

            foreach (var number1 in Enumerable.Range(min, max + 1))
            {
                foreach (var number2 in Enumerable.Range(min, max + 1))
                {
                    _pool.Add(new NumberPair { Number1 = number1, Number2 = number2 });
                }
            }
        }

        private void UpdateIndexOfNextPair()
        {
            ++_indexOfNextPair;
            if (_indexOfNextPair == _pool.Count)
            {
                _indexOfNextPair = 0;
            }
        }

        /// <summary>
        /// Fisher–Yates shuffle
        /// https://en.wikipedia.org/wiki/Fisher%E2%80%93Yates_shuffle
        /// </summary>
        private void ShufflePool()
        {
            int n = _pool.Count;
            while (n > 1)
            {
                --n;
                int k = _rng.GetOneNumber(0, n + 1);
                var value = _pool[k];
                _pool[k] = _pool[n];
                _pool[n] = value;
            }
        }
    }
}