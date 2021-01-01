namespace Base.UI
{
    using System;
    using Base.Game.Signal;
    using UnityEngine;

    public class PanelStageComplete : MonoBehaviour
    {
        private void Awake()
        {
            Registration();
            gameObject.SetActive(false);
        }

        private void OnDestroy()
        {
            UnRegistration();
        }

        private void Registration()
        {
            SignalBus<SignalStageComplete>.Instance.Register(OnStageComplete);
        }

        private void OnStageComplete()
        {
            gameObject.SetActive(true);
        }

        private void UnRegistration()
        {
            SignalBus<SignalStageComplete>.Instance.UnRegister(OnStageComplete);
        }

    }
}
