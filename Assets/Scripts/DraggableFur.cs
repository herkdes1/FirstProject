using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class DraggableFur : MonoBehaviour
{
    private Vector3 offset;
    private float zCoord;
    Camera cam;
    public static bool isDraggable;
    
    public static Action FirstClickOnFur = delegate {  };


    private void Awake()
    {
        cam = Camera.main;
    }

    private void OnMouseDown()
    {
        FirstClickOnFur.Invoke();
        zCoord = cam.WorldToScreenPoint(transform.position).z;
        offset = transform.position - GetMouseWorldPos();
    }

    private void OnMouseDrag()
    {
        if (!isDraggable) { return;}
        Vector3 total = GetMouseWorldPos() + offset;
        transform.position = new Vector3(total.x, total.y, transform.position.z) ;
    }

    private Vector3 GetMouseWorldPos()
    {
        Vector3 mousePosition = Input.mousePosition;
        mousePosition.z = zCoord;
        return cam.ScreenToWorldPoint(mousePosition);
    }

}
