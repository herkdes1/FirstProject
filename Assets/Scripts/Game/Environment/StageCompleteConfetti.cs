namespace Base.Game.Environment
{
    using Base.Game.Signal;
    using System;
    using System.Collections;
    using UnityEngine;
    [RequireComponent(typeof(ParticleSystem))]
    public class StageCompleteConfetti : MonoBehaviour
    {
        private ParticleSystem _particle;

        private void Awake()
        {
            _particle = GetComponent<ParticleSystem>();
            Registration();
        }

        private void OnDestroy()
        {
            UnRegistration();
        }

        private void Registration()
        {
            SignalBus<SignalStageComplete>.Instance.Register(OnStageComplete);
        }

        private void UnRegistration()
        {
            SignalBus<SignalStageComplete>.Instance.UnRegister(OnStageComplete);
        }

        private void OnStageComplete()
        {
            StopAllCoroutines();
            StartCoroutine(ParticleAction());
        }

        private IEnumerator ParticleAction()
        {
            _particle.Play();
            yield return new WaitForSeconds(1f);
            _particle.Stop();
        }
    }
}