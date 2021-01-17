using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ArrowFollow_Active : MonoBehaviour
{
    public GameObject toFollow;

    void Update()
    {
        Vector3 v = toFollow.transform.position;
        v.z = 0;
        transform.LookAt(transform.position + transform.forward, v - transform.position);
    }
}
