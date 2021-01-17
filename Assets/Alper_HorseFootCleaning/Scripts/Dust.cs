namespace Alper_HorseFootCleaning.Scripts
{
    using Base.Game.InteractableObject;
    using Base.Game.Signal;
    using UnityEngine;

    public class Dust : MonoBehaviour, IInteractableObject
    {
        public Transform GetTransform()
        {
            return transform;
        }

        private void OnDisable()
        {
            SignalBus<SignalInteractableObjectDestroy, IInteractableObject>.Instance.Fire(this);
        }

    }
}
