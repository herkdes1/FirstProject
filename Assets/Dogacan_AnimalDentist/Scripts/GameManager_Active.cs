using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine.SceneManagement;
using System.Linq;
using UnityEngine;
using UnityEngine.UI;
using cakeslice;
using TMPro;
using Base.Game.Signal;
public enum GameMode { ToothPick, ToothBrush, TeethSpray}
public enum GameDifficulty { Easy,Medium,Hard}
public enum GameState { START,ST1,ST2,ST3,ST4,ST5,ST6,ST7,ST8,ST9,EMPTY,END}
public class GameManager_Active : MonoBehaviour
{
    #region Public Fields
    //Gamemanager Actions
    public static Action EndgameActions;
    public static Action PlayerHurtAction;
    public static Action UpdateUIAction;
    //---------------------------------------------------------------------------------------//
    //Game mode, state, diff, etc
    public static GameState CurrentState;
    public static GameDifficulty CurrentGameDiff;
    public static GameMode CurrentGameMode;
    //---------------------------------------------------------------------------------------//
    //Ingame Numbers, Points etc
    public static int ModdedTeethCount;
    public static int ProgressbarValueAdd;
    private static int playerCurrentScore;
    public static int PlayerCurrentScore { get => playerCurrentScore; set {
            SignalBus<SignalCoinChange, int>.Instance.Fire(value - playerCurrentScore);
            playerCurrentScore = value;
        } }
    int _playerMaxScore;
    //---------------------------------------------------------------------------------------//
    //UI Management
    [SerializeField]ParticleSystem EndGameParticle;
    [SerializeField] Slider Progressbar;
    public GameObject StartMenu, EndMenu;
    //---------------------------------------------------------------------------------------//
    //Lists and Arrays
    GameObject[] GeneralTeethArray;
    GameObject[] UsedTeethArray;
    GameObject[] UnusedTeethArray;
    GameObject[] DirtObjectArray;

    List<GameObject> GeneralTeethList;
    List<GameObject> UsedTeethList;
    List<GameObject> ModTeethList;
    //---------------------------------------------------------------------------------------//
    //Game objects, instantiates, Materials
    GameObject[] Tools;
    GameObject Tool_Nepper;
    GameObject Tool_Brush;
    GameObject Tool_Painter;
    GameObject ArrowLoad;

    Material CleanMat;
    Material RottenMat;
    Material DirtyMat;

    Material DropPaintMat;
    Material FlowerPaintMat;
    Material HeartPainthMat;
    Material StarPaintMat;
    Material[] PaintMats;
    //---------------------------------------------------------------------------------------//
    //UI Management
    [SerializeField] TextMeshProUGUI EndGameTitle;
    [SerializeField] TextMeshProUGUI PlayerMoneyIngame;
    [SerializeField] TextMeshProUGUI PlayerMoneyMenu;
    #endregion

    #region Private Fields

    #endregion

    #region Awake, OnEnable, Start, Update, Etc..
    void Awake()
    {
        LoadResourses();
        EndgameActions += EndGameMethod;
        PlayerHurtAction += PlayerLoseMoney;
        UpdateUIAction = UpdateUI;
        EndMenu.SetActive(false);
        StartMenu.SetActive(true);
    }
    void OnDisable()
    {
        EndgameActions = null;
        PlayerHurtAction = null;
        UpdateUIAction = null;
    }

    #endregion

    #region Methods


    #region Setup Methods

    void LoadResourses()
    {
        Tool_Painter = GameObject.FindGameObjectWithTag(CoreFunct_Abs.ToolPainterTag); Tool_Painter.SetActive(false);
        Tool_Nepper = GameObject.FindGameObjectWithTag(CoreFunct_Abs.ToolNepperTag); Tool_Nepper.SetActive(false);
        Tool_Brush = GameObject.FindGameObjectWithTag(CoreFunct_Abs.ToolCleanerTag); Tool_Brush.SetActive(false);
        Tools = new GameObject[] { Tool_Painter, Tool_Nepper, Tool_Brush};
        ArrowLoad = Resources.Load<GameObject>("Prefabs/Clean/3D_Model_Arrow");
        //ArrowLoad = Resources.Load<GameObject>("Originals/IndıcatorArrow 1");
        CleanMat = Resources.Load<Material>("NormalMats/Teeth_Clean_Mat");
        RottenMat = Resources.Load<Material>("NormalMats/Teeth_Rotten_Mat");
        DirtyMat = Resources.Load<Material>("NormalMats/Teeth_Dirty_Mat");
        DropPaintMat = Resources.Load<Material>("NormalMats/PainterMats/Teeth_Drop_Mat");
        FlowerPaintMat = Resources.Load<Material>("NormalMats/PainterMats/Teeth_Flower_Mat");
        HeartPainthMat = Resources.Load<Material>("NormalMats/PainterMats/Teeth_Hearth");
        StarPaintMat = Resources.Load<Material>("NormalMats/PainterMats/Teeth_Star_Mat");
        PaintMats = new Material[] { DropPaintMat, /*FlowerPaintMat*/ HeartPainthMat, StarPaintMat };
    }

    protected List<GameObject> SetAGameobjectList(GameObject[] AnyArray, List<GameObject> AnyList)
    {
        List<GameObject> ModList = new List<GameObject>();
        int ToModİnt = 0;
        if(CurrentGameMode == GameMode.ToothBrush)
        {
            if (CurrentGameDiff == GameDifficulty.Easy) { ToModİnt = UnityEngine.Random.Range(2, 3); } else if (CurrentGameDiff == GameDifficulty.Medium) { ToModİnt = UnityEngine.Random.Range(4, 5); } else if (CurrentGameDiff == GameDifficulty.Hard) { ToModİnt = UnityEngine.Random.Range(6, 7); _playerMaxScore = ToModİnt * 100; }

        }
        else if(CurrentGameMode == GameMode.ToothPick)
        {
            if (CurrentGameDiff == GameDifficulty.Easy) { ToModİnt = 2; } else if (CurrentGameDiff == GameDifficulty.Medium) { ToModİnt = 3; } else if (CurrentGameDiff == GameDifficulty.Hard) { ToModİnt = UnityEngine.Random.Range(4, 5); _playerMaxScore = ToModİnt * 100; }

        }
        else if (CurrentGameMode == GameMode.TeethSpray)
        {
            if (CurrentGameDiff == GameDifficulty.Easy) { ToModİnt = UnityEngine.Random.Range(2, 3); } else if (CurrentGameDiff == GameDifficulty.Medium) { ToModİnt = UnityEngine.Random.Range(4, 5); } else if (CurrentGameDiff == GameDifficulty.Hard) { ToModİnt = UnityEngine.Random.Range(6, 7); _playerMaxScore = ToModİnt * 100; }
        }

        AnyList = AnyArray.ToList();
        while (AnyList.Count > 0 && ModList.Count != ToModİnt)
        {
            int index = UnityEngine.Random.Range(0, AnyList.Count);
            ModList.Add(AnyList[index]);
            AnyList.RemoveAt(index);
        }
        return ModList;
    }

    protected void ModTeethAdjust()
    {
        if(CurrentGameMode == GameMode.ToothPick)
        {
            foreach(GameObject AnyObj in ModTeethList)
            {
                AnyObj.gameObject.tag = CoreFunct_Abs.TeethModTag;
                AnyObj.AddComponent<Teeth_Used_Active>();
                AnyObj.AddComponent<Shake_Teeth>();
                AnyObj.GetComponent<BoxCollider>().enabled = true;
                AnyObj.GetComponent<MeshRenderer>().material = RottenMat;
                Vector3 ArrowSpawnPos = AnyObj.transform.position;
                if (AnyObj.gameObject.transform.position.y > 4f) { ArrowSpawnPos = new Vector3(0, -0.001f, 0); } else { ArrowSpawnPos = new Vector3(0, 0.0006f, 0); }
                GameObject SpawendArrow = Instantiate(ArrowLoad, AnyObj.transform);
                if (AnyObj.gameObject.transform.position.y > 4f) { SpawendArrow.transform.localRotation = Quaternion.Euler(-90, 90, 0);  } else { SpawendArrow.transform.localRotation = Quaternion.Euler(90, 90, 0); }
                SpawendArrow.transform.localPosition = ArrowSpawnPos;
                SpawendArrow.AddComponent<Arrow_MoveAnim_Active>();
            }
        }
        else if (CurrentGameMode == GameMode.ToothBrush)
        {
            foreach (GameObject AnyObj in ModTeethList)
            {
                AnyObj.gameObject.tag = CoreFunct_Abs.TeethModTag;
                AnyObj.GetComponent<BoxCollider>().enabled = true;
                AnyObj.GetComponent<BoxCollider>().size = new Vector3(0.0003f, 0.0012f, 0.002f);
                Material[] dirtyMatArray = { DirtyMat, CleanMat };
                AnyObj.GetComponent<MeshRenderer>().materials = dirtyMatArray;
                Vector3 ArrowSpawnPos = AnyObj.transform.position;
                if (AnyObj.gameObject.transform.position.y > 4f) { ArrowSpawnPos = new Vector3(0, -0.001f, 0); } else { ArrowSpawnPos = new Vector3(0, 0.0006f, 0); }
                GameObject SpawendArrow = Instantiate(ArrowLoad, AnyObj.transform);
                if (AnyObj.gameObject.transform.position.y > 4f) { SpawendArrow.transform.localRotation = Quaternion.Euler(-90, 90, 0); } else { SpawendArrow.transform.localRotation = Quaternion.Euler(90, 90, 0); }
                SpawendArrow.transform.localPosition = ArrowSpawnPos;
                SpawendArrow.AddComponent<Arrow_MoveAnim_Active>();
            }
        }
        else if (CurrentGameMode == GameMode.TeethSpray)
        {
            foreach (GameObject AnyObj in ModTeethList)
            {
                AnyObj.gameObject.tag = CoreFunct_Abs.TeethModTag;
                AnyObj.GetComponent<BoxCollider>().enabled = true;
                AnyObj.AddComponent<Teeth_Used_Active>();
                AnyObj.GetComponent<BoxCollider>().size = new Vector3(0.0003f, 0.0012f, 0.002f);
                //Material[] dirtyMatArray = { PaintMats[UnityEngine.Random.Range(0, PaintMats.Length)], CleanMat };
                //AnyObj.GetComponent<MeshRenderer>().materials = dirtyMatArray;
                AnyObj.GetComponent<MeshRenderer>().material.color = new Color(1, 1, 1, 0);
                Vector3 ArrowSpawnPos = AnyObj.transform.position;
                if (AnyObj.gameObject.transform.position.y > 4f) { ArrowSpawnPos = new Vector3(0, -0.001f, 0); } else { ArrowSpawnPos = new Vector3(0, 0.0006f, 0); }
                GameObject SpawendArrow = Instantiate(ArrowLoad, AnyObj.transform);
                if (AnyObj.gameObject.transform.position.y > 4f) { SpawendArrow.transform.localRotation = Quaternion.Euler(-90, 90, 0); } else { SpawendArrow.transform.localRotation = Quaternion.Euler(90, 90, 0); }
                SpawendArrow.transform.localPosition = ArrowSpawnPos;
                SpawendArrow.AddComponent<Arrow_MoveAnim_Active>();
            }
        }
    }
    #endregion

    #region ButtonActions
    public void ToothpickModeStart()
    {
        CoreFunct_Abs.ChangeGameMode(GameMode.ToothPick);
        CoreFunct_Abs.ChangeGameDiff(GameDifficulty.Hard);
        CoreFunct_Abs.ChangeGameState(GameState.START);
        UsedTeethArray = GameObject.FindGameObjectsWithTag(CoreFunct_Abs.TeetUsedTag);
        foreach (GameObject X in UsedTeethArray) { X.gameObject.GetComponent<BoxCollider>().enabled = false; }
        UnusedTeethArray = GameObject.FindGameObjectsWithTag(CoreFunct_Abs.TeethUnusedTag);
        GeneralTeethArray = UsedTeethArray.Concat(UnusedTeethArray).ToArray();
        DirtObjectArray = GameObject.FindGameObjectsWithTag(CoreFunct_Abs.TeethDirtTag);
        foreach (GameObject X in DirtObjectArray) { X.SetActive(false); }
        UsedTeethList = UsedTeethArray.ToList();
        ModTeethList = SetAGameobjectList(UsedTeethArray, UsedTeethList);
        ModTeethAdjust();
        ModdedTeethCount = ModTeethList.Count;
        Player_Active.PlayerOriginalPos = new Vector3(1.28f, 4, -40);
        Player_Active.PlayerStartAction?.Invoke();
        StartMenu.SetActive(false);
        ActiveTool(Tool_Nepper);
        Player_Active.ToolOriginalPos = Player_Active.SelectedTool.transform.localPosition;
        _playerMaxScore = ModTeethList.Count * 100;
        Progressbar.maxValue = ModTeethList.Count;
        Progressbar.minValue = 0;
        Progressbar.value = Progressbar.minValue;
        ProgressbarValueAdd = 0;
        playerCurrentScore = _playerMaxScore;
        PlayerMoneyIngame.text = PlayerCurrentScore.ToString();
    }
    public void TootBrushModeStart()
    {
        CoreFunct_Abs.ChangeGameMode(GameMode.ToothBrush);
        CoreFunct_Abs.ChangeGameDiff(GameDifficulty.Hard);
        CoreFunct_Abs.ChangeGameState(GameState.START);
        UsedTeethArray = GameObject.FindGameObjectsWithTag(CoreFunct_Abs.TeetUsedTag);
        foreach (GameObject X in UsedTeethArray) { X.gameObject.GetComponent<BoxCollider>().enabled = false; }
        UnusedTeethArray = GameObject.FindGameObjectsWithTag(CoreFunct_Abs.TeethUnusedTag);
        GeneralTeethArray = UsedTeethArray.Concat(UnusedTeethArray).ToArray();
        DirtObjectArray = GameObject.FindGameObjectsWithTag(CoreFunct_Abs.TeethDirtTag);
        foreach (GameObject X in DirtObjectArray) { X.SetActive(false); }
        UsedTeethList = UsedTeethArray.ToList();
        ModTeethList = SetAGameobjectList(UsedTeethArray, UsedTeethList);
        ModTeethAdjust();
        ModdedTeethCount = ModTeethList.Count;
        Player_Active.PlayerOriginalPos = new Vector3(1.28f, 4, -34f);
        Player_Active.PlayerStartAction?.Invoke();
        StartMenu.SetActive(false);
        ActiveTool(Tool_Brush);
        Player_Active.ToolOriginalPos = Player_Active.SelectedTool.transform.localPosition;
        Progressbar.maxValue = ModTeethList.Count;
        Progressbar.minValue = 0;
        Progressbar.value = Progressbar.minValue;
        ProgressbarValueAdd = 0;
        PlayerCurrentScore = 0;
        PlayerMoneyIngame.text = PlayerCurrentScore.ToString();
    }

    public void TeethSprayMode()
    {
        CoreFunct_Abs.ChangeGameMode(GameMode.TeethSpray);
        CoreFunct_Abs.ChangeGameDiff(GameDifficulty.Hard);
        CoreFunct_Abs.ChangeGameState(GameState.START);
        UsedTeethArray = GameObject.FindGameObjectsWithTag(CoreFunct_Abs.TeetUsedTag);
        foreach (GameObject X in UsedTeethArray) { X.gameObject.GetComponent<BoxCollider>().enabled = false; }
        UnusedTeethArray = GameObject.FindGameObjectsWithTag(CoreFunct_Abs.TeethUnusedTag);
        GeneralTeethArray = UsedTeethArray.Concat(UnusedTeethArray).ToArray();
        DirtObjectArray = GameObject.FindGameObjectsWithTag(CoreFunct_Abs.TeethDirtTag);
        foreach (GameObject X in DirtObjectArray) { X.SetActive(false); }
        UsedTeethList = UsedTeethArray.ToList();
        ModTeethList = SetAGameobjectList(UsedTeethArray, UsedTeethList);
        ModTeethAdjust();
        ModdedTeethCount = ModTeethList.Count;
        Player_Active.PlayerOriginalPos = new Vector3(1.28f, 4, -34f);
        Player_Active.PlayerStartAction?.Invoke();
        StartMenu.SetActive(false);
        ActiveTool(Tool_Painter);
        Player_Active.ToolOriginalPos = Player_Active.SelectedTool.transform.localPosition;
        Player_Active.ToolOriginalRotation = Player_Active.SelectedTool.transform.localRotation;
        _playerMaxScore = ModTeethList.Count * 100;
        Progressbar.maxValue = ModTeethList.Count;
        Progressbar.minValue = 0;
        ProgressbarValueAdd = 0;
        Progressbar.value = Progressbar.minValue;
        playerCurrentScore = _playerMaxScore;
        PlayerMoneyIngame.text = PlayerCurrentScore.ToString();
    }
    public void UpdateUI()
    {
        PlayerMoneyIngame.text = PlayerCurrentScore.ToString();
        Progressbar.value = ProgressbarValueAdd;
    }

    
    public void PlayerLoseMoney()
    {
        int MistakeValue = Mathf.RoundToInt(Powerbar_Active.CurrentValue);
        PlayerCurrentScore -= MistakeValue;
        UpdateUIAction?.Invoke();
    }
    void EndGameMethod()
    {
        StartCoroutine(EndGameRoutine());
    }
    IEnumerator EndGameRoutine()
    {
        PlayerMoneyIngame.text = PlayerCurrentScore.ToString();
        EndGameParticle.Play();
        yield return new WaitForSeconds(1.2f);
        EndMenu.SetActive(true);
        PlayerMoneyMenu.text = PlayerCurrentScore.ToString();
        SignalBus<SignalNextStage>.Instance.Fire();
    }

    public void GoToNextScene()
    {
        SceneManager.LoadScene(SceneManager.GetActiveScene().buildIndex + 1);
    }

    public void ResetCurrentScene()
    {
        SceneManager.LoadScene(SceneManager.GetActiveScene().name);
    }


    //Usefull few things

    void ActiveTool(GameObject ActiveObject)
    {
        foreach(GameObject AnyTool in Tools)
        {
            AnyTool.SetActive(false);
        }
        ActiveObject.SetActive(true);
        Player_Active.SelectedTool = ActiveObject;
    }

    void GeneralLoadFuncts()
    {
        UsedTeethArray = GameObject.FindGameObjectsWithTag(CoreFunct_Abs.TeetUsedTag);
        foreach (GameObject X in UsedTeethArray) { X.gameObject.GetComponent<BoxCollider>().enabled = false; }
        UnusedTeethArray = GameObject.FindGameObjectsWithTag(CoreFunct_Abs.TeethUnusedTag);
        GeneralTeethArray = UsedTeethArray.Concat(UnusedTeethArray).ToArray();
        DirtObjectArray = GameObject.FindGameObjectsWithTag(CoreFunct_Abs.TeethDirtTag);
        foreach (GameObject X in DirtObjectArray) { X.SetActive(false); }
        UsedTeethList = UsedTeethArray.ToList();
        ModTeethList = SetAGameobjectList(UsedTeethArray, UsedTeethList);
        ModTeethAdjust();
        ModdedTeethCount = ModTeethList.Count;
    }

    #endregion
    #endregion

    #region Vectors, Gameobjects, Etc..

    #endregion
}
