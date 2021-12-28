using System;
using UnityEngine;

namespace Platformer
{
    public class LevelObjectView : MonoBehaviour
    {
        public Transform Transform;
        public SpriteRenderer SpriteRenderer;
        public Collider2D Collider;
        public Rigidbody2D Rigidbody;

        public Action<LevelObjectView> OnLevelObjectContact { get; set; }

        private void OnTriggerEnter2D(Collider2D collision)
        {
            LevelObjectView levelObject = collision.gameObject.GetComponent<LevelObjectView>();
            OnLevelObjectContact?.Invoke(levelObject);
        }
    }
}
