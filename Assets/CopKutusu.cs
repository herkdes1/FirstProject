using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class CopKutusu : MonoBehaviour
{
    public Text dusenPofText;
    float dusenPofNum;
    public Slider slider;
    public Text progress;
    public GameObject FinishPanel;
    private void OnTriggerEnter(Collider other)
    {
        if (other.CompareTag("ToplananPofuduk"))
        {
            other.gameObject.SetActive(false);
            other.tag = "DusenPofuduk";
            dusenPofNum += GameObject.FindGameObjectsWithTag("ToplananPofuduk").Length;
            other.gameObject.transform.SetParent(gameObject.transform);
            dusenPofText.text = dusenPofNum.ToString();
            slider.value = slider.value + 2;
            progress.text = slider.value.ToString();
            if(progress.text == "100")
            {
                FinishPanel.gameObject.SetActive(true);
            }
        }
    }
}
