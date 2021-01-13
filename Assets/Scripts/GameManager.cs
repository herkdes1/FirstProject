using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GameManager : MonoBehaviour
{
    public GameObject arrowObject;
    public GameObject cutterObject;
    public GameObject furContainerObject;
    private Animator arrowAnimator;
    

    private void Awake()
    {
        arrowAnimator = arrowObject.GetComponent<Animator>();
    }

    private void OnEnable()
    {
        FoodContainer.FirstClickOnFood += CloseFoodArrowAnimation;
        FoodTrigger.OnFoodCompleted += ShowCutterIndicator;
        Shaver.FirstClickOnCutter += CloseCutterArrowAnimation;
        DraggableFur.FirstClickOnFur += CloseCutterArrowAnimation;
        Shaver.OnFurContainerOpen += ShowFurContainer;
    }

    private void OnDisable()
    {
        FoodContainer.FirstClickOnFood -= CloseFoodArrowAnimation;
        FoodTrigger.OnFoodCompleted -= ShowCutterIndicator;
        Shaver.FirstClickOnCutter -= CloseCutterArrowAnimation;
        DraggableFur.FirstClickOnFur -= CloseCutterArrowAnimation;
        Shaver.OnFurContainerOpen -= ShowFurContainer;
    }

    void CloseFoodArrowAnimation()
    {
        arrowAnimator.enabled = false;
        arrowObject.SetActive(false);
    }

    void ShowCutterIndicator()
    {
        Debug.Log("trigger");

        arrowObject.SetActive(true);
        arrowAnimator.enabled = true;
        arrowObject.transform.position += new Vector3(0,100,0);
        arrowObject.transform.Rotate(Vector3.forward, 50 );
        cutterObject.transform.position += new Vector3(-1,0,0);
        FoodContainer.isDraggable = false;

    }

    void ShowFurContainer()
    {
        arrowObject.SetActive(true);
        arrowAnimator.enabled = true;
        arrowObject.transform.Rotate(Vector3.forward, -10 );
        furContainerObject.transform.position += new Vector3(-1,0,0);
    }

    void CloseCutterArrowAnimation()
    {
        arrowAnimator.enabled = false;
        arrowObject.SetActive(false);
    }
}
