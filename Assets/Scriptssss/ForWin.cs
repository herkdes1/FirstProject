using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ForWin : MonoBehaviour
{

    public int GrubPoint;

    public GameObject WinImage;

    private void Start()
    {
        GrubPoint = 0;
    }


    void OnTriggerEnter(Collider other)
    {
        if (other.gameObject.tag == "Grub")
        {
            other.gameObject.SetActive(false);
            GrubPoint++;

            if (GrubPoint > 2)
            {

                WinImage.SetActive(true);
            }

        }
    }
}
