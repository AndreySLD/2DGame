using UnityEngine;

namespace Platformer
{
    [CreateAssetMenu(fileName = "QuestCfg", menuName = "Configs / Quest Cfg", order = 1)]
    public class QuestConfig : ScriptableObject
    {
        public int ID;
        public QuestType TypeOfQuest;
    }

    public enum QuestType
    {
        Coins
    }
}