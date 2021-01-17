using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Shake_Teeth : MonoBehaviour
{

    Vector3 originalPos;
    bool shaking;
    float shakeAmount = .02f;

    public void Shake()
    {
        StartCoroutine(StartStopShake());
    }
    Vector3 RandomPos()
    {
        return new Vector3(Random.Range(originalPos.x - shakeAmount, originalPos.x + shakeAmount), Random.Range(originalPos.y - shakeAmount, originalPos.y + shakeAmount), Random.Range(originalPos.z - shakeAmount, originalPos.z + shakeAmount));
    }
    IEnumerator ShakeThisObject()
    {
        while (shaking)
        {
            this.gameObject.transform.position = RandomPos();
            yield return new WaitForFixedUpdate();
        }
    }
    IEnumerator StartStopShake()
    {
        originalPos = this.gameObject.transform.position;
        shaking = true;
        StartCoroutine(ShakeThisObject());
        yield return new WaitForSeconds(0.2f);
        shaking = false;
        StopCoroutine(ShakeThisObject());
        this.gameObject.transform.position = originalPos;
    }
}
