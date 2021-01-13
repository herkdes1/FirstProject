using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class FoodTrigger : MonoBehaviour
{
    
    public static Action OnFoodCompleted = delegate {  };

    
    private void OnTriggerEnter(Collider other)
    {
        if (other.gameObject.CompareTag("FoodContainer"))
        {
            Shaver.isDraggable = true;
            OnFoodCompleted.Invoke();
        }
        
    }
}
