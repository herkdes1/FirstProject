using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ForGrubStopper : MonoBehaviour
{
    private Rigidbody rigid;
    private void Awake()
    {
        rigid = GetComponent<Rigidbody>();
    }

    void Update()
    {
        if (transform.position.x > 1.7f)
        {
            rigid.MovePosition(new Vector3(1.7f, transform.position.y, transform.position.z));
        }
        else if (transform.position.x < -1.5f)
        {
            rigid.MovePosition(new Vector3(-1.5f, transform.position.y, transform.position.z));
        }
        else if (transform.position.z > 4.0f)
        {
            rigid.MovePosition(new Vector3(transform.position.x, transform.position.y, 4.0f));
        }
        else if (transform.position.z < -2.3f)
        {
            rigid.MovePosition(new Vector3(transform.position.x, transform.position.y, -2.3f));
        }
    }
}
