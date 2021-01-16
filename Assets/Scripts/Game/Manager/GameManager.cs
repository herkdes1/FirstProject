namespace Base.Game.Manager
{
    using Base.Game.Signal;
    using System;
    using UnityEngine;
    using UnityEngine.SceneManagement;

    public class GameManager : MonoBehaviour
    {
        public static GameManager Instance { get; private set; }

        private void Awake()
        {
            if(Instance != null)
            {
                Destroy(gameObject);
                return;
            }
            Registration();
            DontDestroyOnLoad(gameObject);
            Instance = this;
        }

        private void OnDestroy()
        {
            UnRegistration();
        }
        private void Update()
        {
            if (Input.GetKeyDown(KeyCode.Return))
            {
                SignalBus<SignalNextStage>.Instance.Fire();
            }
        }
        private void Registration()
        {
            SignalBus<SignalNextStage>.Instance.Register(OnNextStage);
        }

        private void UnRegistration()
        {
            SignalBus<SignalNextStage>.Instance.UnRegister(OnNextStage);
        }

        private void OnNextStage()
        {
            if (SceneManager.GetActiveScene().buildIndex + 1 <= SceneManager.sceneCount)
                SceneManager.LoadScene(SceneManager.GetActiveScene().buildIndex + 1);
            else
                SceneManager.LoadScene(0);
        }

    }
}