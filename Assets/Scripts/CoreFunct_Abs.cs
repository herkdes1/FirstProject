using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using System.Linq;
public abstract class CoreFunct_Abs : MonoBehaviour
{
    #region Constants
    public const string TeethUnusedTag = "Teeth_Unused";
    public const string TeetUsedTag = "Teeth_used";
    public const string TeethDirtTag = "Teeth_Dirt";
    public const string TeethModTag = "Teeth_Mod";
    public const string ToolCleanerTag = "Tool_Cleaner";
    public const string ToolNepperTag = "Tool_Nepper";

    #endregion

    #region Static Methods
    public static void ChangeGameMode(GameMode AnyMode)
    {
        GameManager_Active.CurrentGameMode = AnyMode;
    }

    public static void ChangeGameDiff(GameDifficulty AnyDiff)
    {
        GameManager_Active.CurrentGameDiff = AnyDiff;
    }

    public static void ChangeGameState(GameState AnyState)
    {
        GameManager_Active.CurrentState = AnyState;
        Player_Active.PlayerFunctChange?.Invoke();
    }


    public static GameObject GrandParent(GameObject AnyObject)
    {
        return AnyObject.gameObject.transform.parent.parent.gameObject;
    }



    #endregion
}
