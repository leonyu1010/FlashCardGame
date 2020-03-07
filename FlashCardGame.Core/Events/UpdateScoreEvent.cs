using Prism.Events;
using System;
using System.Collections.Generic;
using System.Text;

namespace FlashCardGame.Core.Events
{
    public class UpdateScoreEvent : PubSubEvent<int>
    {
    }
}