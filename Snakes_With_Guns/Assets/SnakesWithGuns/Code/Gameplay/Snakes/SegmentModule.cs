using UnityEngine;

namespace SnakesWithGuns.Gameplay.Snakes
{
    [CreateAssetMenu(menuName = "Snakes With Guns/Segment Module", fileName = "module_")]
    public class SegmentModule : ScriptableObject
    {
        [SerializeField] private string _moduleName;
        [SerializeField] private SegmentModuleComponent _modulePrefab;

        public string ModuleName => _moduleName;
        public SegmentModuleComponent ModulePrefab => _modulePrefab;
    }
}       