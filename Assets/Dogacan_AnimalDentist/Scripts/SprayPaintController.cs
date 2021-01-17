using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using System;
public class SprayPaintController : MonoBehaviour
{
    Player_Active Player;
    public static Action ActivatePainterMenuAction;
    public static Action DeactivatePainterMenuAction;
    public GameObject PainterMenu;

    Material CleanMat;
    Material RottenMat;
    Material DirtyMat;


    //Paint Options
    Material DropPaintMat;
    Material FlowerPaintMat;
    Material HeartPainthMat;
    Material StarPaintMat;

    void Awake()
    {
        Player = FindObjectOfType<Player_Active>();
        CleanMat = Resources.Load<Material>("NormalMats/Teeth_Clean_Mat");
        RottenMat = Resources.Load<Material>("NormalMats/Teeth_Rotten_Mat");
        DirtyMat = Resources.Load<Material>("NormalMats/Teeth_Dirty_Mat");
        //Paint Mats
        DropPaintMat = Resources.Load<Material>("NormalMats/PainterMats/Teeth_Drop_Mat");
        FlowerPaintMat = Resources.Load<Material>("NormalMats/PainterMats/Teeth_Flower_Mat");
        HeartPainthMat = Resources.Load<Material>("NormalMats/PainterMats/Teeth_Hearth");
        StarPaintMat = Resources.Load<Material>("NormalMats/PainterMats/Teeth_Star_Mat");
        ActivatePainterMenuAction += ActivatePainterMenu;
        DeactivatePainterMenuAction += DeactivatePainterMenu;
        DeactivatePainterMenu();
    }

    void OnDisable()
    {
        ActivatePainterMenuAction = null;
        DeactivatePainterMenuAction = null;
    }

    void ActivatePainterMenu()
    {
        PainterMenu.SetActive(true);
    }
    void DeactivatePainterMenu()
    {
        PainterMenu.SetActive(false);
    }
    public void _DropPaintButton()
    {
        Player_Active.SelectedObject.GetComponent<MeshRenderer>().materials = AnyMaterialArray(DropPaintMat);
        Player_Active.SelectedObject.GetComponent<MeshRenderer>().material.color = new Color(1, 1, 1, 0);
        Player.SprayAnimState("SprayShake");
        CoreFunct_Abs.ChangeGameState(GameState.ST3);
        DeactivatePainterMenu();
    }
    public void _FlowerPaintButton()
    {
        Player_Active.SelectedObject.GetComponent<MeshRenderer>().materials = AnyMaterialArray(FlowerPaintMat);
        Player_Active.SelectedObject.GetComponent<MeshRenderer>().material.color = new Color(1, 1, 1, 0);
        Player.SprayAnimState("SprayShake");
        CoreFunct_Abs.ChangeGameState(GameState.ST3);
        DeactivatePainterMenu();
    }
    public void _HeartPaintButton()
    {
        Player_Active.SelectedObject.GetComponent<MeshRenderer>().materials = AnyMaterialArray(HeartPainthMat);
        Player_Active.SelectedObject.GetComponent<MeshRenderer>().material.color = new Color(1, 1, 1, 0);
        Player.SprayAnimState("SprayShake");
        CoreFunct_Abs.ChangeGameState(GameState.ST3);
        DeactivatePainterMenu();
    }
    public void _StarPaintButton()
    {
        Player_Active.SelectedObject.GetComponent<MeshRenderer>().materials = AnyMaterialArray(StarPaintMat);
        Player_Active.SelectedObject.GetComponent<MeshRenderer>().material.color = new Color(1, 1, 1, 0);
        Player.SprayAnimState("SprayShake");
        CoreFunct_Abs.ChangeGameState(GameState.ST3);
        DeactivatePainterMenu();
    }

    Material[] AnyMaterialArray(Material anyMat)
    {
        Material[] X = { anyMat, CleanMat };
        return X;
    }
}
