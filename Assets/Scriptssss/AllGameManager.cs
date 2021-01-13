using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.SceneManagement;

public class AllGameManager : MonoBehaviour
{
    public void MainScene()
    {
        SceneManager.LoadScene("MainScene");
    }

    public void AppleScene()
    {
        SceneManager.LoadScene("LevelAppleAnimTry");
    }

    public void CarrotScene()
    {
        SceneManager.LoadScene("LevelCarrot");
    }

    
}
