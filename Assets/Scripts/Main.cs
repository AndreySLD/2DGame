using System.Collections.Generic;
using UnityEngine;

namespace Platformer
{
    public class Main : MonoBehaviour
    {
        [SerializeField] private SpriteAnimatorConfig _playerConfig;
        [SerializeField] private CharacterDescription _playerDescription;
        [SerializeField] private GameObject _spawnPoint;
        [SerializeField] private GameObject _cannonSpawnPoint;
        [SerializeField] private GameObject _cannonPrefab;


        private SpriteAnimatorController _playerAnimator;
        private PlayerController _playerController;
        private CameraController _cameraController;
        private CannonAimController _cannon;
        public ShotPointerController _shotPointerController;
        private void Awake()
        {
            _playerConfig = Resources.Load<SpriteAnimatorConfig>("PlayerAnimCfg");
            _playerAnimator = new SpriteAnimatorController(_playerConfig);
            _playerController = PlayerController.CreatePlayer(_playerDescription, _spawnPoint.transform, _playerAnimator);
            _cameraController = CameraController.ConnectCamera();
            _cannon = CannonAimController.InitializeCannon(_cannonPrefab, _cannonSpawnPoint.transform);
            _shotPointerController = new ShotPointerController(_cannon._cannonView.Bullets, _cannon._cannonView.ShootPointTransform);
        }
        void Update()
        {           
            _playerController.Update();
            _cameraController.Update();
            _cannon.Update();
            _shotPointerController.Update();
        }
    }
}
