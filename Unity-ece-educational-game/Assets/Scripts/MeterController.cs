using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class MeterController : MonoBehaviour
{
    public float meterValue { 
        get { return _meterValue; }
        set
        {        
            if (_meterValue != value)
            {
                _meterValue = value;
                OnVariableChange?.Invoke();
            }
        }
    }
    private float _meterValue;
    
    public float maxValue;
    [SerializeField]
    private Transform pointer;
    
    public float meterPercentage 
    { 
        get { return Mathf.Clamp(meterValue/maxValue,0,1); }
    }


    public delegate void OnVariableChangeDelegate();
    public event OnVariableChangeDelegate OnVariableChange;
    // Start is called before the first frame update
    void Start()
    {
        OnVariableChange += VariableChangeHandler;
    }

    private void VariableChangeHandler()
    {
        pointer.localEulerAngles = new Vector3(0,0,50-meterPercentage*100);
    }
}
