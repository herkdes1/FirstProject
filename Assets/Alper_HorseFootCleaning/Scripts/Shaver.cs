using Base.Game.InteractableObject;
using Base.Game.Signal;
using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;


namespace  Alper_HorseFootCleaning.Scripts
{
    public class Shaver : MonoBehaviour
{
    public ParticleSystem particle;
    private Vector3 offset;
    private float zCoord;
    Camera cam;
    public Transform top;
    public LayerMask mask;
    private bool isDragging;

    private int shavedHair;

    public Image fillImage;
    
    public static Action OnGameWin = delegate {  };

    private void Awake()
    {
        cam = Camera.main;
    }

    private void Start()
    {
        Vibration.Init ();
    }


    private void OnMouseDown()
    {
        zCoord = cam.WorldToScreenPoint(transform.position).z;
        offset = transform.position - GetMouseWorldPos();
    }

    private void OnMouseDrag()
    {
        isDragging = true;
        Vector3 total = GetMouseWorldPos() + offset;
        transform.position = new Vector3(total.x, transform.position.y, total.z);
    }
    
    private void OnMouseUp()
    {
        isDragging = false;
    }

    private Vector3 GetMouseWorldPos()
    {
        Vector3 mousePosition = Input.mousePosition;
        mousePosition.z = zCoord;
        return cam.ScreenToWorldPoint(mousePosition);
    }

    private void Update()
    {
        if (isDragging)
        {
            RaycastHit hit;

            if (Physics.Raycast(top.position, -transform.up+ new Vector3(0,0,-.3f), out hit, 30, mask))
            {
                shavedHair++;
                
                ShavedHairScore();
           
                Vibration.VibratePop ();
                GameObject hitGameObject = hit.transform.gameObject;
                ActivateParticles(hitGameObject);
            
                
            }
            //Debug.DrawRay(top.position, -transform.up + new Vector3(0,0,-.3f), Color.green, .01f, false);
        }
        
    }

    void ShavedHairScore()
    {
        float fillAmount = 0.333f;

        if (shavedHair == 25)
        {
            fillImage.fillAmount = fillAmount;
        }
        else if (shavedHair == 50)
        {
            fillImage.fillAmount = fillAmount * 2;
        }
        else if (shavedHair == 80)
        {
            fillImage.fillAmount = fillAmount * 3;
                foreach (Dust obj in FindObjectsOfType<Dust>())
                    obj.gameObject.SetActive(false);
        }

        SignalBus<SignalAddCoin, int>.Instance.Fire(2);
    }
    
    void ActivateParticles(GameObject hitGameObject)
    {
        var emitParams = new ParticleSystem.EmitParams();
        emitParams.position = transform.position ;
        emitParams.velocity = new Vector3(0.0f, .5f, 0.0f);
        particle.Emit(emitParams, 5);
        hitGameObject.SetActive(false);
    }

}
}
