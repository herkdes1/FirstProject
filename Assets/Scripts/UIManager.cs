using UnityEngine;
using UnityEngine.EventSystems;
using UnityEngine.UI;

public class UIManager : MonoBehaviour
{
    public GameObject endScreen;
    public GameObject startScreen;
    public GameObject horseFurs;

    private void Awake()
    {
        Time.timeScale = 0;
    }

    public void ShowWinScreen()
    {
        endScreen.SetActive(true);
        horseFurs.SetActive(false);
        Time.timeScale = 0;
    }

    public void StartButton()
    {
        startScreen.SetActive(false);
        Time.timeScale = 1;
    }
}
