    using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ForStopper : MonoBehaviour
{
    private Rigidbody rigid;
    private void Awake()
    {
        rigid = GetComponent<Rigidbody>();
    }




    void Update()
    {
        if (transform.position.x > 0.25f)
        {
            rigid.MovePosition(new Vector3(0.25f, transform.position.y, transform.position.z));
        }
        else if (transform.position.x < 0.24f)
        {
            rigid.MovePosition(new Vector3(0.24f, transform.position.y, transform.position.z));
        }
        else if (transform.position.z > 6.0f)
        {
            rigid.MovePosition(new Vector3(transform.position.x, transform.position.y,6.0f));
        }
        else if (transform.position.z < -2.3f)
        {
            rigid.MovePosition(new Vector3(transform.position.x, transform.position.y, -2.3f));
        }
        else if (transform.position.y <5.0f)
        {
            rigid.MovePosition(new Vector3(transform.position.x, 5.0f,transform.position.z));
        }
        else if (transform.position.y > 5.0f)
        {
            rigid.MovePosition(new Vector3(transform.position.x, 5.0f, transform.position.z));
        }
    }
}
