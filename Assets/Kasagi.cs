using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using DG.Tweening;
using UnityEngine.SceneManagement;

public class Kasagi : MonoBehaviour
{
    Vector3 posVector;

    float state0PosX;
    static public int state;

    float cleanProgress;
    public Camera MainCam;
    public Vector3 CamNewPos;
    public ParticleSystem smoke;
    int touchCount = 0;

    public Image progressImage;

    public GameObject arrow;
    public GameObject horseFrontFurs;
    bool horseTurn = false;
    private void Start()
    {
        state0PosX = 1f;
        cleanProgress = 0;
    }
    void Update()
    {
        KasagiHareketAlani();
    }
    private void OnTriggerStay(Collider other)
    {
        if (other.CompareTag("Horse"))
        {
            if (cleanProgress > 55.5f)
            {
                other.transform.DORotate(new Vector3(0, 90, 0), 0.5f);
                other.transform.position = Vector3.forward * 4.3f;
                horseFrontFurs.SetActive(false);
                if (!horseTurn)
                {
                    touchCount = 5;
                    horseTurn = true;
                }
            }
            if(cleanProgress > 100)
            {
                FindObjectOfType<UIManager>().ShowWinScreen();
            }
        }
    }
    private void OnTriggerEnter(Collider other)
    {
        if (other.CompareTag("Clean"))
        {
            touchCount++;
            if (touchCount > 10)
            {
                other.gameObject.SetActive(false);
                cleanProgress += 0.09f;
                smoke.Play();
                progressImage.fillAmount = cleanProgress / 100;
            }
        }
    }
    public void RestartGame()
    {
        SceneManager.LoadScene(0);
    }

    public void KasagiHareketAlani()
    {
        posVector = transform.position;

        if(Input.GetMouseButtonDown(0) && state0PosX != 0.3f)
        {
            Ray ray = Camera.main.ScreenPointToRay(Input.mousePosition);
            RaycastHit hit;

            if (Physics.Raycast(ray, out hit))
            {
                if (hit.transform.name == "Horse")
                {
                    MainCam.transform.DOMove(CamNewPos, 1.5f);
                    arrow.SetActive(false);
                    state0PosX = 0.3f;
                    GetComponent<MeshRenderer>().enabled = true;
                }
            }
        }

        posVector.y = Mathf.Clamp(posVector.y, 0.85f, 1.15f);
        if (state0PosX == 0.3f)
            posVector.x = Mathf.Clamp(posVector.x, -0.21f, 0.41f);

        posVector.z = 4f;

        if(transform.position.x >= 0)
        {
            transform.DORotate(new Vector3(0, 60, -90), 0);
        }
        else
        {
            transform.DORotate(new Vector3(0, 120, -90), 0);
        }

        transform.position = posVector;
    }
}
