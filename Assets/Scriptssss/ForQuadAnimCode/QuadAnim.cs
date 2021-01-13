using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class QuadAnim : MonoBehaviour
{

    public GameObject Quad1;
    public GameObject Quad1Animm;

    public GameObject Quad2;
    public GameObject Quad2Animm;

    public GameObject Quad3;
    public GameObject Quad3Animm;


    /*public GameObject LancetWin;
    public GameObject MoveLancet;
    public GameObject Twizz;
    public GameObject FirtTrainer;
    public GameObject SecondTrainer;
    */


    public int StitchPoint;

    void Start()
    {
        StitchPoint = 0;
    }


    private void OnTriggerEnter(Collider other)
    {
        if (other.gameObject.tag =="Quad1")
        {
            Quad1.SetActive(false);
            Quad1Animm.SetActive(true);
            other.gameObject.SetActive(false);
            StitchPoint++;
        }
        else if (other.gameObject.tag == "Quad2")
        {           
            Quad2.SetActive(false);
            Quad2Animm.SetActive(true);
            other.gameObject.SetActive(false);
            StitchPoint++;
        }
        else if (other.gameObject.tag == "Quad3")
        {
            Quad3.SetActive(false);
            Quad3Animm.SetActive(true);
            other.gameObject.SetActive(false);
            StitchPoint++;
        }
        /*
        else if (StitchPoint >2)
        {
            MoveLancet.SetActive(false);
            LancetWin.SetActive(true);
            Twizz.SetActive(true);
            FirtTrainer.SetActive(false);
            SecondTrainer.SetActive(true);
            
        }
        */
    }

}
