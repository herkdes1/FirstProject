using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Arrow_MoveAnim_Active : MonoBehaviour
{
    GameObject ArrowObject;
    Vector3 originalPos;
    Vector3 movePosUp;
    Vector3 movePosDown;
    bool Up;
    float moveAmount;
    void Start()
    {
        moveAmount = .05f;
        ArrowObject = this.gameObject;
        originalPos = ArrowObject.transform.position;
        if (originalPos.y < 4) { movePosUp = new Vector3(originalPos.x, originalPos.y + moveAmount, originalPos.z); } else { movePosUp = new Vector3(originalPos.x, originalPos.y - moveAmount, originalPos.z); }
        if (originalPos.y < 4) { movePosDown = new Vector3(originalPos.x, originalPos.y - moveAmount, originalPos.z); } else { movePosDown = new Vector3(originalPos.x, originalPos.y + moveAmount, originalPos.z); }
        StartCoroutine(MoveDown());
    }

    IEnumerator MoveDown()
    {
        while (ArrowObject.transform.position != movePosDown)
        {
            ArrowObject.transform.position = Vector3.MoveTowards(ArrowObject.transform.position, movePosDown, .005f);
            yield return new WaitForFixedUpdate();
        }
        StartCoroutine(MoveUP());
        StopCoroutine(MoveDown());
    }

    IEnumerator MoveUP()
    {
        while (ArrowObject.transform.position != movePosUp)
        {
            ArrowObject.transform.position = Vector3.MoveTowards(ArrowObject.transform.position, movePosUp, .005f);
            yield return new WaitForFixedUpdate();
        }
        StartCoroutine(MoveDown());
        StopCoroutine(MoveUP());
    }
}
