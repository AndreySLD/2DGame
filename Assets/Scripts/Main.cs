using System.Collections.Generic;
using UnityEngine;

namespace Platformer
{
    public class Main : MonoBehaviour
    {
        [SerializeField] private SpriteAnimatorConfig _playerConfig;
        [SerializeField] private SpriteAnimatorConfig _coinAnimatorCfg;
        [SerializeField] private CharacterDescription _playerDescription;
        [SerializeField] private GameObject _spawnPoint;
        [SerializeField] private GameObject _cannonSpawnPoint;
        [SerializeField] private GameObject _cannonPrefab;
        [SerializeField] private List<LevelObjectView> _coinViews;
        [SerializeField] private LevelGeneratorView _generatorView;
        [SerializeField] private QuestView _questView;


        private SpriteAnimatorController _playerAnimator;
        private SpriteAnimatorController _coinAnimator;
        private PlayerController _playerController;
        private CameraController _cameraController;
        private CannonAimController _cannonLvL1;
        private ShotPointerController _shotPointerController;
        private CoinsManager _coinsManager;
        private LevelGeneratorController _levelGeneratorController;
        private QuestConfiguratorController _questConfiguratorController;


        private void Awake()
        {
            _playerConfig = Resources.Load<SpriteAnimatorConfig>("PlayerAnimCfg");
            _coinAnimatorCfg = Resources.Load<SpriteAnimatorConfig>("CoinAnimCfg");
            _playerAnimator = new SpriteAnimatorController(_playerConfig);
            _coinAnimator = new SpriteAnimatorController(_coinAnimatorCfg);
            _playerController = PlayerController.CreatePlayer(_playerDescription, _spawnPoint.transform, _playerAnimator);
            _cameraController = CameraController.ConnectCamera();
            _cannonLvL1 = CannonAimController.InitializeCannon(_cannonPrefab, _cannonSpawnPoint.transform);
            _shotPointerController = new ShotPointerController(_cannonLvL1._cannonView.Bullets, _cannonLvL1._cannonView.ShootPointTransform, _cannonLvL1._cannonView);

            _coinsManager = new CoinsManager(_playerController, _coinViews, _coinAnimator);

            _levelGeneratorController = new LevelGeneratorController(_generatorView);
            _levelGeneratorController.Init();

            _questConfiguratorController = new QuestConfiguratorController(_questView);
            _questConfiguratorController.Init();

        }
        void Update()
        {           
            _playerController.Update();
            _cameraController.Update();
            _cannonLvL1.Update();
            _shotPointerController.Update();
            _coinAnimator.Update();
        }
    }
}
