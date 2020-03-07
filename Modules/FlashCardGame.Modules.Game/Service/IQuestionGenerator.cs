using FlashCardGame.Model;
using System;
using System.Collections.Generic;
using System.Text;

namespace FlashCardGame.Modules.Game.Service
{
    public interface IQuestionGenerator
    {
        GameQuestion GenerateQuestion();

        void Reset();
    }
}