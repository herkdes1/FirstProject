using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class FoodContainer : MonoBehaviour
{
    private Vector3 offset;
    private float zCoord;
    Camera cam;

    public static bool isDraggable;
    
    public static Action FirstClickOnFood = delegate {  };

    private void Awake()
    {
        cam = Camera.main;
    }

    private void OnMouseDown()
    {
        FirstClickOnFood.Invoke();
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
}
