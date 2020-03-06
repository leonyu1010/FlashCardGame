using Prism.Commands;
using Prism.Mvvm;
using System;
using System.Collections.Generic;
using System.Linq;

namespace FlashCardGame.Modules.Game.ViewModels
{
    public class QuestionViewModel : BindableBase
    {
        public QuestionViewModel()
        {
            _questionText = "My Question";

        }

        public string QuestionText
        {
            get { return _questionText; }
            set { SetProperty(ref _questionText, value); }
        }

        public string AnswerText
        {
            get { return _answerText; }
            set { SetProperty(ref _answerText, value); }
        }

        private string _questionText;
        private string _answerText;
    }
}