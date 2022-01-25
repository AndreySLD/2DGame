using UnityEngine;

namespace Platformer 
{
    public class CoinQuestModel : IQuestModel
    {
        private const string _tag = "Player";
        public bool TryComplete(GameObject actor)
        {
            return actor.CompareTag(_tag);
        }
    } 
}
