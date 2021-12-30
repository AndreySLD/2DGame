using UnityEngine;

namespace Platformer
{
    public class CannonAimController
    {
        private Transform _muzzleTransform;
        private Transform _targetTransform;

        private Vector3 _dir;
        private float _angle;
        private Vector3 _axis;

        public CannonView _cannonView;

        public CannonAimController(CannonView cannonView, Transform targetTransform)
        {
            _cannonView = cannonView;
            _muzzleTransform = cannonView.MuzzleTransform;
            _targetTransform = targetTransform;
        }
        public static CannonAimController InitializeCannon(GameObject prefab, Transform spawnPoint) 
        {
            var cannonView = Object.Instantiate(prefab, spawnPoint).GetComponent<CannonView>();
            var targetTransform = GameObject.FindGameObjectWithTag("Player").transform;
            var cannon = new CannonAimController(cannonView, targetTransform);
            return cannon;
        }
        public void Update()
        {                            
            _dir = _targetTransform.position - _muzzleTransform.position;               
            _angle = Vector3.Angle(Vector3.down, _dir);               
            _axis = Vector3.Cross(Vector3.down, _dir);

            //if (_angle <= _cannonView.RotationAngle)
            _muzzleTransform.rotation = Quaternion.AngleAxis(_angle, _axis);           
            //else _muzzleTransform.rotation = Quaternion.AngleAxis(0, Vector3.down);
        }
    }
}
