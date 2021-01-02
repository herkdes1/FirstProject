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

        private Rigidbody _body;

        private Vector3 _startingRot;

        protected override void Initialize()
        {
            _startingRot = transform.eulerAngles;
            _body = GetComponent<Rigidbody>();
            base.Initialize();
            SignalBus<SignalStageStart>.Instance.Register(OnStageStart);
            SignalBus<SignalWoundSelection, Transform>.Instance.Register(OnWoundSelection);
            DeActive();
        }
        private void OnEnable()
        {
            FindObjectOfType<Wound>().Activate(this);
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
            Invoke("OnEnable", 1f);
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

        private void OnCollisionEnter(Collision collision)
        {
            collision.collider.GetComponent<IEnterInteractable>()?.Interact(this);
        }

        private void OnCollisionExit(Collision collision)
        {
            collision.collider.GetComponent<IExitInteractable>()?.ExitInteract(this);
        }

        private void OnCollisionStay(Collision collision)
        {
            collision.collider.GetComponent<IContinuousInteractable>()?.ContinuousInteract(this);
        }

        protected override IState InitState()
        {
            return new StateMoveableXY(GetContext());
        }

        protected override void Movement(Vector3 target)
        {
            _body.velocity = Vector3.zero;
            base.Movement(target);
            if (_lookableObj == null)
                return;
            Vector3 lookPos = new Vector3(_lookableObj.position.x, _lookableObj.position.y, transform.position.z);
            transform.LookAt(lookPos);
        }

        public override void Interaction(IInteractableObject obj)
        {
            _lookableObj = obj.GetTransform();
            transform.rotation = Quaternion.Euler(_startingRot);
            transform.position = new Vector3(obj.GetTransform().position.x + .5f, obj.GetTransform().position.y - .5f, transform.position.z);
            _minX = _lookableObj.position.x + .05f;
            SignalBus<SignalWoundSelection, Transform>.Instance.Fire(obj.GetTransform());
        }

    }
}
