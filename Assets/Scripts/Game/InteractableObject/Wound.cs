namespace Base.Game.InteractableObject
{
    using Base.Game.InteractionalObject;
    using Base.Game.Signal;
    using System.Collections;
    using System.Collections.Generic;
    using UnityEngine;

    [RequireComponent(typeof(Collider))]
    public class Wound : MonoBehaviour, IEnterInteractable, IExitInteractable, IContinuousInteractable, IInteractableObject
    {
        [SerializeField] private MeshRenderer _renderer = null;
        private Color _defaultMeshColor;
        [Space(20)]
        [SerializeField] private float _defaultCompressionRatio = 0.5f;
        [SerializeField] private float _defaultStretchRatio = 10f;
        [Space(10)]
        [SerializeField] private float _minCompressionValue = 0.2f;
        private float _maxCompressionValue = 1f;
        private float _comprassionRatio;
        private float _stretchRatio;

        [Space(10)]
        [SerializeField] private List<WormOfWound> _worms = null;

        private SphereCollider _collider;
        private float _maxColliderRadius;

        private bool _isActive = false;
        
        private void Awake()
        {
            Initialize();
        }

        private void OnDestroy()
        {
            SignalBus<SignalInteractableObjectDestroy,IInteractableObject>.Instance.Fire(this);
        }

        private void Initialize()
        {
            _defaultMeshColor = _renderer.material.color;
            _collider = GetComponent<SphereCollider>();
            _maxColliderRadius = _collider.radius;
            _comprassionRatio = _defaultCompressionRatio * Time.deltaTime;
            _stretchRatio = _defaultStretchRatio * Time.deltaTime;
            StartCoroutine(MaterialColorChangeAction());
        }
        public void Activate()
        {
            _isActive = true;
            _renderer.material.color = _defaultMeshColor;
            StopAllCoroutines();
        }

        private IEnumerator MaterialColorChangeAction()
        {
            var wait = new WaitForSeconds(.5f);
            bool isWhite = true;
            while (true)
            {
                isWhite = !isWhite;
                _renderer.material.color = isWhite ? _defaultMeshColor : Color.red;
                yield return wait;
            }
        }

        public void Interact(IInteractionalObject obj)
        {
            if (!(obj is Hand) || !_isActive)
                return;
            transform.localScale -= Vector3.right * _comprassionRatio;
            if (transform.localScale.x < _minCompressionValue)
                transform.localScale = new Vector3(_minCompressionValue, transform.localScale.y, transform.localScale.z);
            foreach (WormOfWound worm in _worms)
                worm.MoveUp();
            _collider.radius -= _comprassionRatio/10;
            if (_collider.radius < .05f)
                _collider.radius = .05f;
        }

        public void ExitInteract(IInteractionalObject obj)
        {
            if (!(obj is Hand) || !_isActive)
                return;
            transform.localScale += Vector3.right * _stretchRatio;
            if (transform.localScale.x > 1)
            {
                transform.localScale = new Vector3(1, transform.localScale.y, transform.localScale.z);
                return;
            }
            foreach (WormOfWound worm in _worms)
                worm.MoveDown();
            _collider.radius += _stretchRatio / 10;
            if (_collider.radius > _maxColliderRadius)
                _collider.radius = _maxColliderRadius;
            _maxCompressionValue = transform.localScale.x;
        }

        public void ContinuousInteract(IInteractionalObject obj)
        {
            if (!(obj is Hand))
                return;
            Interact(obj);
        }

        public void WormExit(WormOfWound worm)
        {
            _worms.Remove(worm);
            if (_worms.Count == 0)
                Destroy(gameObject, 3f);
        }

        public Transform GetTransform()
        {
            return transform;
        }
    }

}
