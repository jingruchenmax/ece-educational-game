using System.Collections;
using System.Collections.Generic;
using UnityEngine;
[ExecuteInEditMode]
public class LightBulbRenderer : MonoBehaviour
{
    [SerializeField]
    SpriteRenderer lightBulb;
    [SerializeField]
    SpriteRenderer lightObject;

    public void SetRendererActive(bool status)
    {
        switch (status)
        {
            case true:
                lightBulb.enabled = true;
                lightObject.enabled = true;
                break;
            case false:
                lightBulb.enabled = false;
                lightObject.enabled = false;
                break;
        }
    }


}
