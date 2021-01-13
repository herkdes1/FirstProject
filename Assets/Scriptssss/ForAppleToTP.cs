using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ForAppleToTP : MonoBehaviour
{

    public GameObject TTPIcone;
    public Animator Anim;
    public GameObject AllApple;
    public GameObject Stit;
    public GameObject ProgBar;
    public GameObject Trainer;
    public GameObject HelpSound;


    public void TouchToPlay()
    {
        TTPIcone.SetActive(false);
        Anim.SetBool("CamPlay",true);
        AllApple.SetActive(true);
        Stit.SetActive(true);
        ProgBar.SetActive(true);
        Trainer.SetActive(true);
        HelpSound.SetActive(true);
    }

}
