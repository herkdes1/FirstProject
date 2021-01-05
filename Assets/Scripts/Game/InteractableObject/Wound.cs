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
        [SerializeField] private GameObject _cap = null;
        private Color _defaultMeshColor;
        [Space(20)]
        [SerializeField] private float _defaultCompressionRatio = 0.5f;
        [SerializeField] private float _defaultStretchRatio = 10f;
        private float _comprassionRatio;
        private float _stretchRatio;

        [Space(10)]
        [SerializeField] private List<WormOfWound> _worms = null;

        private SphereCollider _collider;
        private float _maxColliderRadius;

        private bool _isActive = false;

        private IInteractionalObject _connectedObj;

        private void Awake()
        {
            Initialize();
        }

        private void OnDestroy()
        {
            SignalBus<SignalInteractableObjectDestroy,IInteractableObject>.Instance.Fire(this);
            FindObjectOfType<Wound>()?.Activate(_connectedObj);
        }


        private void Initialize()
        {
            _defaultMeshColor = _renderer.material.color;
            _collider = GetComponent<SphereCollider>();
            _maxColliderRadius = _collider.radius;
            _comprassionRatio = _defaultCompressionRatio * Time.deltaTime;
            _stretchRatio = _defaultStretchRatio * Time.deltaTime;
            StartCoroutine(MaterialColorChangeAction());
            _collider.enabled = false;
        }
        public void Activate(IInteractionalObject obj)
        {
            _collider.enabled = true;
            _connectedObj = obj;
            _isActive = true;
            _renderer.material.color = _defaultMeshColor;
            StopAllCoroutines();
            obj.Interaction(this);
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
            foreach (WormOfWound worm in _worms)
                worm.MoveDown();
            _collider.radius += _stretchRatio / 10;
            if (_collider.radius > _maxColliderRadius)
                _collider.radius = _maxColliderRadius;
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
            {
                _cap.SetActive(false);
                Destroy(gameObject, 3f);
            }
        }

        public Transform GetTransform()
        {
            return transform;
        }
    }

}
