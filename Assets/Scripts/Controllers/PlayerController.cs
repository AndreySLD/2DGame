using System;
using UnityEngine;

namespace Platformer
{
    public class PlayerController : IDisposable
    {
        private float _xAxisInput;
        private bool _isInTheAir;
        private bool _isMoving;

        private float _animationSpeed = 10f;
        private Vector3 _leftScale = new Vector3(-1, 1, 1);
        private Vector3 _rightScale = new Vector3(1, 1, 1);
      
        private float _g = -9.8f;
        private float _groundLevel = 0.5f;
        private float _yVelocity = 0f;

        private LevelObjectView _view;
        private SpriteAnimatorController _playerAnimator;
        private PlayerModel _playerModel;

        public PlayerController (LevelObjectView playerView, PlayerModel playerModel, SpriteAnimatorController playerAnimator)
        {
            _view = playerView;
            _playerModel = playerModel;
            _playerAnimator = playerAnimator;
            _playerAnimator.StartAnimation(_view.SpriteRenderer, AnimState.Idle, true, _animationSpeed);
        }
        public static PlayerController CreatePlayer(CharacterDescription description, Transform spawnPoint, SpriteAnimatorController playerAnimator)
        {
            var playerView = GameObject.Instantiate(description.Prefab, spawnPoint).GetComponent<LevelObjectView>();
            var playerModel = new PlayerModel(description);
            var player = new PlayerController(playerView, playerModel, playerAnimator);
            
            return player;
        }       
        private void MoveTowards()
        {
            _view.Transform.position += Vector3.right * Time.deltaTime * _playerModel.MovementSpeed * (_xAxisInput < 0 ? -1 : 1);
            _view.Transform.localScale = _xAxisInput < 0 ? _leftScale : _rightScale;
        }
        public bool IsGrounded()
        {
            return _view.Transform.position.y <= _groundLevel && _yVelocity <= 0;
        }
        public void Update()
        {
            _playerAnimator.Update();
            _xAxisInput = Input.GetAxis("Horizontal");
            _isInTheAir = Input.GetAxis("Vertical") > 0;
            _isMoving = Mathf.Abs(_xAxisInput) > _playerModel.MovingTreshhold;

            if (_isMoving)
            {
                MoveTowards();
            }
            if (IsGrounded())
            {
                _playerAnimator.StartAnimation(_view.SpriteRenderer, _isMoving ? AnimState.Run : AnimState.Idle, true, _animationSpeed);

                if (_isInTheAir && _yVelocity <= 0)
                {
                    _yVelocity = _playerModel.JumpSpeed;
                }
                else if (_yVelocity < 0)
                {
                    _yVelocity = 0f;

                    _view.Transform.position = _view.Transform.position.Change(y: _groundLevel);
                }
            }
            else
            {
                if (Mathf.Abs(_yVelocity) > _playerModel.JumpTreshhold)
                {
                    _playerAnimator.StartAnimation(_view.SpriteRenderer, AnimState.Jump, true, _animationSpeed);
                }

                _yVelocity += _g * Time.deltaTime;
                _view.Transform.position += Vector3.up * (Time.deltaTime * _yVelocity);
            }
        }
        public void Dispose()
        {
            //отписываться здесь
        }
    }
}
