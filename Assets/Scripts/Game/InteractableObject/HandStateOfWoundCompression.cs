namespace Base.Game.InteractableObject
{
    using Base.Game.InteractionalObject;
    using Base.Game.Signal;
    using UnityEngine;
    
    [RequireComponent(typeof(Collider))]
    public class HandStateOfWoundCompression : MonoBehaviour, IEnterInteractable, IInteractableObject
    {
        private Wound _connetedWound;
        private void Awake()
        {
            _connetedWound = GetComponent<Wound>();
        }

        private void OnDestroy()
        {
            SignalBus<SignalInteractableObjectDestroy, IInteractableObject>.Instance.Fire(this);
        }

        public Transform GetTransform()
        {
            return transform;
        }

        public void Interact(IInteractionalObject obj)
        {
            if (!(obj is Hand))
                return;
            if (((Hand)obj).IsBusy)
                return;
            obj.Interaction(this);
            SignalBus<SignalCameraPositionChange>.Instance.Fire();
            obj.GetTransform().position = new Vector3(transform.position.x + .5f,transform.position.y - .5f, obj.GetTransform().position.z);
            _connetedWound.Activate();
            DeActive();
        }

        private void Active()
        {
            enabled = true;
        }

        private void DeActive()
        {
            Destroy(this);
        }

    }
}
