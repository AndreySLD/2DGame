using UnityEngine;

namespace Platformer
{
    [CreateAssetMenu(menuName = "GameAssets/Character Description")]
    public class CharacterDescription : ScriptableObject
    {
        [SerializeField] private int _maxHealth;
        [SerializeField] private float _movementSpeed;
        [SerializeField] private float _movingTreshhold;
        [SerializeField] private float _jumpSpeed;
        [SerializeField] private float _jumpTreshhold;
        [SerializeField] private GameObject _prefab;

        public int MaxHealth => _maxHealth;
        public float MovementSpeed => _movementSpeed;
        public float MovingTreshhold => _movingTreshhold;
        public float JumpSpeed => _jumpSpeed;
        public float JumpTreshhold => _jumpTreshhold;
        public GameObject Prefab => _prefab;
    }
}
