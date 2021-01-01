namespace Base.UI
{
    using System;
    using Base.Game.InteractableObject;
    using Base.Game.Signal;
    using UnityEngine;
    using UnityEngine.UI;

    [RequireComponent(typeof(Slider))]
    public class SliderStageGeneral : MonoBehaviour
    {
        private int _maxValue;

        private Slider _slider;
        private bool _isCompleted = false;

        private void Awake()
        {
            _slider = GetComponent<Slider>();
            MonoBehaviour[] objs = FindObjectsOfType<MonoBehaviour>();
            foreach(MonoBehaviour obj in objs)
            {
                if (obj is IInteractableObject)
                    _maxValue++;
            }
            _slider.maxValue = _maxValue;
            SignalBus<SignalInteractableObjectDestroy,IInteractableObject>.Instance.Register(OnSignalInteractableObjectDestroyed);
        }

        private void OnDestroy()
        {
            SignalBus<SignalInteractableObjectDestroy,IInteractableObject>.Instance.UnRegister(OnSignalInteractableObjectDestroyed);
        }

        private void OnSignalInteractableObjectDestroyed(IInteractableObject obj)
        {
            if (_isCompleted)
                return;
            _slider.value++;
            if (_slider.value >= _maxValue)
            {
                SignalBus<SignalStageComplete>.Instance.Fire();
                _isCompleted = true;
            }
        }
    }
}
