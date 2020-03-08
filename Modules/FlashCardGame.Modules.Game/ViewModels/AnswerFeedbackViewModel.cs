using FlashCardGame.Core;
using FlashCardGame.Core.Events;
using Prism.Commands;
using Prism.Events;
using Prism.Mvvm;
using System;
using System.Collections.Generic;
using System.Linq;

namespace FlashCardGame.Modules.Game.ViewModels
{
    public class AnswerFeedbackViewModel : BindableBase
    {
        public AnswerFeedbackViewModel(IEventAggregator ea)
        {
            _ea = ea;
            _ea.GetEvent<SendAnswerResultEvent>().Subscribe(UpdateFeedback);

            Icon = MaterialDesignIcons.PointingLeft;
            Feedback = "Game Time!";
        }

        public string Icon
        {
            get { return _icon; }
            set { SetProperty(ref _icon, value); }
        }

        public string Feedback
        {
            get { return _feedback; }
            set { SetProperty(ref _feedback, value); }
        }

        private readonly IEventAggregator _ea;
        private string _feedback;
        private string _icon;

        private void UpdateFeedback(bool correct)
        {
            Feedback = correct ? "Great job! Keep it Up" : "Let's try again";
            Icon = correct ? MaterialDesignIcons.Tick : MaterialDesignIcons.Error;
        }
    }
}