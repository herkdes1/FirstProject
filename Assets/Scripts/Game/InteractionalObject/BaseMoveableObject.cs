namespace Base.Game.InteractionalObject
{
    using Base.Game.InteractableObject;
    using Base.Game.State;
    using UnityEngine;

    [RequireComponent(typeof(Collider), typeof(Rigidbody))]
    public abstract class BaseMoveableObject : MonoBehaviour, IInteractionalObject, IMoveableObject
    {
        [SerializeField] private float _defaultSpeed = 1f;
        [SerializeField] private float _defaultRadius = 10f;
        protected float Speed { get; set; }
        private float _radius;
        private float _maxX, _minX, _maxY, _minY, _maxZ, _minZ;
        protected float Radius
        {
            get => _radius;
            set
            {
                _radius = value;
                _maxX = _startingPosition.x + _radius; _minX = _startingPosition.x - _radius;
                _maxY = _startingPosition.y + _radius; _minY = _startingPosition.y - _radius;
                _maxZ = _startingPosition.z + _radius; _minZ = _startingPosition.z - _radius;
            }
        }

        protected Vector3 _startingPosition;

        private Vector3 _target;

        private MoveableObjectContext _stateContext;


        private void Awake()
        {
            Initialize();
        }

        protected abstract IState InitState();
        protected MoveableObjectContext GetContext() { return _stateContext; }

        protected virtual void Initialize()
        {
            Radius = _defaultRadius;
            Speed = _defaultSpeed;
            _startingPosition = transform.position;
            _target = _startingPosition;
            _stateContext = new MoveableObjectContext(this);
            _stateContext.State = InitState();
        }

        protected void StateChange()
        {
            _stateContext.Request();
        }

        private void OnTriggerEnter(Collider other)
        {
            if (_stateContext.State == null)
                return;
            other.GetComponent<IInteractableObject>()?.Interact(this);
        }

        private void OnTriggerExit(Collider other)
        {
            if (_stateContext.State == null)
                return;
            other.GetComponent<IInteractableObject>()?.DeInteract(this);
        }

        private void OnTriggerStay(Collider other)
        {
            if (_stateContext.State == null)
                return;
            other.GetComponent<IInteractableObject>()?.ContinuousInteract(this);
        }

        protected virtual void Movement(Vector3 target)
        {
            _target = target;
            _stateContext.Movement?.Execute();
        }

        private void OnMouseDown()
        {
            OnMouseDrag();
        }

        private void OnMouseDrag()
        {
            Vector3 mousePos = Input.mousePosition;
            Vector3 objectPos = Camera.main.WorldToScreenPoint(transform.position);
            Vector3 target = Camera.main.ScreenToWorldPoint(new Vector3(mousePos.x, mousePos.y, objectPos.z));
            float x = (target.x > _maxX) ? _maxX : (target.x < _minX) ? _minX : target.x;
            float y = (target.y > _maxY) ? _maxY : (target.y < _minY) ? _minY : target.y;
            float z = (target.z > _maxZ) ? _maxZ : (target.z < _minZ) ? _minZ : target.z;
            Vector3 possibilyTarget = new Vector3(x, y, z);
            Movement(possibilyTarget);
        }

        public virtual void Active()
        {
            gameObject.SetActive(true);
        }

        public virtual void DeActive()
        {
            gameObject.SetActive(false);
        }

        #region Implementations

        public Transform GetTransform()
        {
            return transform;
        }

        public float GetSpeed()
        {
            return Speed * Time.deltaTime;
        }

        public Vector3 GetTarget()
        {
            return _target;
        }

        #endregion
    }
}
