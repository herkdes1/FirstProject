using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using System;
using UnityEngine.UI;
public class Powerbar_Active : MonoBehaviour
{
    public static Action PowerbarDeactivateAction;
    public static Action PowerbarActivateAction;
    public static Action PowerbarValueResetAction;

    [SerializeField] GameObject Visuals;
    [SerializeField] RectTransform ButtonPivot;
    [SerializeField] GameObject DownArrowPivot;
    [SerializeField] Image DownArrowImage;
    [SerializeField] Image Smile;

    public static float CurrentValue;
    public static bool Counting;
    public static bool Up;

    private bool Rewind;
    private int PowerbarSpeed;


    private void Start()
    {
        CurrentValue = 0;
        PowerbarSpeed = 80;
        PowerbarActivateAction += ActivatePowerbar;
        PowerbarDeactivateAction += ResetPowerbar;
        PowerbarValueResetAction += PowerbarValueReset;
        ResetPowerbar();
        Visuals.SetActive(false);
        Counting = false;

    }
    private void Update()
    {
        BarChange(PowerbarCycle());
    }
    private void OnDisable()
    {
        PowerbarActivateAction = null;
        PowerbarDeactivateAction = null;
        PowerbarValueResetAction = null;
    }
    float PowerbarCycle()
    {
        if (Counting)
        {
            if (Rewind)
            {
                CurrentValue += PowerbarSpeed * Time.deltaTime;
            }
            else
            {
                CurrentValue -= PowerbarSpeed * Time.deltaTime;
            }
            if (CurrentValue >= 100)
            {
                Rewind = false;
            }
            else if (CurrentValue <= 0)
            {
                Rewind = true;
            }
        }

        return CurrentValue;
    }

    void BarChange(float barvalue)
    {
        float amount = (barvalue / 100f) * 90f / 360f;
        float buttonAngle = amount * 360;
        ButtonPivot.localEulerAngles = new Vector3(0, 0, -buttonAngle);
    }

    void PowerbarValueReset()
    {
        CurrentValue = 0;
    }
    void ActivatePowerbar()
    {

        Visuals.SetActive(true);
        CurrentValue = 0;
        Counting = true;
        if (Player_Active.SelectedObject.transform.position.y > 0)
        {
            if (Player_Active.SelectedObject.transform.position.x > 0)
            {
                Visuals.GetComponent<RectTransform>().rotation = Quaternion.Euler(0, 0, -90);
            }
            else if (Player_Active.SelectedObject.transform.position.x < 0)
            {
                Visuals.GetComponent<RectTransform>().rotation = Quaternion.Euler(0, 0, 0);
            }
        }
        else if (Player_Active.SelectedObject.transform.position.y < 0)
        {
            if (Player_Active.SelectedObject.transform.position.x > 0)
            {
                Visuals.GetComponent<RectTransform>().rotation = Quaternion.Euler(0, 0, -90);
            }
            else if (Player_Active.SelectedObject.transform.position.x < 0)
            {
                Visuals.GetComponent<RectTransform>().rotation = Quaternion.Euler(0, 0, 0);
            }
        }
    }

    void ResetPowerbar()
    {
        Visuals.SetActive(false);
        CurrentValue = 0;
        Counting = false;
    }
}
