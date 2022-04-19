using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using resistor;
using UnityEngine.Events;

public class ResistorController : MonoBehaviour
{
    [SerializeField]
    Transform componentObject;
    ResistorRenderer resistorRenderer;
    Animator animator;
    ResistorRenderer occupiedResistorRenderer;
    InventoryItem inventoryItem;
    public int resistorValue { get { return resistorRenderer.resistorValue; } }
    public bool resistorStatus
    {
        get { return _resistorStatus; }
        set
        {
            if (_resistorStatus != value)
            {
                _resistorStatus = value;
                OnVariableChange?.Invoke();
            }
        }
    }
    public bool _resistorStatus;
    public UnityEvent OnVariableChange;
    void Awake()
    {
        if (componentObject != null)
            resistorRenderer = componentObject.GetComponent<ResistorRenderer>();
        animator = GetComponent<Animator>();
        resistorStatus = _resistorStatus;
        if(!resistorStatus)
            resistorRenderer.SetRendererActive(false);
    }
    public void ChangeResistorValue(ResistorParameters parameters)
    {
        resistorRenderer.resistorAsset = parameters;
        // visualize the calculation of resistor
        //string st = string.Format("total {0}, band 1 {1}, band 2 {2}, band multiplier {3}, calculate {4}", resistorValue, (int)resistorRenderer.resistorAsset.band_1.value, (int)resistorRenderer.resistorAsset.band_2.value, (int)resistorRenderer.resistorAsset.multiplier.value, (int)Mathf.Pow(10, (int)resistorRenderer.resistorAsset.multiplier.value));
        //Debug.Log(st);
    }
    private void Start()
    {
        OnVariableChange.AddListener(VariableChangeHandler);
    }
    void VariableChangeHandler()
    {
        switch (resistorStatus)
        {
            case true:
                animator.Play("ResistorPlugIn");              
                break;
            case false:
                animator.Play("ResistorPlugOut");
                break;
        }
    }

    public void DisableResistorRenderer()
    {
        resistorRenderer.SetRendererActive(false);
    }

    public void EnableResistorRenderer()
    {
        resistorRenderer.SetRendererActive(true);
    }
    public void OnTriggerEnter2D(Collider2D other)
    {
        if (other.GetComponent<ResistorRenderer>() != null && resistorStatus) // caution: a new resistor is trying to replace the old one!!
        {
            if (other.GetComponent<LightBulbRenderer>() != occupiedResistorRenderer)
            {
                occupiedResistorRenderer.SetRendererActive(true);
                inventoryItem.canDrop = false;
                inventoryItem.OnMouseUp(); // throw item back to inventory
                //replaced
                occupiedResistorRenderer = other.GetComponent<ResistorRenderer>();
                occupiedResistorRenderer.SetRendererActive(false);
                ChangeResistorValue(occupiedResistorRenderer.resistorAsset);
                resistorRenderer.OnValueChange();
                OnVariableChange.Invoke();
                inventoryItem = occupiedResistorRenderer.GetComponent<InventoryItem>();
                inventoryItem.canDrop = true; //stay here
                
                                              // resistorStatus = true; omitted
            }
        }

        if (other.GetComponent<ResistorRenderer>() != null && !resistorStatus)
        {
            occupiedResistorRenderer = other.GetComponent<ResistorRenderer>();
            ChangeResistorValue(occupiedResistorRenderer.resistorAsset);
            resistorRenderer.OnValueChange();
            occupiedResistorRenderer.SetRendererActive(false);
            inventoryItem = occupiedResistorRenderer.GetComponent<InventoryItem>();
            inventoryItem.canDrop = true;
            resistorStatus = true;
        }
    }

    public void OnTriggerStay2D(Collider2D other)
    {
        other.gameObject.transform.position = componentObject.transform.position;

    }
    public void OnTriggerExit2D(Collider2D other)
    {

        if (other.GetComponent<ResistorRenderer>() != null && resistorStatus)
        {
            if (other.GetComponent<ResistorRenderer>() == occupiedResistorRenderer)
            {
                occupiedResistorRenderer.SetRendererActive(true);
                inventoryItem.canDrop = false;
                resistorRenderer.SetRendererActive(false);
                resistorStatus = false;
            }      
        }
    }
}
