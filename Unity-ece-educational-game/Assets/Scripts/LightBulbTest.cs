using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class LightBulbTest : MonoBehaviour
{
    [Range(0,1f)]
    public float v;
    LightBulbController Controller;
    private void Start()
    {
        Controller = GetComponent<LightBulbController>();
    }
    void Update()
    {
        Controller.lightIntensity = v;
    }
}
