using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ForCutting : MonoBehaviour
{
    public int StitchPoint;

    public Animator Anim;

    
    //public GameObject LancetWin;
    public GameObject MoveLancet;
    
    



    // Start is called before the first frame update
    void Start()
    {
        StitchPoint = 0;
    }


    void OnTriggerEnter(Collider other)
    {
        if (other.gameObject.tag =="Stitch")
        {
            other.gameObject.SetActive(false);
            StitchPoint++;

            if (StitchPoint > 2)
            {
                //LancetWin.SetActive(true);
                Anim.SetBool("Open",true);
                MoveLancet.SetActive(false);
                //TextImage.SetActive(true);
                //GrubZone.SetActive(true);
            }
            
        }
    }

}
