using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ForClock : MonoBehaviour
{
    public float RotSpeed = 17f;

    private void FixedUpdate()
    {
        transform.Rotate(0, 0, RotSpeed * Time.deltaTime);
    }
    
}
