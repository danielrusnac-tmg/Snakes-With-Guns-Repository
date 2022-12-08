using UnityEngine;

namespace SnakesWithGuns.Gameplay
{
    public class Actor : MonoBehaviour
    {
        public int ActorID => GetInstanceID();
    }
}