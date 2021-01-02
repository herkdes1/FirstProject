namespace Base.Game.Environment
{
    using Base.Game.InteractableObject;
    using Base.Game.Signal;
    using System;
    using System.Collections;
    using TMPro;
    using UnityEngine;
    [RequireComponent(typeof(TextMeshPro))]
    public class EarnCoinTextObject : MonoBehaviour
    {
        [SerializeField] private int _value = 10;
        [Space(20)]
        [SerializeField] private float _movementSpeed;

        private TextMeshPro _text;
        private void Awake()
        {
            _text = GetComponent<TextMeshPro>();
            _text.text = _value + "$";
            _movementSpeed *= Time.fixedDeltaTime;
            Registration();
            DeActive();
        }
        private void OnDestroy()
        {
            UnRegistration();
        }
        private void Registration()
        {
            SignalBus<SignalInteractableObjectDestroy, IInteractableObject>.Instance.Register(OnObjectDestroyed);
        }

        private void OnObjectDestroyed(IInteractableObject obj)
        {
            transform.position = new Vector3(obj.GetTransform().position.x, obj.GetTransform().position.y, transform.position.z);
            Active();
            Handheld.Vibrate();
        }

        private void UnRegistration()
        {
            SignalBus<SignalInteractableObjectDestroy, IInteractableObject>.Instance.UnRegister(OnObjectDestroyed);
        }

        private void OnEnable()
        {
            SignalBus<SignalAddCoin, int>.Instance.Fire(_value);
            StopAllCoroutines();
            StartCoroutine(MovementAction());
        }

        private IEnumerator MovementAction()
        {
            var wait = new WaitForFixedUpdate();
            Vector3 target = new Vector3(transform.position.x, transform.position.y + 1f, transform.position.z);
            while(Vector3.Distance(transform.position, target) > .5f)
            {
                transform.position = Vector3.MoveTowards(transform.position, target, _movementSpeed);
                yield return wait;
            }
            DeActive();
        }

        private void Active()
        {
            gameObject.SetActive(true);
        }

        private void DeActive()
        {
            gameObject.SetActive(false);
        }

    }
}