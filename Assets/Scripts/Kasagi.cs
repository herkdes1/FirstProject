using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using DG.Tweening;
using UnityEngine.SceneManagement;
using Base.Game.Signal;
using TMPro;

public class Kasagi : MonoBehaviour
{
    Vector3 posVector;

    float state0PosX;
    static public int state;

    int coin;
    float coinProgress;
    public Camera MainCam;
    public Vector3 CamNewPos;
    public ParticleSystem smoke;
    public TMP_Text coinText;

    public Slider progress;

    public GameObject arrow;
    public GameObject kasagiHairleri;
    public GameObject horseFrontFurs;
    bool horseTurn = false;
    private void Start()
    {
        state0PosX = 1f;
        coin = 0;
    }
    void Update()
    {
        KasagiHareketAlani();
    }
    private void OnTriggerStay(Collider other)
    {
        if (other.CompareTag("Horse"))
        {
            if (coinProgress > 49.5f)
            {
                other.transform.DORotate(new Vector3(0, 90, 0), 0.5f);
                other.transform.position = Vector3.forward * 4.3f;
                horseFrontFurs.SetActive(false);
            }
            if(coinProgress > 100)
            {
                FindObjectOfType<UIManager>().ShowWinScreen();
            }
        }
    }
    private void OnTriggerEnter(Collider other)
    {
        if (other.CompareTag("Clean"))
        {
            other.transform.DOLocalRotateQuaternion(Quaternion.Euler(0f, -14f, 74f), 0.5f);
            other.GetComponent<BoxCollider>().enabled = false;
            coin += 1;
            smoke.Play();
            coinProgress = coin * 0.40f;
            coinText.text = (coinProgress*3f).ToString("F0");
            HapticAction.Vibrate();

            progress.value = coinProgress;

            if (!horseTurn && coinProgress > 20)
            {
                kasagiHairleri.SetActive(true);
                horseTurn = !horseTurn;
            }

            SignalBus<SignalCoinChange, int>.Instance.Fire(1);
        }
    }
    public void RestartGame()
    {
        SceneManager.LoadScene(0);
    }
    public void NextLevel()
    {
        SignalBus<SignalNextStage>.Instance.Fire();
    }
    void BrushActivated()
    {
        GetComponent<MeshRenderer>().enabled = true;
        gameObject.AddComponent<DragObject>();
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
                    MainCam.transform.DOMove(CamNewPos, 1.5f).OnComplete(() => BrushActivated());
                    arrow.SetActive(false);
                   // hit.collider.enabled = false;
                    state0PosX = 0.3f;
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
