using UnityEngine;
using UnityEngine.EventSystems;
using UnityEngine.UI;
using Base.Game.Signal;
using UnityEngine.SceneManagement;

namespace YusufTS
{
    public class TS_UIManager : MonoBehaviour
    {
        public GameObject endScreen;
        public GameObject startScreen;

        public void ShowWinScreen()
        {
            endScreen.SetActive(true);
        }

        public void StartButton()
        {
            startScreen.SetActive(false);
        }

        public void RestartLevel()
        {
            SceneManager.LoadScene(SceneManager.GetActiveScene().name);

        }
        
    }
}

