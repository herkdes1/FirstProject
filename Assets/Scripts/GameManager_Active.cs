using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine.SceneManagement;
using System.Linq;
using UnityEngine;
using UnityEngine.UI;
using cakeslice;
using TMPro;
using System;
using Base.Game.Signal;
public enum GameMode { ToothPick, ToothBrush}
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
    private static int playerCurrentScore;
    public static int PlayerCurrentScore { get => playerCurrentScore; set {
            SignalBus<SignalCoinChange, int>.Instance.Fire(value - playerCurrentScore);
            playerCurrentScore = value;
        } }
    int _playerMaxScore;
    //---------------------------------------------------------------------------------------//
    //UI Management
    [SerializeField]ParticleSystem EndGameParticle;
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
    GameObject Tool_Nepper;
    GameObject Tool_Brush;
    GameObject ArrowLoad;
    Material CleanMat;
    Material RottenMat;
    Material DirtyMat;
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
        PlayerHurtAction += PlayerLoseHealth;
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
        Tool_Nepper = GameObject.FindGameObjectWithTag(CoreFunct_Abs.ToolNepperTag); Tool_Nepper.SetActive(false);
        Tool_Brush = GameObject.FindGameObjectWithTag(CoreFunct_Abs.ToolCleanerTag); Tool_Brush.SetActive(false);
        ArrowLoad = Resources.Load<GameObject>("Prefabs/Clean/3D_Model_Arrow");
        CleanMat = Resources.Load<Material>("NormalMats/Teeth_Clean_Mat");
        RottenMat = Resources.Load<Material>("NormalMats/Teeth_Rotten_Mat");
        DirtyMat = Resources.Load<Material>("NormalMats/Teeth_Dirty_Mat");
    }

    protected List<GameObject> SetAGameobjectList(GameObject[] AnyArray, List<GameObject> AnyList)
    {
        List<GameObject> ModList = new List<GameObject>();
        int ToModİnt = 0;
        if (CurrentGameDiff == GameDifficulty.Easy) { ToModİnt = 2; _playerMaxScore = 200; } else if (CurrentGameDiff == GameDifficulty.Medium) { ToModİnt = 3; _playerMaxScore = 300; } else if (CurrentGameDiff == GameDifficulty.Hard) { ToModİnt = 4; _playerMaxScore = 400; }
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
    }
    #endregion

    #region ButtonActions
    public void ToothpickModeStart()
    {
        CoreFunct_Abs.ChangeGameMode(GameMode.ToothPick);
        CoreFunct_Abs.ChangeGameDiff(GameDifficulty.Hard);
        CoreFunct_Abs.ChangeGameState(GameState.START);
        UsedTeethArray = GameObject.FindGameObjectsWithTag(CoreFunct_Abs.TeetUsedTag);
        foreach(GameObject X in UsedTeethArray) { X.gameObject.GetComponent<BoxCollider>().enabled = false; }
        UnusedTeethArray = GameObject.FindGameObjectsWithTag(CoreFunct_Abs.TeethUnusedTag);
        GeneralTeethArray = UsedTeethArray.Concat(UnusedTeethArray).ToArray();
        DirtObjectArray = GameObject.FindGameObjectsWithTag(CoreFunct_Abs.TeethDirtTag);
        foreach(GameObject X in DirtObjectArray) { X.SetActive(false); }
        UsedTeethList = UsedTeethArray.ToList();
        ModTeethList = SetAGameobjectList(UsedTeethArray, UsedTeethList);
        ModTeethAdjust();
        ModdedTeethCount = ModTeethList.Count;
        Player_Active.PlayerOriginalPos = new Vector3(1.28f, 4, -40);
        Player_Active.PlayerStartAction?.Invoke();
        StartMenu.SetActive(false);
        Player_Active.SelectedTool = Tool_Nepper;
        Tool_Nepper.SetActive(true);
        Player_Active.ToolOriginalPos = Player_Active.SelectedTool.transform.localPosition;
        PlayerCurrentScore = _playerMaxScore;
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
        Player_Active.SelectedTool = Tool_Brush;
        Tool_Brush.SetActive(true);
        Player_Active.ToolOriginalPos = Player_Active.SelectedTool.transform.localPosition;
        PlayerCurrentScore = 0;
        PlayerMoneyIngame.text = PlayerCurrentScore.ToString();
    }

    public void UpdateUI()
    {
        PlayerMoneyIngame.text = PlayerCurrentScore.ToString();
    }
    public void PlayerLoseHealth()
    {
        PlayerCurrentScore -= 20;
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


    #endregion
    #endregion

    #region Vectors, Gameobjects, Etc..

    #endregion
}
