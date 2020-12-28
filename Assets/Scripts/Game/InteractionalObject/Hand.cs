namespace Base.Game.InteractionalObject
{
    using System;
    using Base.Game.Signal;
    using Base.Game.State;
    using UnityEngine;

    public class Hand : BaseMoveableObject
    {
        [SerializeField] private Transform _lookableObj = null;

        protected override void Initialize()
        {
            base.Initialize();
            SignalBus<SignalStageStart>.Instance.Register(OnStageStart);
            DeActive();
        }

        private void OnDestroy()
        {
            SignalBus<SignalStageStart>.Instance.UnRegister(OnStageStart);
        }

        private void OnStageStart()
        {
            Invoke("Active", 2f);
        }

        protected override IState InitState()
        {
            return new StateMoveableYZ(GetContext());
        }

        protected override void Movement(Vector3 target)
        {
            base.Movement(target);
            transform.LookAt(_lookableObj);
        }
    }
}
