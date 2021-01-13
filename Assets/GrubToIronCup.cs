using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GrubToIronCup : MonoBehaviour
{

    public GameObject GrubWin;
    public GameObject OriTwiz;
    public GameObject Needle;
    public GameObject NeedlePoint;
    public GameObject GrubInIron;
    public GameObject Trainer3;

    void OnTriggerEnter(Collider other)
    {
        if (other.gameObject.tag == "IronCup")
        {
            GrubWin.SetActive(true);
            OriTwiz.SetActive(false);
            Needle.SetActive(true);
            NeedlePoint.SetActive(true);
            GrubInIron.SetActive(true);
            Trainer3.SetActive(true);

        }
    }

}
