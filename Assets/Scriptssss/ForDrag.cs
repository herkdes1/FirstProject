using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ForDrag : MonoBehaviour
{


    public float distance = 5.5f;
    public GameObject Lancet;

/*
    public void OnMouseDown()
    {
        Lancet.gameObject.SetActive(true);

    }

*/
    


        /*

     private void OnMouseDrag()
    {
      //  Lancet.gameObject.SetActive(true);
        
        Vector3 mousePos = new Vector3(Input.mousePosition.x, Input.mousePosition.y, distance);
        Vector3 objectPos = Camera.main.ScreenToWorldPoint(mousePos);
        transform.position = objectPos;
        
    }
    */



   


    Rigidbody rb;
    
    
    // Start is called before the first frame update
    void Start()
    {
        rb = GetComponent<Rigidbody>();        
    }

    private void OnMouseDrag()
    {
        Vector3 mousePos = new Vector3(Input.mousePosition.x, Input.mousePosition.y, -Camera.main.transform.position.z + transform.position.z);
        Vector3 objPos = Camera.main.ScreenToViewportPoint(mousePos);
        transform.position = objPos;
        rb.isKinematic = true;
        
    }

    private void OnMouseUp()
    {
        rb.isKinematic = false;
    }
    
}
