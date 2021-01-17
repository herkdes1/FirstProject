namespace Base.Game.Environment
{
    using System;
    using Base.Game.InteractionalObject;
    using Base.Game.Signal;
    using UnityEngine;

    public class StaticHand : MonoBehaviour
    {
        private Vector3 _defaultLocalPos;
        private Vector3 _defaultRotation;

        private void Awake()
        {
            Registration();
            DeActive();
        }
        private void OnDestroy()
        {
            UnRegistration();
        }
        private void Registration()
        {
            SignalBus<SignalWoundSelection, Transform>.Instance.Register(OnWoundSelection);
        }

        private void OnWoundSelection(Transform obj)
        {
            if (obj == null)
                return;
            transform.position = new Vector3(obj.position.x - .05f, obj.position.y + .05f, obj.position.z);
        }

        private void UnRegistration()
        {
            SignalBus<SignalWoundSelection, Transform>.Instance.UnRegister(OnWoundSelection);
        }

        public void Connected(Hand hand)
        {
            DeActive();
        }

        public void ConnectionFailed(Hand hand)
        {
            Active();
        }

        public void Active()
        {
            gameObject.SetActive(true);
        }

        public void DeActive()
        {
            gameObject.SetActive(false);
        }

    }
}
