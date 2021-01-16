using UnityEngine;
using UnityEngine.EventSystems;
using UnityEngine.UI;

namespace YusufMS
{

    public class UIManager : MonoBehaviour
    {
        public GameObject endScreen;
        public GameObject startScreen;
        public GameObject horseFurs;

        public void ShowWinScreen()
        {
            endScreen.SetActive(true);
            horseFurs.SetActive(false);
        }

        public void StartButton()
        {
            startScreen.SetActive(false);
        }
    }

}