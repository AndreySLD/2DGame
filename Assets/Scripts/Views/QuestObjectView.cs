using UnityEngine;

namespace Platformer
{
    public class QuestObjectView : LevelObjectView
    {
        [SerializeField] private int _id;
        [SerializeField] private Color _completedColor;
        [SerializeField] private Color _defaultColor;

        public int Id { get => _id; set => _id = value; }

        void Awake()
        {
            _defaultColor = SpriteRenderer.color;
        }

        public void ProcessComplete()
        {
            SpriteRenderer.color = _completedColor;
        }

        public void ProcessActivate()
        {
            SpriteRenderer.color = _defaultColor;
        }
    }
}
