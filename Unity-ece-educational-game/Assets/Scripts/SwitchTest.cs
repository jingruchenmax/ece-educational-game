using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SwitchTest : MonoBehaviour
{
    public bool switchsetting;
    SwitchController switchController;
    // Start is called before the first frame update
    void Start()
    {
        switchController = GetComponent<SwitchController>();
    }

    // Update is called once per frame
    void Update()
    {
        switchController.switchStatus = switchsetting;
    }
}
