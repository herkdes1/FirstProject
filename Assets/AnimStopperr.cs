using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class AnimStopperr : MonoBehaviour
{
    public GameObject ThisTwizz;
    public GameObject OriginalTwizz;
    public GameObject Grub;
   
    void OnTriggerEnter(Collider other)
    {
        if (other.gameObject.tag == "AnimStop")
        {
            ThisTwizz.SetActive(false);
            OriginalTwizz.SetActive(true);
            Grub.SetActive(false);
            
        }
    }
}
