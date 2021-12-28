using UnityEngine;

namespace Platformer
{
    public class CameraController
    {
        private LevelObjectView _playerView;
        private Transform _playerTransform;
        private Transform _mainCamTransform;

        [SerializeField] private float _camSpeed = 10f;
        [SerializeField] private float _offsetChanger = 4;
        [SerializeField] private float _pivotZ = 10;

        private float _x;
        private float _y;

        private float _offsetX = 0;
        private float _offsetY = 0;

        private float _xAxisInput;
        private float _yAxisVelocity;


        public CameraController(LevelObjectView player, Transform camera)
        {
            _playerView = player;
            _playerTransform = _playerView.transform;
            _mainCamTransform = camera;
        }
        public static CameraController ConnectCamera()
        {
            var playerView = GameObject.FindGameObjectWithTag("Player").GetComponent<LevelObjectView>();
            var cameraTransform = Camera.main.transform;
            var MainCamera = new CameraController(playerView, cameraTransform);

            return MainCamera;                
        }
        public void Update()
        {
            _xAxisInput = Input.GetAxis("Horizontal");
            _yAxisVelocity = _playerView.Rigidbody.velocity.y;

            if (_xAxisInput > 0) _offsetX = _offsetChanger;
            else if (_xAxisInput < 0) _offsetX = -_offsetChanger;
            else _offsetX = 0;

            if (_yAxisVelocity > 0) _offsetY = _offsetChanger;
            else if (_yAxisVelocity < 0) _offsetY = -_offsetChanger;
            else _offsetY = 0;

            var target = new Vector3(_playerTransform.position.x, _playerTransform.position.y, _playerTransform.position.z - _pivotZ);
            var velocity = new Vector3(_x + _offsetX, _y + _offsetY, _mainCamTransform.position.z);
            var delta = Time.deltaTime;

            _mainCamTransform.position = Vector3.SmoothDamp(_mainCamTransform.position, target, ref velocity, delta, _camSpeed);
        }
    }
}
