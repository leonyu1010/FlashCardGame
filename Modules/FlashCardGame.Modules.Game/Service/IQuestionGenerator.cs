using FlashCardGame.Core;

namespace FlashCardGame.Modules.Game.Service
{
    public interface IQuestionGenerator
    {
        GameQuestion GenerateQuestion();
    }
}