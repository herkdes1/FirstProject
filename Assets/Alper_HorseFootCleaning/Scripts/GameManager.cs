using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

namespace Alper_HorseFootCleaning.Scripts
{
    public class GameManager : MonoBehaviour
    {
    
        public GameObject startScreen;
        public GameObject winScreen;

        public static Action OnGameStart = delegate {  };
    

        private void OnEnable()
        {
            Shaver.OnGameWin += ShowWinScreen;
        }
        private void OnDisable()
        {
            Shaver.OnGameWin -= ShowWinScreen;
        }

        void ShowWinScreen()
        {
            winScreen.SetActive(true);
        }
   

        public void CloseStartScreen()
        {
            startScreen.SetActive(false);
            OnGameStart.Invoke();
        }

        public void RestartGame()
        {
            SceneManager.LoadScene(SceneManager.GetActiveScene().name);
        }

    }
}

