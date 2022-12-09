using UnityEngine;

namespace SnakesWithGuns.Infrastructure
{
    public class SceneController : MonoBehaviour
    {
        private ISceneService[] _sceneServices;

        private void Start()
        {
            _sceneServices = GetComponentsInChildren<ISceneService>();
            Initialize();
            Activate();
        }

        private void Update()
        {
            foreach (ISceneService sceneService in _sceneServices)
                sceneService.Tick(Time.deltaTime);

            OnUpdate(Time.deltaTime);
        }

        private void OnDestroy()
        {
            Deactivate();
            Cleanup();
        }

        private void Initialize()
        {
            OnInitialize();
            
            foreach (ISceneService sceneService in _sceneServices)
                sceneService.Initialize();
        }

        public void Activate()
        {
            OnActivate();
            
            foreach (ISceneService sceneService in _sceneServices)
                sceneService.Activate();
        }

        public void Deactivate()
        {
            OnDeactivate();
            
            foreach (ISceneService sceneService in _sceneServices)
                sceneService.Deactivate();
        }

        private void Cleanup()
        {
            OnCleanup();
            
            foreach (ISceneService sceneService in _sceneServices)
                sceneService.Cleanup();
        }

        protected virtual void OnInitialize() { }
        protected virtual void OnActivate() { }
        protected virtual void OnUpdate(float deltaTime) { }
        protected virtual void OnDeactivate() { }
        protected virtual void OnCleanup() { }
    }
}