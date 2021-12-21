using UnityEngine;

namespace Platformer
{
    public class PlayerModel
    {
        public int MaxHealth { get; set; }
        public float MovementSpeed { get; set; }
        public float MovingTreshhold { get; }
        public float JumpSpeed { get; set; }
        public float JumpTreshhold { get; }

        public PlayerModel(CharacterDescription description)
        {
            MaxHealth = description.MaxHealth;
            MovementSpeed = description.MovementSpeed;
            MovingTreshhold = description.MovingTreshhold;
            JumpSpeed = description.JumpSpeed;
            JumpTreshhold = description.JumpTreshhold;
        }
    }
}
