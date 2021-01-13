using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ForTweezzzz : MonoBehaviour
{
    public GameObject Iron;
    public GameObject Twezz;
    public GameObject Twezzz2;
    public GameObject AppleInGrub;


    void OnTriggerEnter(Collider other)
    {
        if (other.gameObject.tag == "Grub")
        {

            Iron.SetActive(true);
            Twezz.SetActive(false);
            Twezzz2.SetActive(true);
            AppleInGrub.SetActive(false);    

           

        }
    }
}
