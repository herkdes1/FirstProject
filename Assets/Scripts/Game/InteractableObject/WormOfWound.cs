namespace Base.Game.InteractableObject
{
    using Base.Game.Signal;
    using UnityEngine;

    [RequireComponent(typeof(Collider), typeof(Rigidbody))]
    public class WormOfWound : MonoBehaviour, IInteractableObject
    {
        [SerializeField] private Animator _animator = null;

        [SerializeField] private float _defaultBounceRatio = 1f;
        [SerializeField] private float _defaultPenetrationRatio = 1f;
        private float _minDistanceOfPenetration;
        private float _penetrationRatio;
        private float _bounceRatio;

        private Rigidbody _body;
        private Collider _collider;
        private bool _destroyed = false;

        private void Awake()
        {
            Initialize();
        }

        private void Initialize()
        {
            _collider = GetComponent<Collider>();
            _body = GetComponent<Rigidbody>();
            _bounceRatio = _defaultBounceRatio;
            _penetrationRatio = _defaultPenetrationRatio;
            _minDistanceOfPenetration = transform.position.x;
        }
        
        public void MoveUp()
        {
            transform.position += transform.up * _bounceRatio * Time.deltaTime;
        }

        public void MoveDown()
        {
            transform.position -= transform.up * _penetrationRatio * Time.deltaTime;
        }
        
        private void OnTriggerExit(Collider other)
        {
            if (other.GetComponent<Wound>() && !_destroyed)
            {
                _destroyed = true;
                other.GetComponent<Wound>().WormExit(this);
                Invoke("UseGravity", 1f);
                _animator.SetTrigger("Trigger");
                SignalBus<SignalInteractableObjectDestroy, IInteractableObject>.Instance.Fire(this);
            }
        }

        private void UseGravity()
        {
            _body.isKinematic = false;
            _collider.isTrigger = false;
            Destroy(gameObject, 1f);
        }

        public Transform GetTransform()
        {
            return transform;
        }
    }
}
