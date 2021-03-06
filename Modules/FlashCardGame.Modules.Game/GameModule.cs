﻿using FlashCardGame.Modules.Game.Views;
using Prism.Ioc;
using Prism.Modularity;
using Prism.Regions;
using FlashCardGame.Modules.Game.Service;

namespace FlashCardGame.Modules.Game
{
    public class GameModule : IModule
    {
        public GameModule(IRegionManager regionManager)
        {
            _regionManager = regionManager;
        }

        public void OnInitialized(IContainerProvider containerProvider)
        {
        }

        public void RegisterTypes(IContainerRegistry containerRegistry)
        {
            containerRegistry.RegisterSingleton<IGameSetting, GameSetting>();
            containerRegistry.Register<IQuestionGenerator, QuestionGenerator>();

            _regionManager.RegisterViewWithRegion("ContentRegion", typeof(GameView));
            _regionManager.RegisterViewWithRegion("SettingRegion", typeof(GameSettingView));
            _regionManager.RegisterViewWithRegion("QuestionRegion", typeof(QuestionView));
            _regionManager.RegisterViewWithRegion("ScoreBoardRegion", typeof(ScoreBoardView));
            _regionManager.RegisterViewWithRegion("TimingRegion", typeof(TimingView));
            _regionManager.RegisterViewWithRegion("FeedbackRegion", typeof(AnswerFeedbackView));
        }

        private readonly IRegionManager _regionManager;
    }
}