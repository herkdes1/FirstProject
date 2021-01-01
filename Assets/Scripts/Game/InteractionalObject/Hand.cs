namespace Base.Game.InteractionalObject
{
    using System;
    using Base.Game.Environment;
    using Base.Game.InteractableObject;
    using Base.Game.Signal;
    using Base.Game.State;
    using UnityEngine;

    public class Hand : BaseMoveableObject
    {
        [SerializeField] private Transform _lookableObj = null;
        [SerializeField] private StaticHand _staticHand = null;

        public bool IsBusy { get; set; }

        protected override void Initialize()
        {
            base.Initialize();
            SignalBus<SignalStageStart>.Instance.Register(OnStageStart);
            SignalBus<SignalWoundSelection, Transform>.Instance.Register(OnWoundSelection);
            DeActive();
        }

        private void OnWoundSelection(Transform obj)
        {
            if (obj)
            {
                _staticHand.Active();
                Speed = _defaultSpeed / 2;
                IsBusy = true;
            }
            if (obj != null)
                return;
            Radius = Radius;
            Speed = _defaultSpeed;
            _staticHand.DeActive();
            IsBusy = false;
            _lookableObj = null;
        }

        private void OnDestroy()
        {
            SignalBus<SignalWoundSelection, Transform>.Instance.UnRegister(OnWoundSelection);
            SignalBus<SignalStageStart>.Instance.UnRegister(OnStageStart);
        }

        private void OnStageStart()
        {
            Invoke("Active", 2f);
        }

        protected override IState InitState()
        {
            return new StateMoveableXY(GetContext());
        }

        protected override void Movement(Vector3 target)
        {
            base.Movement(target);
            if (_lookableObj == null)
                return;
            Vector3 lookPos = new Vector3(_lookableObj.position.x, _lookableObj.position.y, transform.position.z);
            transform.LookAt(lookPos);
        }

        public override void Interaction(IInteractableObject obj)
        {
            _lookableObj = obj.GetTransform();
            _minX = _lookableObj.position.x + .001f;
            SignalBus<SignalWoundSelection, Transform>.Instance.Fire(obj.GetTransform());
        }

    }
}
