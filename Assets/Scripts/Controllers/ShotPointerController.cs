using UnityEngine;
using System.Collections.Generic;

namespace Platformer
{
    public class ShotPointerController
    {
        private List<BulletController> _bullets = new List<BulletController>();
        private Transform _transform;

        private int _currentIndex;
        private float _timeTillNextShot;

        private float _delay = 1;
        private float _startSpeed = 15f;

        public ShotPointerController(List<LevelObjectView> bulletViews, Transform transform)
        {
            _transform = transform;
            foreach (LevelObjectView bulletView in bulletViews)
            {
                _bullets.Add(new BulletController(bulletView));
            }
        }
        public void Update()
        {
            if(_timeTillNextShot > 0)
            {
                _bullets[_currentIndex].Active(false);
                _timeTillNextShot -= Time.deltaTime;
            }
            else
            {
                _timeTillNextShot = _delay;
                _bullets[_currentIndex].Shot(_transform.position, -_transform.up * _startSpeed);
                _currentIndex++;

                if(_currentIndex >= _bullets.Count)
                {
                    _currentIndex = 0;
                }
            }
        }
    }
}
