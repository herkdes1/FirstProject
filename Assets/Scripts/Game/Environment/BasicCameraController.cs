namespace Base.Game.Environment
{
    using UnityEngine;
    using Base.Game.Signal;
    using System;
    using System.Collections;
    using Base.Util;

    public class BasicCameraController : MonoBehaviour
    {
        [SerializeField] private Transform[] _positions = null;
        [Space(10)]
        [SerializeField] private float _speed;
        private float _targetFieldOfView = 40;

        private int _indicator = 0;

        private Vector3 _target;

        private void Awake()
        {
            Registration();
            _speed *= Time.fixedDeltaTime;
        }

        private void OnDestroy()
        {
            UnRegistration();
        }

        private void Registration()
        {
            SignalBus<SignalWoundSelection, Transform>.Instance.Register(T);
            SignalBus<SignalWoundSelection, Transform>.Instance.Register(OnWoundSelection);
            SignalBus<SignalStageStart>.Instance.Register(T);
            SignalBus<SignalStageStart>.Instance.Register(OnStageStart);

        }

        private void T()
        {

        }

        private void T(Transform obj)
        {

        }

        private void OnStageStart()
        {
            _indicator = 1;
            _targetFieldOfView = 40;
            _target = _positions[_indicator].position;
            StartCoroutine(MoveToTarget(_target));
        }

        private void OnCameraPositionChanged()
        {
            StopAllCoroutines();
            StartCoroutine(MoveToTarget(_target));
        }

        private void UnRegistration()
        {
            SignalBus<SignalWoundSelection, Transform>.Instance.UnRegister(OnWoundSelection);
            SignalBus<SignalWoundSelection, Transform>.Instance.UnRegister(T);
            SignalBus<SignalStageStart>.Instance.UnRegister(OnStageStart);
            SignalBus<SignalStageStart>.Instance.UnRegister(T);
        }
        
        private void OnWoundSelection(Transform obj)
        {
            if(obj == null)
            {
                _indicator = 1;
                _targetFieldOfView = 40;
                _target = _positions[_indicator].position;
                StopAllCoroutines();
                StartCoroutine(MoveToTarget(_target));
                return;
            }
            _targetFieldOfView = 90;
            _indicator=2;
            Vector3 target = new Vector3(obj.position.x, _positions[_indicator].position.y, _positions[_indicator].position.z);
            _target = target;
            StopAllCoroutines();
            StartCoroutine(MoveToTarget(_target));
        }
        

        private IEnumerator MoveToTarget(Transform target)
        {
            var wait = new WaitForFixedUpdate();
            float distance = Vector3.Distance(transform.position, target.position);
            while (distance > .5f || Mathf.Abs(Camera.main.fieldOfView - _targetFieldOfView) > .5f)
            {
                transform.position = Vector3.MoveTowards(transform.position, target.position, _speed);
                transform.rotation = Quaternion.RotateTowards(transform.rotation, target.rotation, _speed * 10);
                distance = Vector3.Distance(transform.position, target.position);
                Camera.main.fieldOfView = Mathf.Lerp(Camera.main.fieldOfView, _targetFieldOfView, .12f);
                yield return wait;
            }

        }

        private IEnumerator MoveToTarget(Vector3 target)
        {
            var wait = new WaitForFixedUpdate();
            float distance = Vector3.Distance(transform.position, target);
            while (distance > .5f || Mathf.Abs(Camera.main.fieldOfView - _targetFieldOfView) > .5f)
            {
                transform.position = Vector3.MoveTowards(transform.position, target, _speed);
                distance = Vector3.Distance(transform.position, target);
                Camera.main.fieldOfView = Mathf.Lerp(Camera.main.fieldOfView, _targetFieldOfView,.12f);
                yield return wait;
            }

        }

    }
}
