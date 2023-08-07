using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Events;

public class LightBulbController : MonoBehaviour
{
    [SerializeField]
    Transform componentObject;
    LightBulbRenderer occupiedLightBulbRenderer;
    InventoryItem inventoryItem;
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
    public bool _lightBulbStatus;
    [SerializeField]
    private Transform lightObject;
    private Material lightMaterial;

    public UnityEvent OnVariableChange;
    public UnityEvent OnStatusChange;
    private Animator animator;

    LightBulbRenderer lightBulbRenderer;

    public int resistorValue { get { return lightBulbRenderer.resistorValue; } }
    private void Awake()
    {
        if (componentObject != null)
            lightBulbRenderer = componentObject.GetComponent<LightBulbRenderer>();
        animator = GetComponent<Animator>();
        _lightIntensity = 0;
        lightIntensity = 0;
        lightBulbStatus = _lightBulbStatus;
    }
    private void Start()
    {
        OnVariableChange.AddListener(VariableChangeHandler);
        OnStatusChange.AddListener(VariableChangeHandler);
        OnStatusChange.AddListener(StatusChangeHandler);
        lightMaterial = lightObject.GetComponent<SpriteRenderer>().material;
        if(!lightBulbStatus)
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
        if (other.GetComponent<LightBulbRenderer>() != null && !lightBulbStatus)
        {
            other.gameObject.transform.position = componentObject.transform.position;
            occupiedLightBulbRenderer = other.gameObject.GetComponent<LightBulbRenderer>();
            lightBulbRenderer.resistorValue = occupiedLightBulbRenderer.resistorValue;
            occupiedLightBulbRenderer.SetRendererActive(false);
            lightBulbStatus = true;
            inventoryItem = other.GetComponent<InventoryItem>();
            inventoryItem.canDrop = true;
        }
        if (other.GetComponent<LightBulbRenderer>() != null && lightBulbStatus) // ready for replace
        {
            if (other.GetComponent<LightBulbRenderer>() != occupiedLightBulbRenderer)
            {
                other.gameObject.transform.position = componentObject.transform.position;
                occupiedLightBulbRenderer.SetRendererActive(true);
                inventoryItem.canDrop = false;
                inventoryItem.OnMouseUp(); // throw item back to inventory
                occupiedLightBulbRenderer = other.GetComponent<LightBulbRenderer>();
                occupiedLightBulbRenderer.SetRendererActive(false);
                lightBulbRenderer.resistorValue = occupiedLightBulbRenderer.resistorValue;
                OnVariableChange.Invoke();
                inventoryItem = other.GetComponent<InventoryItem>();
                inventoryItem.canDrop = true;
                
            }
            
        }
    }

    public void OnTriggerStay2D(Collider2D other)
    {
        other.gameObject.transform.position = componentObject.transform.position;

    }
    public void OnTriggerExit2D(Collider2D other)
    {
        if (other.GetComponent<LightBulbRenderer>() != null && lightBulbStatus)
        {
            if (other.GetComponent<LightBulbRenderer>() == occupiedLightBulbRenderer)
            {
                occupiedLightBulbRenderer.SetRendererActive(true);
                inventoryItem.canDrop = false;
                lightBulbRenderer.SetRendererActive(false);
                lightBulbStatus = false;
                lightBulbRenderer.resistorValue = 0;
            }           
        }
    }
}
