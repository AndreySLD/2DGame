using UnityEngine;

namespace Platformer
{
    public class BulletController
    {
        private Vector3 _velocity;
        private LevelObjectView _view;

        public BulletController(LevelObjectView view)
        {
            _view = view;
            Active(false);
        }
        public void Active(bool value)
        {
            _view.gameObject.SetActive(value);
        }
        private void SetVelocity(Vector3 velocity)
        {
            _velocity = velocity;

            float angle = Vector3.Angle(Vector3.left, _velocity);
            Vector3 axis = Vector3.Cross(Vector3.left, _velocity);

            _view.transform.rotation = Quaternion.AngleAxis(angle, axis);
        }
        public void Shot(Vector3 position, Vector3 velocity)
        {
            Active(true);
            SetVelocity(velocity);
            _view.transform.position = position;
            _view.Rigidbody.velocity = Vector2.zero;
            _view.Rigidbody.angularVelocity = 0;

            _view.Rigidbody.AddForce(velocity, ForceMode2D.Impulse);
        }
    }
}
