using System;
using UnityEngine;

namespace SnakesWithGuns.Prototype.Weapons
{
    public class PooledParticle : MonoBehaviour
    {
        private ParticleSystem _effect;
        private Action<ParticleSystem> _onRelease;

        public void Construct(ParticleSystem effect, Action<ParticleSystem> onRelease)
        {
            _onRelease = onRelease;
            _effect = effect;
            ParticleSystem.MainModule main = effect.main;
            main.stopAction = ParticleSystemStopAction.Callback;
        }

        private void OnParticleSystemStopped()
        {
            _onRelease(_effect);
        }
    }
}