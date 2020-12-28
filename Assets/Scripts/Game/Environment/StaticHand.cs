namespace Base.Game.Environment
{
    using System;
    using Base.Game.Signal;
    using UnityEngine;

    public class StaticHand : MonoBehaviour
    {
        private void Awake()
        {
            SignalBus<SignalStageStart>.Instance.Register(OnStageStart);
            gameObject.SetActive(false);
        }

        private void OnDestroy()
        {
            SignalBus<SignalStageStart>.Instance.UnRegister(OnStageStart);
        }

        private void OnStageStart()
        {
            Invoke("Active", 1.8f);
        }

        private void Active()
        {
            gameObject.SetActive(true);
        }
    }
}
