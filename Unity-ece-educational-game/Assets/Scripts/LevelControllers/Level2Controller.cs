using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Events;

public class Level2Controller : MonoBehaviour
{
    [SerializeField]
    LightBulbController lightBulbController;
    [SerializeField]
    SwitchController switchController;
    [SerializeField]
    ResistorController resistor1Controller;
    [SerializeField]
    ResistorController resistor2Controller;
    [SerializeField]
    MeterController meterController;


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
        lightBulbController.OnVariableChange.AddListener(OnCheckLevelStatusHandler);
        lightBulbController.OnStatusChange.AddListener(OnCheckLevelStatusHandler);
        switchController.OnVariableChange.AddListener(OnCheckLevelStatusHandler);
        resistor1Controller.OnVariableChange.AddListener(OnCheckLevelStatusHandler);
        resistor2Controller.OnVariableChange.AddListener(OnCheckLevelStatusHandler);
        meterController.OnVariableChange.AddListener(OnCheckLevelStatusHandler);
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
        if(lightBulbController.lightBulbStatus == false  || switchController.switchStatus == false || resistor1Controller.resistorStatus == false || resistor2Controller.resistorStatus == false)
        {
            lightBulbController.lightIntensity = 0;
            meterController.meterValue = 0;
        }

        else if (lightBulbController.lightBulbStatus == true  && switchController.switchStatus == true && resistor1Controller.resistorStatus == true && resistor2Controller.resistorStatus == true && !isLevelClear)
        {
            lightBulbController.lightIntensity = 0.5f;
            meterController.meterValue = 50;
            isLevelClear = true;
            
        }
    }

    void LevelClearHandler()
    {
        this.enabled = false;
        gameManager.OnLevelClear.Invoke();
    }
}
