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
        private float _xVelocity = 0f;

        private LevelObjectView _view;
        private SpriteAnimatorController _playerAnimator;
        private PlayerModel _playerModel;
        private ContactPooler _contactPooler;

        public PlayerController (LevelObjectView playerView, PlayerModel playerModel, SpriteAnimatorController playerAnimator, ContactPooler contactPooler)
        {
            _view = playerView;
            _playerModel = playerModel;
            _playerAnimator = playerAnimator;
            _contactPooler = contactPooler;
            _playerAnimator.StartAnimation(_view.SpriteRenderer, AnimState.Idle, true, _animationSpeed);           
        }
        public static PlayerController CreatePlayer(CharacterDescription description, Transform spawnPoint, SpriteAnimatorController playerAnimator)
        {
            var playerView = GameObject.Instantiate(description.Prefab, spawnPoint).GetComponent<LevelObjectView>();
            var playerModel = new PlayerModel(description);
            var contactPooler = new ContactPooler(playerView.Collider);
            var player = new PlayerController(playerView, playerModel, playerAnimator, contactPooler);

            return player;
        }
        private void MoveTowards()
        {
            _xVelocity = (Time.fixedDeltaTime * _playerModel.MovementSpeed * (_xAxisInput < 0 ? -1 : 1));
            _view.Rigidbody.velocity = _view.Rigidbody.velocity.Change(x: _xVelocity);
            _view.Transform.localScale = _xAxisInput < 0 ? _leftScale : _rightScale;
        }
        public void Update()
        {
            _playerAnimator.Update();
            _contactPooler.Update();
            _xAxisInput = Input.GetAxis("Horizontal");
            _isInTheAir = Input.GetAxis("Vertical") > 0;
            _isMoving = Mathf.Abs(_xAxisInput) > _playerModel.MovingTreshhold;

            if (_isMoving)
            {
                MoveTowards();
            }
            if (_contactPooler.IsGrounded)
            {
                _playerAnimator.StartAnimation(_view.SpriteRenderer, _isMoving ? AnimState.Run : AnimState.Idle, true, _animationSpeed);

                if (_isInTheAir && Mathf.Abs(_view.Rigidbody.velocity.y) <= _playerModel.JumpTreshhold)
                {
                    _view.Rigidbody.AddForce(Vector2.up * _playerModel.JumpSpeed, ForceMode2D.Impulse);
                }
                else if (_yVelocity < 0)
                {
                    _yVelocity = 0f;

                    _view.Transform.position = _view.Transform.position.Change(y: _groundLevel);
                }
            }
            else
            {
                if (Mathf.Abs(_view.Rigidbody.velocity.y) > _playerModel.JumpTreshhold)
                {
                    _playerAnimator.StartAnimation(_view.SpriteRenderer, AnimState.Jump, true, _animationSpeed);
                }
            }
        }
        public void Dispose()
        {
            //отписываться здесь
        }
    }
}
