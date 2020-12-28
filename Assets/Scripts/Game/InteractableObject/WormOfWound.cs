namespace Base.Game.InteractableObject
{
    using UnityEngine;

    [RequireComponent(typeof(Collider), typeof(Rigidbody))]
    public class WormOfWound : MonoBehaviour
    {
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
            transform.position -= Vector3.right * _bounceRatio;
            Control();
        }

        public void MoveDown()
        {
            transform.position += Vector3.right * _penetrationRatio;
            if (transform.position.x > _minDistanceOfPenetration)
                transform.position = new Vector3(_minDistanceOfPenetration, transform.position.y, transform.position.z);
        }

        private void Control()
        {
            if(transform.position.x < _maxDistanceOfBounce)
            {
                _body.isKinematic = false;
            }
        }
        
    }
}
