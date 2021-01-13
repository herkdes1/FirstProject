using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.SceneManagement;

public class MainManager : MonoBehaviour
{
    public GameObject Bottom;
    public GameObject TapToPlayImg;
    public GameObject PauseScene;
    public GameObject PauseObj;

    public void TapToPlay()
    {
        TapToPlayImg.SetActive(false);
        Bottom.SetActive(true);
    }
    public void OpenPauseScene()
    {
        PauseScene.SetActive(true);
        PauseObj.SetActive(false);
    }
    public void ClosePauseScene()
    {
        PauseScene.SetActive(false);
        PauseObj.SetActive(true);
    }

    public void OpenFruitShop()
    {
        SceneManager.LoadScene("SelectFruit");
    }

    public void OpenMaterials()
    {
        SceneManager.LoadScene("Materials");
    }
    public void LevelApple()
    {
        SceneManager.LoadScene("LevelAppleAnimTry");
    }
    
}
