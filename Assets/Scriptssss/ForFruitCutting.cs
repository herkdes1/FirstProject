using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ForFruitCutting : MonoBehaviour
{

    public int StitchPoint;

    //public Animator Anim;

    public GameObject LancetWin;
    public GameObject MoveLancet;
    public GameObject Twizz;
    public GameObject FirtTrainer;
    public GameObject SecondTrainer;


    // Bu bir yorum satırıdırrrrrrrr
    void Start()
    {
        StitchPoint = 0;
    }



    private void OnTriggerEnter(Collider other)
    {
        if (other.gameObject.tag =="Stitch")
        {
            other.gameObject.SetActive(false);
            StitchPoint++;

            if (StitchPoint>2)
            {
                //Anim.SetBool("Open", true);
                MoveLancet.SetActive(false);
                LancetWin.SetActive(true);
                Twizz.SetActive(true);
                FirtTrainer.SetActive(false);
                SecondTrainer.SetActive(true);

            }
        }
    }
}
