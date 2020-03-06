using System;
using System.Collections.Generic;

namespace FlashCardGame.Model
{
    public class GameSession
    {
        public int SessionId { get; set; }
        public int PlayId { get; set; }
        public int Score { get; set; }
        public DateTimeOffset StartTime { get; set; }
        public TimeSpan Duration { get; set; }
        public List<GameQuestion> Questions { get; set; }
    }
}