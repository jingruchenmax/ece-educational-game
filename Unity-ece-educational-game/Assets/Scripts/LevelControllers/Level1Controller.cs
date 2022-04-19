using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Events;

public class Level1Controller : MonoBehaviour
{
    [SerializeField]
    LightBulbController lightBulbController;
    [SerializeField]
    SwitchController switchController;
    [SerializeField]
    ResistorController resistorController;
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
        resistorController.OnVariableChange.AddListener(OnCheckLevelStatusHandler);
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
        if(lightBulbController.lightBulbStatus == false || switchController.switchStatus == false || resistorController.resistorStatus == false)
        {
            lightBulbController.lightIntensity = 0;
            meterController.meterValue = 0;
        }

        else if (lightBulbController.lightBulbStatus == true && switchController.switchStatus == true && resistorController.resistorStatus == true && !isLevelClear)
        {
            lightBulbController.lightIntensity = 0.5f;
            meterController.meterValue = 50f;
            isLevelClear = true;
        }
    }

    void LevelClearHandler()
    {
        this.enabled = false;
        gameManager.OnLevelClear.Invoke();
    }
}
