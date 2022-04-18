using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using resistor;

public class ResistorController : MonoBehaviour
{
    [SerializeField]
    Transform componentObject;
    ResistorRenderer resistorRenderer;
    Animator animator;

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
    private bool _resistorStatus;
    public delegate void OnVariableChangeDelegate();
    public event OnVariableChangeDelegate OnVariableChange;
    void Awake()
    {
        if (componentObject != null)
            resistorRenderer = componentObject.GetComponent<ResistorRenderer>();
        _resistorStatus = false;
        resistorStatus = false;
        animator = GetComponent<Animator>();
        resistorRenderer.SetRendererActive(false);
    }
    public void ChangeResistorValue(ResistorParameters parameters)
    {
        resistorRenderer.resistorAsset = parameters;
    }
    private void Start()
    {
        OnVariableChange += VariableChangeHandler;
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
        if (other.GetComponent<ResistorRenderer>() != null)
        {
            ResistorRenderer rr = other.GetComponent<ResistorRenderer>();
            rr.SetRendererActive(false);
            ChangeResistorValue(rr.resistorAsset);
            if (other.GetComponent<InventoryItem>() != null)
            {
                Debug.Log("set");
                InventoryItem ii = other.GetComponent<InventoryItem>();
                ii.canDrop = true;
            }
            resistorStatus = true;
        }
    }

    public void OnTriggerStay2D(Collider2D other)
    {
        other.gameObject.transform.position = componentObject.transform.position;

    }
    public void OnTriggerExit2D(Collider2D other)
    {
        if (other.GetComponent<ResistorRenderer>() != null)
        {
            ResistorRenderer rr = other.GetComponent<ResistorRenderer>();
            rr.SetRendererActive(true);
            resistorRenderer.SetRendererActive(false);
        }
        resistorStatus = false;

        if (other.GetComponent<ResistorRenderer>() != null)
        {
            ResistorRenderer rr = other.GetComponent<ResistorRenderer>();
            rr.SetRendererActive(true);
            if (other.GetComponent<InventoryItem>() != null)
            {
                InventoryItem ii = other.GetComponent<InventoryItem>();
                ii.canDrop = false;
            }
            resistorStatus = false;
        }
    }
}
