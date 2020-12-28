namespace Base.Game.Environment
{
    using UnityEngine;
    using Base.Game.Signal;
    using System;
    using System.Collections;

    public class BasicCameraController : MonoBehaviour
    {
        [SerializeField] private Transform _target = null;
        [Space(10)]
        [SerializeField] private float _speed;

        private void Awake()
        {
            SignalBus<SignalStageStart>.Instance.Register(OnStageStart);
            _speed *= Time.fixedDeltaTime;
        }

        private void OnDestroy()
        {
            SignalBus<SignalStageStart>.Instance.UnRegister(OnStageStart);
        }

        private void OnStageStart()
        {
            StartCoroutine(MoveToTarget(_target));
        }

        private IEnumerator MoveToTarget(Transform target)
        {
            var wait = new WaitForFixedUpdate();
            float distance = Vector3.Distance(transform.position, target.position);
            while (distance > .5f)
            {
                transform.position = Vector3.MoveTowards(transform.position, target.position, _speed);
                transform.rotation = Quaternion.RotateTowards(transform.rotation, target.rotation, _speed * 10);
                distance = Vector3.Distance(transform.position, target.position);
                Camera.main.fieldOfView -= _speed;
                if (Camera.main.fieldOfView < 40)
                    Camera.main.fieldOfView = 40;
                yield return wait;
            }

        }

    }
}
