using System.Collections.Generic;
using UnityEngine;

namespace Platformer
{
    public class Main : MonoBehaviour
    {
        [SerializeField] private SpriteAnimatorConfig _playerConfig;

        [SerializeField] private CharacterDescription _playerDescription;
        [SerializeField] private GameObject _spawnPoint;

        private SpriteAnimatorController _playerAnimator;
        private PlayerController _playerController;
        private void Awake()
        {
            _playerConfig = Resources.Load<SpriteAnimatorConfig>("PlayerAnimCfg");
            _playerAnimator = new SpriteAnimatorController(_playerConfig);
            _playerController = PlayerController.CreatePlayer(_playerDescription, _spawnPoint.transform, _playerAnimator);
        }
        void Update()
        {
            _playerController.Update();
        }
    }
}
