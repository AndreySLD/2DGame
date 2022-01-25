using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace Platformer
{
    [CreateAssetMenu(fileName = "QuestStoryCfg", menuName = "Configs / Quest Story Cfg", order = 1)]
    public class QuestStoryConfig : ScriptableObject
    {
        public QuestConfig[] Quests;
        public QuestStoryType Type;
    }

    public enum QuestStoryType
    {
        Common,
        Resettable
    }
}