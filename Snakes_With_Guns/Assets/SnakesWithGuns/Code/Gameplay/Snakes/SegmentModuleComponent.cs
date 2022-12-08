using UnityEngine;

namespace SnakesWithGuns.Gameplay.Snakes
{
    public class SegmentModuleComponent : MonoBehaviour
    {
        public Actor ParentActor { get; set; }

        public virtual void OnInstall() { }
    }
}