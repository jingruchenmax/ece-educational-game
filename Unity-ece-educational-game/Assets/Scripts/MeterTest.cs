using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class MeterTest : MonoBehaviour
{
    public float max;
    public float v;
    MeterController meterController;
    private void Start()
    {
        meterController = GetComponent<MeterController>();
    }
    void Update()
    {
        meterController.maxValue = max;
        meterController.meterValue = v;
    }
}
