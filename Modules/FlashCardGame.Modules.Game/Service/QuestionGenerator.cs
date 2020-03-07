using FlashCardGame.Core;
using FlashCardGame.Model;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace FlashCardGame.Modules.Game.Service
{
    public class QuestionGenerator : IQuestionGenerator
    {
        public QuestionGenerator(IRandomNumberGenerator rng, IGameConfig gameConfig)
        {
            _rng = rng;
            _gameConfig = gameConfig;

            _useRandomOp = _gameConfig.UseRandomOp;
            if (!_useRandomOp)
            {
                _op = _gameConfig.SelectedOp;
            }
            Reset();
        }

        public GameQuestion GenerateQuestion()
        {
            NumberPair pair;
            if (_useRandomOp)
            {
                _op = new ArithmeticOp((Operator)_rng.GetOneNumber(0, 4));
            }
            while (true)
            {
                pair = _pool[_indexOfNextPair];
                UpdateIndexOfNextPair();
                if (_op.IsValid(pair))
                {
                    break;
                }
            }

            return new GameQuestion()
            {
                Question = $"{pair.Number1} {_op.ToSign()} {pair.Number2}",
                CorrectAnswer = _op.Calculate(pair).ToString()
            };
        }

        public void Reset()
        {
            CreatePoolOfNumberPair();
            ShufflePool();
            _indexOfNextPair = 0;
        }

        private readonly bool _useRandomOp;
        private readonly IGameConfig _gameConfig;
        private readonly IRandomNumberGenerator _rng;
        private IArithmeticOp _op;
        private List<NumberPair> _pool;

        private int _indexOfNextPair;

        private void CreatePoolOfNumberPair()
        {
            _pool = new List<NumberPair>();
            int min = _gameConfig.MinValue;
            int max = _gameConfig.MaxValue;

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
            _indexOfNextPair++;
            if (_indexOfNextPair == _pool.Count)
            {
                _indexOfNextPair = 0;
            }
        }

        private void ShufflePool()
        {
            int n = _pool.Count;
            while (n > 1)
            {
                n--;
                int k = _rng.GetOneNumber(0, n + 1);
                var value = _pool[k];
                _pool[k] = _pool[n];
                _pool[n] = value;
            }
        }
    }
}