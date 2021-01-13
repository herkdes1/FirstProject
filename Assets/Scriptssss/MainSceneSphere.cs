using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class MainSceneSphere : MonoBehaviour
{
    public float TurnSpeed = 2f;

    void OnMouseDrag()
    {
        /*
        float rotX = Input.GetAxis("Mouse X") * TurnSpeed * Mathf.Deg2Rad;

        transform.RotateAround(Vector3.up, -rotX);
        */
        float rotX = Input.GetAxis("Mouse X") * TurnSpeed * Mathf.Deg2Rad;

        transform.RotateAround(Vector3.up, rotX);

    }


}
