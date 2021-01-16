namespace Base.UI
{
    using Base.Game.Signal;
    using System.Collections;
    using UnityEngine;
    using UnityEngine.SceneManagement;
    using UnityEngine.UI;

    [RequireComponent(typeof(Image))]
    public class PanelFade : MonoBehaviour
    {
        [SerializeField] private bool _isInFade = false;
        [SerializeField] private float _fadeTime = 2f;

        private Image _image;

        private Coroutine _fadeRoutine;

        private void Awake()
        {
            _image = GetComponent<Image>();
            if (SceneManager.GetActiveScene().buildIndex == 0)
                return;
            OnSceneLoaded();
        }

        private void OnEnable()
        {
            Registration();
        }

        private void OnSceneLoaded()
        {
            if (SceneManager.GetActiveScene().buildIndex == 0)
                OnFadeCalled(true);
            else
                OnFadeCalled(false);
        }

        private void OnDestroy()
        {
            UnRegistration();
        }

        private void Registration() => SignalBus<SignalFade, bool>.Instance.Register(OnFadeCalled);
        private void UnRegistration() => SignalBus<SignalFade, bool>.Instance.UnRegister(OnFadeCalled);
        private void OnFadeCalled(bool obj)
        {
            if(_fadeRoutine != null)
            {
                StopCoroutine(_fadeRoutine);
            }
            gameObject.SetActive(true);
            _isInFade = obj;
            _image.color = new Color(_image.color.r, _image.color.g, _image.color.b, (obj ? 0 : 1));
            _fadeRoutine = StartCoroutine(FadeAction((_image.color.a)));
            
        }

        private IEnumerator FadeAction(float init)
        {
            var wait = new WaitForFixedUpdate();
            float target = init == 0 ? 1 : 0;
            float time = 0;
            while(time < _fadeTime)
            {
                time += Time.fixedDeltaTime;
                init = Mathf.Lerp(init, target, Time.deltaTime);
                _image.color = new Color(_image.color.r, _image.color.g, _image.color.b, init);
                yield return wait;
            }
            OnComplete();
        }

        private void OnComplete()
        {
            if (_isInFade)
                SignalBus<SignalNextStage>.Instance.Fire();
            else
                gameObject.SetActive(false);
            _fadeRoutine = null;
        }

    }
}