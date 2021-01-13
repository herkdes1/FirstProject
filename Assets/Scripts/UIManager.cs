using UnityEngine;
using UnityEngine.EventSystems;
using UnityEngine.UI;

using UnityEngine.SceneManagement;
public class UIManager : MonoBehaviour
{
    public GameObject winScreen;
    public GameObject startScreen;

    private void Awake()
    {
        Time.timeScale = 0;
    }

    public void ShowWinScreen()
    {
        winScreen.SetActive(true);
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
}
