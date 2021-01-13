using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Shaver : MonoBehaviour
{
    //public GameObject levelCompletedEffect;
    //public ParticleSystem particle;
    public GameObject furs;
    private Vector3 offset;
    private float zCoord;
    Camera cam;
    Transform top;
    public static bool isDraggable;
    private float furCounter;

    public static Action OnGameWin = delegate { };
    public static Action FirstClickOnCutter = delegate {  };
    public static Action OnFurContainerOpen = delegate {  };

    

    private void Awake()
    {
        cam = Camera.main;
        top = GetComponentInChildren<Transform>();
    }

    private void OnMouseDown()
    {
        FirstClickOnCutter.Invoke();
        zCoord = cam.WorldToScreenPoint(transform.position).z;
        offset = transform.position - GetMouseWorldPos();
    }

    private void OnMouseDrag()
    {
        if (!isDraggable) { return;}
        Vector3 total = GetMouseWorldPos() + offset;
        transform.position = new Vector3(total.x, total.y, transform.position.z);
    }

    private Vector3 GetMouseWorldPos()
    {
        Vector3 mousePosition = Input.mousePosition;
        mousePosition.z = zCoord;
        return cam.ScreenToWorldPoint(mousePosition);
    }

    private void Update()
    {
        RaycastHit hit;

        if (Physics.Raycast(top.position, -transform.up, out hit, 20))
        {
            //particle.Emit(5);

            GameObject hitGameObject = hit.transform.gameObject;
            Rigidbody hitObjectRB = hitGameObject.GetComponent<Rigidbody>();
            hitObjectRB.isKinematic = false;

            int random = UnityEngine.Random.Range(5, 10);
            hitObjectRB.AddForce(new Vector3(0, -random, 0), ForceMode.Impulse);
            //Handheld.Vibrate();

            int randomToDelete = UnityEngine.Random.Range(1, 4);
            if (randomToDelete == 3)
            {

            }
            else
            {
                hitObjectRB.velocity = Vector3.zero;
                StartCoroutine(DeleteFur(hitGameObject));
                if (furCounter == 5)
                {
                    OnFurContainerOpen.Invoke();
                    DraggableFur.isDraggable = true;
                }
            }
            

        }

        if (furs.transform.childCount <= 0)
        {
            //isi bitince shaverin pozisyonunu ilk poz a al.
            //OnGameWin.Invoke();
            //levelCompletedEffect.SetActive(true);
        }

        Debug.DrawRay(top.position, -transform.up, Color.green, .01f, false);
        

    }

    IEnumerator DeleteFur(GameObject go)
    {
        furCounter++;
        yield return new WaitForSeconds(1);
        
        Destroy(go);
    }


}
