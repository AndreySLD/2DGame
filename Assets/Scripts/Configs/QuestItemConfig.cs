using UnityEngine;
using System.Collections.Generic;

namespace Platformer
{
    [CreateAssetMenu(fileName = "QuestItemCfg", menuName = "Configs / Quest Item Cfg", order = 1)]
    public class QuestItemConfig : ScriptableObject
    {
        public int QuestID;
        public List<int> QuestItemList;
    }
}
