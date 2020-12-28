namespace Base.Game.InteractableObject
{
    using Base.Game.InteractionalObject;
    using Base.Game.Signal;
    using System.Collections;
    using UnityEngine;

    [RequireComponent(typeof(Collider))]
    public class Wound : MonoBehaviour, IInteractableObject
    {
        [SerializeField] private MeshRenderer _renderer = null;
        [Space(20)]
        [SerializeField] private float _defaultCompressionRatio = 0.5f;
        [SerializeField] private float _defaultStretchRatio = 10f;
        [Space(10)]
        [SerializeField] private float _minCompressionValue = 0.2f;
        private float _maxCompressionValue = 1f;
        private float _comprassionRatio;
        private float _stretchRatio;

        private WormOfWound[] _worms;

        private SphereCollider _collider;
        private float _maxColliderRadius;

        private bool _clicked = false;

        private void Awake()
        {
            Initialize();
        }

        private void Initialize()
        {
            _collider = GetComponent<SphereCollider>();
            _maxColliderRadius = _collider.radius;
            _comprassionRatio = _defaultCompressionRatio * Time.deltaTime;
            _stretchRatio = _defaultStretchRatio * Time.deltaTime;
            _worms = GetComponentsInChildren<WormOfWound>();
            StartCoroutine(MaterialColorChangeAction());
        }
        private void OnMouseDown()
        {
            if (_clicked)
                return;
            _clicked = true;
            _renderer.material.color = Color.white;
            StopAllCoroutines();
            SignalBus<SignalStageStart>.Instance.Fire();
        }
        private IEnumerator MaterialColorChangeAction()
        {
            var wait = new WaitForSeconds(.5f);
            bool isWhite = true;
            while (true)
            {
                isWhite = !isWhite;
                _renderer.material.color = isWhite ? Color.white : Color.red;
                yield return wait;
            }
        }

        public void Interact(IInteractionalObject obj)
        {
            if (!(obj is Hand))
                return;
            transform.localScale -= Vector3.forward * _comprassionRatio;
            if (transform.localScale.z < _minCompressionValue)
                transform.localScale = new Vector3(transform.localScale.x, transform.localScale.y, _minCompressionValue);
            foreach (WormOfWound worm in _worms)
                worm.MoveUp();
            _collider.radius -= _comprassionRatio/2;
            if (_collider.radius < .5f)
                _collider.radius = .5f;
        }

        public void DeInteract(IInteractionalObject obj)
        {
            if (!(obj is Hand))
                return;
            transform.localScale += Vector3.forward * _stretchRatio;
            if (transform.localScale.z > _maxCompressionValue)
            {
                transform.localScale = new Vector3(transform.localScale.x, transform.localScale.y, _maxCompressionValue);
                return;
            }
            foreach (WormOfWound worm in _worms)
                worm.MoveDown();
            _collider.radius += _stretchRatio / 2;
            if (_collider.radius > _maxColliderRadius)
                _collider.radius = _maxColliderRadius;
            _maxCompressionValue = transform.localScale.z;
        }

        public void ContinuousInteract(IInteractionalObject obj)
        {
            if (!(obj is Hand))
                return;
            Interact(obj);
        }
    }

    internal interface IEmumerator
    {
    }
}
