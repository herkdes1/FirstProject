namespace Base.UI
{
    using Base.Game.Signal;
    using System.Collections;
    using UnityEngine;

    [RequireComponent(typeof(RectTransform))]
    public class LoadingImage : MonoBehaviour
    {
        [SerializeField] private float _sceneTransaction = 2f;
        [SerializeField] private float _maxSize = .75f;
        [SerializeField] private float _speed = 1f;
        private RectTransform _rect;

        private void Start()
        {
            _rect = GetComponent<RectTransform>();
            StartCoroutine(SizeAction());
        }

        private void OnDisable()
        {
            StopAllCoroutines();
        }

        private IEnumerator SizeAction()
        {
            var wait = new WaitForFixedUpdate();
            Vector3 maxSize = new Vector3(_maxSize, _maxSize, _maxSize);
            Vector3 defaultSize = _rect.localScale;
            bool _isBig = false;
            Vector3 targetSize = maxSize;
            float distance = Vector3.Distance(_rect.localScale, targetSize);
            float timer = 0;
            while (timer < _sceneTransaction)
            {
                timer += Time.fixedDeltaTime;
                if (_isBig)
                    targetSize = defaultSize;
                else
                    targetSize = maxSize;
                _rect.localScale = Vector3.MoveTowards(_rect.localScale, targetSize, _speed * Time.deltaTime);
                distance = Vector3.Distance(_rect.localScale, targetSize);
                if (distance < .01f)
                    _isBig = !_isBig;
                yield return wait;
            }
            yield return new WaitForSeconds(1f);
            SignalBus<SignalFade, bool>.Instance.Fire(true);
        }

    }
}
