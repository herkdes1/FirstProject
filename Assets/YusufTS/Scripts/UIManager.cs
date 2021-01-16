using UnityEngine;
using UnityEngine.EventSystems;
using UnityEngine.UI;
using Base.Game.Signal;
using UnityEngine.SceneManagement;

namespace YusufTS
{
    public class UIManager : MonoBehaviour
    {
        public GameObject endScreen;
        public GameObject startScreen;

        private void Awake()
        {
            Time.timeScale = 0;
        }

        private void OnDestroy()
        {
            Time.timeScale = 1;
        }

        public void ShowWinScreen()
        {
            endScreen.SetActive(true);
            Time.timeScale = 0;
        }

        public void StartButton()
        {
            startScreen.SetActive(false);
            Time.timeScale = 1;
        }

        public void RestartLevel()
        {
            Time.timeScale = 1;
            SceneManager.LoadScene(SceneManager.GetActiveScene().name);

        }
        public void NextLevel()
        {
            SignalBus<SignalNextStage>.Instance.Fire();
        }
    }
}

