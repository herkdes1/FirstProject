﻿using UnityEngine;
using System.Collections;
using System.Collections.Generic;
using UnityEngine.UI;
using DG.Tweening;
using UnityEngine.SceneManagement;
using System;
using Base.Game.Signal;

public class Firca : MonoBehaviour
{
    Vector3 posVector;
    static public int state;

    public GameObject horseDirties;

    int coin;
    float coinProgress;
    public Camera MainCam;
    public Vector3 CamNewPos;
    public ParticleSystem smoke;
    public GameObject confeti;

    public Image progressImage;
    public GameObject arrow;

    private void Start()
    {
        coin = 0;
    }
    void Update()
    {
        FircaHareketAlani();
    }

    private void OnTriggerEnter(Collider other)
    {
        if (other.CompareTag("Clean"))
        {
            Color col = other.GetComponent<MeshRenderer>().material.color;
            col.a -= 0.33f;
            other.GetComponent<MeshRenderer>().material.color = col;

            if (other.GetComponent<MeshRenderer>().material.color.a < 0.01)
            {
                other.gameObject.SetActive(false);
            }
            HapticAction.Vibrate();
            coin += 3;
            SignalBus<SignalCoinChange, int>.Instance.Fire(3);
            smoke.Play();
            coinProgress = coin * 0.007f;
            progressImage.fillAmount = coinProgress;
            Debug.Log(coinProgress);

            if (coinProgress > 1)
            {
                horseDirties.SetActive(false);
                StartCoroutine(ShowWinScreenDelay()); 
            }
        }
    }
    public void RestartGame()
    {
        SceneManager.LoadScene(0);
    }

    public void FircaHareketAlani()
    {
        posVector = transform.position;

        posVector.y = Mathf.Clamp(posVector.y, 0.95f, 1.11f);

        posVector.x = Mathf.Clamp(posVector.x, 0.01f, 0.09f);

        posVector.z = 2.9f;

        transform.position = posVector;

        if (Input.GetMouseButtonDown(0))
        {
            Ray ray = Camera.main.ScreenPointToRay(Input.mousePosition);
            RaycastHit hit;

            if (Physics.Raycast(ray, out hit))
            {
                if (hit.transform.name == "Horse")
                {
                    MainCam.transform.DOMove(CamNewPos, 1.5f).OnComplete(() => BrushActivated());
                    arrow.SetActive(false);
                    hit.collider.enabled = false;

                }
            }
        }
    }

    void BrushActivated()
    {
        GetComponent<MeshRenderer>().enabled = true;
        gameObject.AddComponent<DragObject>();
    }
    IEnumerator ShowWinScreenDelay()
    {
        confeti.gameObject.SetActive(true);
        smoke.gameObject.SetActive(false);
        GetComponent<MeshRenderer>().enabled = false;
        yield return new WaitForSeconds(1.5f);
        FindObjectOfType<UIManager>().ShowWinScreen();
    }
}