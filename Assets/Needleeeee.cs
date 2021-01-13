using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Needleeeee : MonoBehaviour
{
    public int StitchPoint;

    //public Animator Anim;

    public Animator ArmAnim;


    //public GameObject LancetWin;
    public GameObject Needle;
    public GameObject NeedleWin;
    public GameObject WinScene;
    public GameObject PainFace;
    public GameObject PainEye;
    public GameObject HappyMouth;



    public GameObject QuadAnim1;
    public GameObject QuadAnim2;
    public GameObject QuadAnim3;

    
    void Start()
    {
        StitchPoint = 0;
    }








    void OnTriggerEnter(Collider other)
    {
        if (other.gameObject.tag == "NeedleClose")
        {
            other.gameObject.SetActive(false);
            StitchPoint++;

            if (StitchPoint > 2)
            {
               // Anim.SetBool("Open", false);
                ArmAnim.SetBool("WinArm", true);
                Needle.SetActive(false);
                NeedleWin.SetActive(true);
                WinScene.SetActive(true);
                PainFace.SetActive(false);
                PainEye.SetActive(false);
                HappyMouth.SetActive(true);
            }


        }
        else if (other.gameObject.tag == "Quad1")
        {
            QuadAnim1.SetActive(true);
        }
        else if (other.gameObject.tag == "Quad2")
        {
            QuadAnim2.SetActive(true);
        }
        else if (other.gameObject.tag == "Quad3")
        {
            QuadAnim3.SetActive(true);
        }
    }


    /*
    private void OnTriggerExit(Collider other)
    {
        if (other.gameObject.tag == "Quad1")
        {
            QuadAnim1.SetActive(true);
        }
        else if (other.gameObject.tag == "Quad2")
        {
            QuadAnim2.SetActive(true);
        }
        else if (other.gameObject.tag == "Quad3")
        {
            QuadAnim3.SetActive(true);
        }
    }

    */
}
