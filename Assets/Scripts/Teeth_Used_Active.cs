using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using System;
public class Teeth_Used_Active : MonoBehaviour
{
    #region Public Fields

    #endregion

    #region Private Fields

    #endregion

    #region Awake, OnEnable, Start, Update, Etc..

    void OnDisable()
    {
        GameManager_Active.ModdedTeethCount -= 1;
    }

    void OnDestroy()
    {

    }
    private void OnTriggerEnter(Collider other)
    {
        if(GameManager_Active.CurrentGameMode == GameMode.ToothPick)
        {
            if (other.CompareTag(CoreFunct_Abs.ToolNepperTag))
            {
                Powerbar_Active.PowerbarActivateAction?.Invoke();
                CoreFunct_Abs.ChangeGameState(GameState.ST3);
            }
        }
        else if (GameManager_Active.CurrentGameMode == GameMode.ToothBrush)
        {


        }

    }
    #endregion

    #region Methods

    #endregion

    #region Vectors, Gameobjects, Etc..

    #endregion
}
