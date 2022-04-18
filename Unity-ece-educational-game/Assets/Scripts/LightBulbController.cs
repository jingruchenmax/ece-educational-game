using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class LightBulbController : MonoBehaviour
{
    [SerializeField]
    Transform componentObject;
    public float lightIntensity
    {
        get { return _lightIntensity; }
        set
        {
            if (_lightIntensity != value)
            {
                _lightIntensity = value;
                OnVariableChange?.Invoke();
            }
        }
    }
    private float _lightIntensity;
    public bool lightBulbStatus //if is plugged in 
    {
        get { return _lightBulbStatus; }
        set
        {
            if (_lightBulbStatus != value)
            {
                _lightBulbStatus = value;
                OnStatusChange?.Invoke();
            }
        }
    }
    private bool _lightBulbStatus;
    [SerializeField]
    private Transform lightObject;
    private Material lightMaterial;

    public delegate void OnVariableChangeDelegate();
    public event OnVariableChangeDelegate OnVariableChange;
    public delegate void OnStatusChangeDelegate();
    public event OnStatusChangeDelegate OnStatusChange;
    private Animator animator;

    LightBulbRenderer lightBulbRenderer;
    private void Awake()
    {
        if (componentObject != null)
            lightBulbRenderer = componentObject.GetComponent<LightBulbRenderer>();
        animator = GetComponent<Animator>();
        _lightIntensity = 0;
        lightIntensity = 0;
        _lightBulbStatus = false;
        lightBulbStatus = false;
        

    }
    private void Start()
    {
        OnVariableChange += VariableChangeHandler;
        OnStatusChange += VariableChangeHandler;
        OnStatusChange += StatusChangeHandler;
        lightMaterial = lightObject.GetComponent<SpriteRenderer>().material;
        lightBulbRenderer.SetRendererActive(false);
    }
    private void VariableChangeHandler()
    {
        if (lightBulbStatus)
            lightMaterial.SetFloat("_LightIntensity", lightIntensity);
        else
            lightMaterial.SetFloat("_LightIntensity", 0);

    }

    private void StatusChangeHandler()
    {
        switch (lightBulbStatus)
        {
            case true:
                animator.Play("LightPlugIn");
                break;
            case false:
                animator.Play("LightPlugOut");
                break;
        }
    }

    public void DisableResistorRenderer()
    {
        lightBulbRenderer.SetRendererActive(false);
    }

    public void EnableResistorRenderer()
    {
        lightBulbRenderer.SetRendererActive(true);
    }
    public void OnTriggerEnter2D(Collider2D other)
    {
        Debug.Log("collide");
        if (other.GetComponent<LightBulbRenderer>() != null)
        {
            other.gameObject.transform.position = componentObject.transform.position;
            LightBulbRenderer rr = other.gameObject.GetComponent<LightBulbRenderer>();
            rr.SetRendererActive(false);
            lightBulbStatus = true;
            if (other.GetComponent<InventoryItem>() != null)
            {
                InventoryItem ii = other.GetComponent<InventoryItem>();
                ii.canDrop = true;
            }
        }
    }

    public void OnTriggerStay2D(Collider2D other)
    {
        other.gameObject.transform.position = componentObject.transform.position;

    }
    public void OnTriggerExit2D(Collider2D other)
    {
        Debug.Log("collide");
        if (other.GetComponent<LightBulbRenderer>() != null)
        {
            other.gameObject.transform.position = componentObject.transform.position;
            LightBulbRenderer rr = other.gameObject.GetComponent<LightBulbRenderer>();
            rr.SetRendererActive(true);
            lightBulbStatus = false;
            if (other.GetComponent<InventoryItem>() != null)
            {
                InventoryItem ii = other.GetComponent<InventoryItem>();
                ii.canDrop = false;
            }
       
        }
    }
}
