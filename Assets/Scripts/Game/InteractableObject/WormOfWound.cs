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
        [SerializeField] private float _maxDistanceOfBounce = 1f;
        private float _minDistanceOfPenetration;
        private float _penetrationRatio;
        private float _bounceRatio;

        private Rigidbody _body;

        private void Awake()
        {
            Initialize();
        }

        private void OnDestroy()
        {
            SignalBus<SignalInteractableObjectDestroy, IInteractableObject>.Instance.Fire(this);
        }

        private void Initialize()
        {
            _body = GetComponent<Rigidbody>();
            _bounceRatio = _defaultBounceRatio * Time.deltaTime;
            _penetrationRatio = _defaultPenetrationRatio * Time.deltaTime;
            _maxDistanceOfBounce -= transform.position.x;
            _minDistanceOfPenetration = transform.position.x;
        }
        
        public void MoveUp()
        {
            transform.position -= transform.forward * _bounceRatio;
        }

        public void MoveDown()
        {
            transform.position += transform.forward * _penetrationRatio;
        }
        
        private void OnTriggerExit(Collider other)
        {
            if (other.GetComponent<Wound>())
            {
                other.GetComponent<Wound>().WormExit(this);
                Invoke("UseGravity", 1f);
                _animator.SetTrigger("Trigger");
            }
        }

        private void UseGravity()
        {
            _body.isKinematic = false;
            SignalBus<SignalWoundSelection, Transform>.Instance.Fire(null);
            Destroy(gameObject, 1f);
        }

        public Transform GetTransform()
        {
            return transform;
        }
    }
}
