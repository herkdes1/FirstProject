using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class FurContainer : MonoBehaviour
{
    private Vector3 offset;
    private float zCoord;
    Camera cam;

    private float furCounter;
    public static Action OnGameWin = delegate {  };
    public static bool isDraggable;


    private void Awake()
    {
        cam = Camera.main;
    }

    private void OnMouseDown()
    {
        
        zCoord = cam.WorldToScreenPoint(transform.position).z;
        offset = transform.position - GetMouseWorldPos();
    }

    private void OnMouseDrag()
    {
        if (!isDraggable) { return;}
        Vector3 total = GetMouseWorldPos() + offset;
        transform.position = new Vector3(total.x, transform.position.y, total.z) ;
    }

    private Vector3 GetMouseWorldPos()
    {
        Vector3 mousePosition = Input.mousePosition;
        mousePosition.z = zCoord;
        return cam.ScreenToWorldPoint(mousePosition);
    }

    private void OnCollisionEnter(Collision other)
    {
        if (other.gameObject.CompareTag("Fur"))
        {
            furCounter++;
            Debug.Log(furCounter);
            Destroy(other.gameObject);

            if (furCounter == 3)
            {
                OnGameWin.Invoke();
            }
            
        }
    }
}
