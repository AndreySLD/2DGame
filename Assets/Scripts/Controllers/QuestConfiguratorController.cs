using System;
using System.Linq;
using System.Collections.Generic;
using UnityEngine;

namespace Platformer
{
    public class QuestConfiguratorController
    {
        private QuestObjectView _singleQuestView;
        private QuestController _singleQuest;
        private CoinQuestModel _coinQuestModel;

        private QuestStoryConfig[] _questStoryConfigs;
        private QuestObjectView[] _questObjectViews;

        private List<IQuestStory> _questStories;

        public QuestConfiguratorController(QuestView questView)
        {
            _singleQuestView = questView._singleQuest;
            _coinQuestModel = new CoinQuestModel();

            _questStoryConfigs = questView._questStoryConfigs;
            _questObjectViews = questView._questObjectViews;
        }

        private readonly Dictionary<QuestType, Func<IQuestModel>> _questFactiories = 
            new Dictionary<QuestType, Func<IQuestModel>>(1);

        private readonly Dictionary<QuestStoryType, Func<List<IQuest>, IQuestStory>> _questStoryFactories =
            new Dictionary<QuestStoryType, Func<List<IQuest>, IQuestStory>>(2);

        public void Init()
        {
            _singleQuest = new QuestController(_singleQuestView, _coinQuestModel);
            _singleQuest.Reset();

            _questFactiories.Add(QuestType.Coins, () => new CoinQuestModel());
            _questStoryFactories.Add(QuestStoryType.Common, questCollection => new QuestStoryController(questCollection));
            _questStories = new List<IQuestStory>();

            foreach (QuestStoryConfig questStoryConfig in _questStoryConfigs)
            {
                //_questStories.Add();
            }
        }
        private IQuestStory CreateQuestStory(QuestStoryConfig cfg)
        {
            List<IQuest> quests = new List<IQuest>();

            foreach (QuestConfig questConfig in cfg.Quests)
            {
                IQuest quest = CreateQuest(questConfig);
                if (quest == null) continue;
                quests.Add(quest);
                Debug.Log("Quest Added");
            }

            return _questStoryFactories[cfg.Type].Invoke(quests);
        }

        private IQuest CreateQuest(QuestConfig config)
        {
            int questID = config.ID;

            QuestObjectView questView = _questObjectViews.FirstOrDefault(value => value.Id == config.ID);
            if(questView == null)
            {
                Debug.Log("No views");
                return null;
            }
            if(_questFactiories.TryGetValue(config.TypeOfQuest, out var factory))
            {
                IQuestModel questModel = factory.Invoke();
                return new QuestController(questView, questModel);
            }
            Debug.Log("No Model");
            return null;
        }
    }
}   
