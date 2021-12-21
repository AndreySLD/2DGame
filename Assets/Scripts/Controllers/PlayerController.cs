using System;
using UnityEngine;

namespace Platformer
{
    public class PlayerController : IDisposable
    {
        private float _xAxisInput;
        private bool _isInTheAir;
        private bool _isMoving;

        private float _movementSpeed = 3f;
        private float _animationSpeed = 10f;
        private float _movingTreshhold = 0.1f;
        private Vector3 _leftScale = new Vector3(-1, 1, 1);
        private Vector3 _rightScale = new Vector3(1, 1, 1);

        private float _jumpSpeed = 9f;
        private float _jumpTreshhold = 1f;
        private float _g = -9.8f;
        private float _groundLevel = 0.5f;
        private float _yVelocity = 0f;

        private LevelObjectView _view;
        private SpriteAnimatorController _playerAnimator;
        public PlayerController(LevelObjectView playerView, SpriteAnimatorController playerAnimator)
        {
            _view = playerView;
            _playerAnimator = playerAnimator;
            _playerAnimator.StartAnimation(_view.SpriteRenderer, AnimState.Idle, true, _animationSpeed);
        }
        private void MoveTowards()
        {
            _view.Transform.position += Vector3.right * Time.deltaTime * _movementSpeed * (_xAxisInput < 0 ? -1 : 1);
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
            _isMoving = Mathf.Abs(_xAxisInput) > _movingTreshhold;

            if (_isMoving)
            {
                MoveTowards();
            }
            if (IsGrounded())
            {
                _playerAnimator.StartAnimation(_view.SpriteRenderer, _isMoving ? AnimState.Run : AnimState.Idle, true, _animationSpeed);

                if (_isInTheAir && _yVelocity <= 0)
                {
                    _yVelocity = _jumpSpeed;
                }
                else if (_yVelocity < 0)
                {
                    _yVelocity = 0f;

                    _view.Transform.position = _view.Transform.position.Change(y: _groundLevel);
                }
            }
            else
            {
                if (Mathf.Abs(_yVelocity) > _jumpTreshhold)
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
