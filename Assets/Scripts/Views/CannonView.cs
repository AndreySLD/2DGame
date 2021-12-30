using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace Platformer
{
    public class CannonView : MonoBehaviour
    {
        public Transform MuzzleTransform;
        public Transform ShootPointTransform;
        public List<LevelObjectView> Bullets;
        public float ShotRange;
        public float RotationAngle;
    }
}
