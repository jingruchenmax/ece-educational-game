using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Events;

public class Level3Controller : MonoBehaviour
{
    [SerializeField]
    LightBulbController lightBulb1Controller;
    [SerializeField]
    LightBulbController lightBulb2Controller;
    [SerializeField]
    SwitchController switchController;
    [SerializeField]
    MeterController meter1Controller;
    [SerializeField]
    MeterController meter2Controller;


    GameManager gameManager;
    public bool isLevelClear
    {
        get { return _isLevelClear; }
        set
        {
            if (_isLevelClear != value)
            {
                _isLevelClear = value;
                OnLevelClear?.Invoke();
            }
        }
    }
    private bool _isLevelClear;
    public float levelTimer;
    public UnityEvent OnLevelClear;
    void Awake()
    {
        lightBulb1Controller.OnVariableChange.AddListener(OnCheckLevelStatusHandler);
        lightBulb1Controller.OnStatusChange.AddListener(OnCheckLevelStatusHandler);
        lightBulb2Controller.OnVariableChange.AddListener(OnCheckLevelStatusHandler);
        lightBulb2Controller.OnStatusChange.AddListener(OnCheckLevelStatusHandler);
        switchController.OnVariableChange.AddListener(OnCheckLevelStatusHandler);
        meter1Controller.OnVariableChange.AddListener(OnCheckLevelStatusHandler);
        meter2Controller.OnVariableChange.AddListener(OnCheckLevelStatusHandler);
        OnLevelClear.AddListener(LevelClearHandler);
        gameManager = GameObject.Find("GameManager").GetComponent<GameManager>();

    }
    void Start()
    {
        this.enabled = true;
        isLevelClear = false;
    }
    void OnCheckLevelStatusHandler()
    {
        //Check and update level variables
        if ((lightBulb1Controller.lightBulbStatus == false && lightBulb2Controller.lightBulbStatus == false) || switchController.switchStatus == false )
        {
            lightBulb1Controller.lightIntensity = 0;
            meter1Controller.meterValue = 0;
            lightBulb2Controller.lightIntensity = 0;
            meter2Controller.meterValue = 0;
        }

        else if (!isLevelClear)
        {
            
            if (lightBulb1Controller.resistorValue == 10)
            {
                lightBulb1Controller.lightIntensity = 0.2f;
                meter1Controller.meterValue = 20;
        
            }


            if (lightBulb1Controller.lightBulbStatus == false)
            {
                lightBulb1Controller.lightIntensity = 0;
                meter1Controller.meterValue = 0;
            }

            if (lightBulb2Controller.resistorValue == 40)
            {
                lightBulb2Controller.lightIntensity = 0.8f;
                meter2Controller.meterValue = 80;
            }

            if (lightBulb1Controller.resistorValue == 40)
            {
                lightBulb1Controller.lightIntensity = 0.8f;
                meter1Controller.meterValue = 80;

            }

            if (lightBulb2Controller.resistorValue == 10)
            {
                lightBulb2Controller.lightIntensity = 0.2f;
                meter2Controller.meterValue = 20;
            }

            if (lightBulb2Controller.lightBulbStatus == false)
            {
                lightBulb2Controller.lightIntensity = 0;
                meter2Controller.meterValue = 0;
            }

            if (lightBulb2Controller.resistorValue == 10 && lightBulb1Controller.resistorValue == 40)
            {
                isLevelClear = true;
            }
        }
    }

    void LevelClearHandler()
    {
        this.enabled = false;
        gameManager.OnLevelClear.Invoke();
    }
}
