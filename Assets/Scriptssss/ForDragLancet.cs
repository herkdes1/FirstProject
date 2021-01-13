using System.Collections;
using System.Collections.Generic;
using UnityEngine;
public class ForDragLancet : MonoBehaviour
{
    public float rotSpeed=5f;

    void OnMouseDrag()
    {
        float rotY = Input.GetAxis("Mouse Y") * rotSpeed * Mathf.Deg2Rad;

        transform.RotateAround(Vector3.right, rotY);
        
     }



}
