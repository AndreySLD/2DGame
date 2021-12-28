using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace Platformer
{
    public class CoinsManager : IDisposable
    {
        private float _animationSpeed = 10f;

        private PlayerController _player;
        private SpriteAnimatorController _spriteAnimator;
        private List<LevelObjectView> _coinViews;

        public CoinsManager(PlayerController player, List<LevelObjectView> coinViews, SpriteAnimatorController spriteAnimator)
        {
            _player = player;
            _coinViews = coinViews;
            _spriteAnimator = spriteAnimator;
            foreach (LevelObjectView coinView in coinViews)
            {
                _spriteAnimator.StartAnimation(coinView.SpriteRenderer, AnimState.Run, true, _animationSpeed);
            }
            _player._view.OnLevelObjectContact += OnContact;
        }
        private void OnContact(LevelObjectView contactView)
        {
            if (_coinViews.Contains(contactView))
            {
                _spriteAnimator.StopAnimation(contactView.SpriteRenderer);
                GameObject.Destroy(contactView.gameObject);
            }
        }
        public void Dispose()
        {
            _player._view.OnLevelObjectContact -= OnContact;
        }
    }
}