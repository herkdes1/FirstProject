namespace Base.UI
{
    using System;
    using Base.Game.Signal;
    using UnityEngine;

    public class PanelStageStart : MonoBehaviour
    {
        private void OnEnable()
        {
            SignalBus<SignalStageStart>.Instance.Register(OnStageStart);
        }

        private void OnDisable()
        {
            SignalBus<SignalStageStart>.Instance.UnRegister(OnStageStart);
        }

        private void OnStageStart()
        {
            gameObject.SetActive(false);
        }
    }
}
