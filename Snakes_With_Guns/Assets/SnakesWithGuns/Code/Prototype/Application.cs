using UnityEngine;

namespace SnakesWithGuns.Prototype
{
    public class Application : MonoBehaviour
    {
        private void Start()
        {
            UnityEngine.Application.targetFrameRate = 60;
        }
    }
}