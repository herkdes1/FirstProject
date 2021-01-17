using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using System;
using cakeslice;
using Base.Game.Signal;

public class Player_Active : MonoBehaviour
{
    #region Actions 
    public static Action PlayerStartAction;
    public static Action PlayerGeneralAction;
    public static Action PlayerFunctChange;
    #endregion
    //---------------------------------------------------------------------------------------//
    #region Public Fields
    public static Vector3 PlayerOriginalPos;
    public static Vector3 ToolOriginalPos;
    public static Quaternion ToolOriginalRotation;
    public static GameObject SelectedTool, SelectedObject;
    //---------------------------------------------------------------------------------------//
    //Animation, Animator and particle Controls
    [SerializeField] ParticleSystem SprayPaintParticle;
    [SerializeField] ParticleSystem BloodPopParticle;
    [SerializeField] Animator ToothPickAnimator;
    [SerializeField] Animator SprayAnimator;
    string ToothPickAnimStateString;
    string SprayAnimStateString;

    const string NeppersIdleAnim = "NepperIdleAnim";
    const string NeppersOpenIdleAnim = "NepperOpenIdleAnim";
    const string NeppersCloseAnim = "NepperCloseActionAnim";

    const string SprayShakeAnim = "SprayShake";
    const string SprayIdleAnim = "SprayIdle";
    const string SprayUpDownShakeAnim = "SprayUpDownShake";

    #endregion
    //---------------------------------------------------------------------------------------//
    #region Private Fields
    Color selectedObjectColor;
    Color sprayColor;
    #endregion
    //---------------------------------------------------------------------------------------//
    #region Awake, OnEnable, Start, Update, Etc..
    void Awake()
    {
        PlayerStartAction += PlayerStartZoom;
        PlayerFunctChange += ChecktState;
        ////StartCoroutine(StartZoom());
        //PlayerStartAction?.Invoke();
    }

    void Update()
    {
        if (Input.touchCount == 1)
            PlayerGeneralAction?.Invoke();
    }

    void OnDisable()
    {
        PlayerStartAction = null;
        PlayerGeneralAction = null;
        PlayerFunctChange = null;
    }

    void OnDestroy()
    {

    }
    #endregion
    //---------------------------------------------------------------------------------------//
    #region Methods
    //State Functions
    public void ChecktState()
    {
        StartCoroutine(FunctionChanger());
    }

    IEnumerator FunctionChanger()
    {
        PlayerGeneralAction = null;
        if (GameManager_Active.CurrentGameMode == GameMode.ToothPick)
        {
            if (GameManager_Active.CurrentState == GameState.ST1)
            {
                ToothPickAnimState(NeppersIdleAnim);
                PlayerGeneralAction += OnTapCheck;
            }
            else if (GameManager_Active.CurrentState == GameState.ST2)
            {
                ToothPickAnimState(NeppersOpenIdleAnim);
                PlayerGeneralAction += MoveToolNeppers;
            }
            else if (GameManager_Active.CurrentState == GameState.ST3)
            {
                Vector3 pos1 = SelectedObject.transform.position; pos1.z -= 0.085f;
                StartCoroutine(AdjustToothpickTool(pos1));
            }
            else if (GameManager_Active.CurrentState == GameState.ST4)
            {

            }
            else if (GameManager_Active.CurrentState == GameState.ST5)
            {

            }
            else if (GameManager_Active.CurrentState == GameState.ST6)
            {

            }
            else if (GameManager_Active.CurrentState == GameState.ST7)
            {

            }
            else if (GameManager_Active.CurrentState == GameState.ST8)
            {

            }
            else if (GameManager_Active.CurrentState == GameState.ST9)
            {

            }
            else if (GameManager_Active.CurrentState == GameState.EMPTY)
            {
                PlayerGeneralAction = null;
            }
        }
        else if (GameManager_Active.CurrentGameMode == GameMode.ToothBrush)
        {
            if (GameManager_Active.CurrentState == GameState.ST1)
            {
                PlayerGeneralAction += MoveBrush;
            }
            else if (GameManager_Active.CurrentState == GameState.ST2)
            {

            }
            else if (GameManager_Active.CurrentState == GameState.ST3)
            {

            }
            else if (GameManager_Active.CurrentState == GameState.ST4)
            {

            }
            else if (GameManager_Active.CurrentState == GameState.ST5)
            {

            }
            else if (GameManager_Active.CurrentState == GameState.ST6)
            {

            }
            else if (GameManager_Active.CurrentState == GameState.ST7)
            {

            }
            else if (GameManager_Active.CurrentState == GameState.ST8)
            {

            }
            else if (GameManager_Active.CurrentState == GameState.ST9)
            {

            }
            else if (GameManager_Active.CurrentState == GameState.EMPTY)
            {
                PlayerGeneralAction = null;
            }
        }
        else if (GameManager_Active.CurrentGameMode == GameMode.TeethSpray)
        {
            if (GameManager_Active.CurrentState == GameState.ST1)
            {
                SprayAnimState(SprayUpDownShakeAnim);
                yield return new WaitForSeconds(.75f);
                PlayerGeneralAction += OnTapCheck;
            }
            else if (GameManager_Active.CurrentState == GameState.ST2)
            {
                SprayPaintController.ActivatePainterMenuAction?.Invoke();
                CoreFunct_Abs.ChangeGameState(GameState.EMPTY);
            }
            else if (GameManager_Active.CurrentState == GameState.ST3)
            {
                PlayerGeneralAction += MoveToolSpray;
            }
            else if (GameManager_Active.CurrentState == GameState.ST4)
            {
                Powerbar_Active.PowerbarActivateAction?.Invoke();
                selectedObjectColor = SelectedObject.GetComponent<MeshRenderer>().material.color;
                Vector3 pos1 = SelectedObject.transform.position; pos1.z -= 0.5f;
                StartCoroutine(AdjustPainter(pos1));
                PlayerGeneralAction += SprayControl;
            }
            else if (GameManager_Active.CurrentState == GameState.ST5)
            {

            }
            else if (GameManager_Active.CurrentState == GameState.ST6)
            {

            }
            else if (GameManager_Active.CurrentState == GameState.ST7)
            {

            }
            else if (GameManager_Active.CurrentState == GameState.ST8)
            {

            }
            else if (GameManager_Active.CurrentState == GameState.ST9)
            {

            }
            else if (GameManager_Active.CurrentState == GameState.EMPTY)
            {
                PlayerGeneralAction = null;
            }
        }
        yield return null;
    }

    //---------------------------------------------------------------------------------------//
    //Zoom Actions 

    void PlayerStartZoom()
    {
        StartCoroutine(StartZoom());
    }

    IEnumerator StartZoom()
    {
        while (this.transform.position != PlayerOriginalPos)
        {
            yield return new WaitForFixedUpdate();
            this.gameObject.transform.position = Vector3.MoveTowards(this.gameObject.transform.position, PlayerOriginalPos, .5f);
        }
        StopCoroutine(StartZoom());
        CoreFunct_Abs.ChangeGameState(GameState.ST1);
    }

    IEnumerator ZoomIn()
    {
        Vector3 gameobjectVec = SelectedObject.transform.position;
        gameobjectVec.z -= 2f;
        while (this.transform.position != gameobjectVec)
        {
            yield return new WaitForFixedUpdate();
            this.gameObject.transform.position = Vector3.MoveTowards(this.gameObject.transform.position, gameobjectVec, .15f);
        }
        CoreFunct_Abs.ChangeGameState(GameState.ST2);
        StopCoroutine(ZoomIn());

    }
    IEnumerator ZoomOut()
    {
        while (this.transform.position != PlayerOriginalPos)
        {
            yield return new WaitForFixedUpdate();
            this.gameObject.transform.position = Vector3.MoveTowards(this.gameObject.transform.position, PlayerOriginalPos, .15f);
        }
        if (GameManager_Active.ModdedTeethCount != 0)
        {
            SelectedTool.transform.localScale = new Vector3(0.1f, 0.1f, 0.1f);
            SelectedTool.transform.localRotation = ToolOriginalRotation;
            SelectedTool.transform.localPosition = ToolOriginalPos;
            StopCoroutine(ZoomOut());
            CoreFunct_Abs.ChangeGameState(GameState.ST1);
        }
        else
        {
            GameManager_Active.EndgameActions?.Invoke();
        }
    }

    //Gameplay Functions

    void OnTapCheck()
    {
        if (Input.touchCount == 1 && Input.GetTouch(0).phase == TouchPhase.Began)
        {
            var ray = Camera.main.ScreenPointToRay(Input.GetTouch(0).position); RaycastHit hit;
            bool didithit = Physics.Raycast(ray.origin, ray.direction, out hit, Mathf.Infinity);
            if (didithit)
            {
                if (hit.transform.CompareTag(CoreFunct_Abs.TeethModTag))
                {
                    SelectedObject = hit.transform.gameObject;
                    SelectedObject.GetComponent<BoxCollider>().size = new Vector3(.0001f, .0001f, .01f);
                    SelectedObject.GetComponent<BoxCollider>().center = new Vector3(0, 0, 0);
                    SelectedObject.GetComponent<BoxCollider>().isTrigger = true;
                    GameObject child = SelectedObject.transform.GetChild(0).gameObject;
                    child.gameObject.GetComponent<MeshRenderer>().materials[0].color = Color.green; child.gameObject.GetComponent<MeshRenderer>().materials[1].color = Color.green;
                    StartCoroutine(ZoomIn());
                    CoreFunct_Abs.ChangeGameState(GameState.EMPTY);
                }
            }
            
        }
    }

    void MoveToolNeppers()
    {
        if (Input.touchCount == 1)
        {
            if (Input.GetTouch(0).phase == TouchPhase.Moved && SelectedObject != null)
            {
                Vector3 TouchWorldPos;
                if (SelectedObject.transform.position.y < 4)
                { SelectedTool.transform.rotation = Quaternion.Euler(SelectedTool.transform.rotation.x, SelectedTool.transform.rotation.y, SelectedTool.transform.rotation.z - 180f);  TouchWorldPos = GetWorldPositionOnPlane(Input.GetTouch(0).position, SelectedObject.transform.position.z - 1); /*SelectedTool.transform.position = new Vector3(TouchWorldPos.x + 4f, TouchWorldPos.y - 6f, TouchWorldPos.z);*/ }
                else { SelectedTool.transform.rotation = Quaternion.Euler(0, 0, 0);  TouchWorldPos = GetWorldPositionOnPlane(Input.GetTouch(0).position, SelectedObject.transform.position.z - 1); /*SelectedTool.transform.position = new Vector3(TouchWorldPos.x + 5f, TouchWorldPos.y + 7f, TouchWorldPos.z);*/ }
                SelectedTool.transform.localScale = new Vector3(.5f, .5f, 0.5f);
                SelectedTool.transform.position = TouchWorldPos;
            }
            else if (Input.GetTouch(0).phase == TouchPhase.Canceled && SelectedObject != null || Input.GetTouch(0).phase == TouchPhase.Ended && SelectedObject != null)
            {
                SelectedTool.transform.localScale = new Vector3(.1f, .1f, .1f);
                SelectedTool.transform.rotation = Quaternion.Euler(0, 0, 0);
                SelectedTool.transform.localPosition = ToolOriginalPos;
            }
        }
    }
    void MoveBrush()
    {
        if (Input.touchCount == 1)
        {
            if (Input.GetTouch(0).phase == TouchPhase.Moved)
            {
                Vector3 TouchPos = Camera.main.ScreenToViewportPoint(Input.GetTouch(0).position);
                Vector3 TouchWorldPos = GetWorldPositionOnPlane(Input.GetTouch(0).position, -30.5f);
                SelectedTool.transform.rotation = Quaternion.Euler(-90, 180, 0);
                //if(TouchPos.x > 0.7f) { SelectedTool.transform.rotation = Quaternion.Euler(-45, 90, 90); } else if(TouchPos.x < 0.7f && TouchPos.x > 0.3f) { SelectedTool.transform.rotation = Quaternion.Euler(270, 90, 90); } else if (TouchPos.x < 0.3f) { SelectedTool.transform.rotation = Quaternion.Euler(-110, 90, 90); }
                SelectedTool.transform.position = new Vector3(TouchWorldPos.x, TouchWorldPos.y, TouchWorldPos.z);
                SelectedTool.transform.localScale = new Vector3(.5f, .5f, .5f);
                Vector3 ToolCamMove = new Vector3(SelectedTool.transform.position.x, SelectedTool.transform.position.y, SelectedTool.transform.position.z - 2.5f);
                float MoveX = ToolCamMove.x; float MoveY = ToolCamMove.y; float MoveZ = ToolCamMove.z;
                MoveY = Mathf.Clamp(MoveY, 4, 4.3f); MoveX = Mathf.Clamp(MoveX, 1f, 1.4f);
                Vector3 Test1 = new Vector3(MoveX, MoveY, MoveZ);
                this.gameObject.transform.position = Vector3.MoveTowards(this.gameObject.transform.position, Test1, 0.01f);
            }
            else if (Input.GetTouch(0).phase == TouchPhase.Canceled || Input.GetTouch(0).phase == TouchPhase.Ended)
            {
                this.gameObject.transform.position = PlayerOriginalPos;
                SelectedTool.transform.localScale = new Vector3(.1f, .1f, .1f);
                SelectedTool.transform.rotation = Quaternion.Euler(-70, -50, 0);
                SelectedTool.transform.localPosition = ToolOriginalPos;
            }
        }
    }
    void MoveToolSpray()
    {
        if (Input.touchCount == 1)
        {
            if (Input.GetTouch(0).phase == TouchPhase.Moved && SelectedObject != null)
            {
                Vector3 TouchWorldPos;
                //if (SelectedObject.transform.position.y < 4)
                //{ SelectedTool.transform.rotation = Quaternion.Euler(ToolOriginalRotation.x, ToolOriginalRotation.y, ToolOriginalRotation.z - 180); TouchWorldPos = GetWorldPositionOnPlane(Input.GetTouch(0).position, SelectedObject.transform.position.z - 1); /*SelectedTool.transform.position = new Vector3(TouchWorldPos.x + 4f, TouchWorldPos.y - 6f, TouchWorldPos.z);*/ }
                //else { SelectedTool.transform.rotation = ToolOriginalRotation; TouchWorldPos = GetWorldPositionOnPlane(Input.GetTouch(0).position, SelectedObject.transform.position.z - 1); /*SelectedTool.transform.position = new Vector3(TouchWorldPos.x + 5f, TouchWorldPos.y + 7f, TouchWorldPos.z);*/ }
                TouchWorldPos = GetWorldPositionOnPlane(Input.GetTouch(0).position, SelectedObject.transform.position.z - 1);
                SelectedTool.transform.localScale = new Vector3(.5f, .5f, 0.5f);
                SelectedTool.transform.position = TouchWorldPos;
            }
            else if (Input.GetTouch(0).phase == TouchPhase.Canceled && SelectedObject != null || Input.GetTouch(0).phase == TouchPhase.Ended && SelectedObject != null)
            {
                SelectedTool.transform.localScale = new Vector3(.1f, .1f, .1f);
                SelectedTool.transform.localRotation = ToolOriginalRotation;
                SelectedTool.transform.localPosition = ToolOriginalPos;
            }
        }
    }
    void SprayControl()
    {
        if(Input.touchCount == 1)
        {

            if(Input.GetTouch(0).phase == TouchPhase.Stationary)
            {
                Powerbar_Active.Counting = true;
                SprayPaintParticle.transform.LookAt(SelectedObject.transform.position);
                SprayPaintParticle.Play();
                selectedObjectColor.a = Powerbar_Active.CurrentValue / 100;
                SelectedObject.GetComponent<MeshRenderer>().material.color = selectedObjectColor;
            }
            else
            {
                if (Input.GetTouch(0).phase == TouchPhase.Canceled || Input.GetTouch(0).phase == TouchPhase.Ended)
                {
                    SprayPaintParticle.Stop();
                    Powerbar_Active.Counting = false;
                    if (Powerbar_Active.CurrentValue < 101 && Powerbar_Active.CurrentValue > 80)
                    {
                        CoreFunct_Abs.ChangeGameState(GameState.EMPTY);
                        GameManager_Active.PlayerCurrentScore += Mathf.RoundToInt(Powerbar_Active.CurrentValue);
                        SelectedObject.transform.GetChild(0).gameObject.SetActive(false);
                        GameManager_Active.ProgressbarValueAdd += 1;
                        GameManager_Active.ModdedTeethCount -= 1;
                        GameManager_Active.UpdateUIAction?.Invoke();
                        SelectedObject.GetComponent<BoxCollider>().enabled = false;
                        HapticAction.Vibrate(100);
                        Powerbar_Active.PowerbarDeactivateAction?.Invoke();
                        StartCoroutine(ZoomOut());
                    }
                    else
                    {
                        GameManager_Active.PlayerHurtAction?.Invoke();
                        Powerbar_Active.PowerbarValueResetAction?.Invoke();
                        selectedObjectColor.a = Powerbar_Active.CurrentValue / 100;
                        SelectedObject.GetComponent<MeshRenderer>().material.color = selectedObjectColor;
                        HapticAction.Vibrate(100);
                    }

                }
            }


        }

    }
    void OnConnection()
    {
        if (Input.touchCount == 1 && Input.GetTouch(0).phase == TouchPhase.Began)
        {
            if (Powerbar_Active.CurrentValue < 70 && Powerbar_Active.CurrentValue > 30)
            {
                CoreFunct_Abs.ChangeGameState(GameState.EMPTY);

                GameManager_Active.PlayerCurrentScore += Mathf.RoundToInt(Powerbar_Active.CurrentValue) * 2;
                GameManager_Active.UpdateUIAction?.Invoke();
                SelectedObject.GetComponent<BoxCollider>().isTrigger = false;
                HapticAction.Vibrate(100);
                Vector3 MoveAnim1 = SelectedObject.transform.position; if (MoveAnim1.y < 4) { MoveAnim1.y += .2f; } else { MoveAnim1.y -= .2f; }
                Vector3 MoveAnim2 = MoveAnim1; MoveAnim2.z -= .4f;
                BloodPopParticle.Play();
                StartCoroutine(MoveAnim(SelectedObject, MoveAnim1, MoveAnim2));
                Powerbar_Active.PowerbarDeactivateAction?.Invoke();
            }
            else
            {
                SelectedObject.GetComponent<Shake_Teeth>().Shake();
                SelectedTool.GetComponentInChildren<Shake_Teeth>();
                GameManager_Active.PlayerHurtAction?.Invoke();
                BloodPopParticle.Play();
                Powerbar_Active.PowerbarValueResetAction?.Invoke();
                HapticAction.Vibrate(100);
            }
        }
    }

    //Code Based Animation

    IEnumerator AdjustToothpickTool(Vector3 selectedObjectPos)
    {
        ToothPickAnimState(NeppersCloseAnim);
        Vector3 pos2 = selectedObjectPos;
        if (selectedObjectPos.y < 4) { selectedObjectPos.y += .1f; } else { selectedObjectPos.y -= .1f; }
        while (SelectedTool.transform.position != selectedObjectPos)
        {
            yield return new WaitForFixedUpdate();
            SelectedTool.gameObject.transform.position = Vector3.MoveTowards(SelectedTool.gameObject.transform.position, selectedObjectPos, .1f);
        }
        PlayerGeneralAction += OnConnection;
        StopCoroutine(AdjustToothpickTool(selectedObjectPos));
    }
    IEnumerator AdjustPainter(Vector3 selectedObjectPos)
    {
        ToothPickAnimState(NeppersCloseAnim);
        Vector3 pos2 = selectedObjectPos;
        //if (selectedObjectPos.y < 4) { selectedObjectPos.y += .1f; } else { selectedObjectPos.y -= .1f; }
        selectedObjectPos.y -= .1f;
        while (SelectedTool.transform.position != selectedObjectPos)
        {
            yield return new WaitForFixedUpdate();
            SelectedTool.gameObject.transform.position = Vector3.MoveTowards(SelectedTool.gameObject.transform.position, selectedObjectPos, .1f);
        }
        StopCoroutine(AdjustToothpickTool(selectedObjectPos));
    }
    protected IEnumerator MoveAnim(GameObject any, Vector3 MovePos1, Vector3 MovePos2)
    {
        Vector3 ToolPos = MovePos1; if (MovePos1.y < 4) { ToolPos.y += 2; } else { ToolPos.y -= 2; }
        while (any.transform.position != MovePos1)
        {
            yield return new WaitForFixedUpdate();
            any.transform.position = Vector3.MoveTowards(any.transform.position, MovePos1, .6f * Time.deltaTime);
            SelectedTool.transform.position = Vector3.MoveTowards(SelectedTool.transform.position, ToolPos, .6f * Time.deltaTime);
        }
        StopCoroutine(MoveAnim(any, MovePos1, MovePos2));
        StartCoroutine(MoveAnim2(any, MovePos2));
    }

    protected IEnumerator MoveAnim2(GameObject Any, Vector3 MovePos)
    {
        Vector3 ToolPos = MovePos;
        ToolPos.z -= 3;
        while (Any.transform.position != MovePos)
        {
            yield return new WaitForFixedUpdate();
            Any.transform.position = Vector3.MoveTowards(Any.transform.position, MovePos, .6f * Time.deltaTime);
            SelectedTool.transform.position = Vector3.MoveTowards(SelectedTool.transform.position, ToolPos, .6f * Time.deltaTime);
        }
        Any.transform.gameObject.SetActive(false);
        StopCoroutine(MoveAnim2(Any, MovePos));
        StartCoroutine(ZoomOut());
    }

    public void ToothPickAnimState(string newState)
    {
        if (ToothPickAnimStateString == newState) return;
        ToothPickAnimator.Play(newState);
        ToothPickAnimStateString = newState;
    }
    public void SprayAnimState(string newState)
    {
        if (SprayAnimStateString == newState) return;
        SprayAnimator.Play(newState);
        SprayAnimStateString = newState;
    }
    #endregion

    #region Vectors, Gameobjects, Etc..
    protected Vector3 GetWorldPositionOnPlane(Vector3 screenPosition, float z)
    {
        Ray ray = Camera.main.ScreenPointToRay(screenPosition);
        Plane xy = new Plane(Vector3.forward, new Vector3(0, 0, z));
        float distance;
        xy.Raycast(ray, out distance);
        return ray.GetPoint(distance);
    }
    #endregion
}
//Kullanışsız Kod
//if(GameManager_Active.CurrentGameMode == GameMode.TeethSpray)
//{
//    print(SelectedObject.GetComponent<MeshRenderer>().material.name);
//    if (SelectedObject.GetComponent<MeshRenderer>().material.name == "Teeth_Drop_Mat (Instance)") { sprayColor = new Color(0, 0, 1, .5f); }
//    else if (SelectedObject.GetComponent<MeshRenderer>().material.name == "Teeth_Hearth (Instance)") { sprayColor = new Color(1, 0, 0, .5f); } 
//    else if (SelectedObject.GetComponent<MeshRenderer>().material.name == "Teeth_Star_Mat (Instance)") { sprayColor = new Color(1, 1, 0, .5f); }
//    SprayPaintParticle.startColor = sprayColor;
//}