using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Pofuduk : MonoBehaviour
{
    bool stateControl;
    private void Start()
    {
        stateControl = true;
    }
    private void Update()
    {
        if(Kasagi.state == 2 && stateControl)
        {
           gameObject.AddComponent<DragObject>();
            stateControl = false;
        }
    }
}
