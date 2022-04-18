using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SwitchController : MonoBehaviour
{
    public bool switchStatus
    {
        get { return _switchStatus; }
        set
        {
            if (_switchStatus != value)
            {
                _switchStatus = value;
                OnVariableChange?.Invoke();
            }
        }
    }
    private bool _switchStatus;
    private Animator animator;
    public delegate void OnVariableChangeDelegate();
    public event OnVariableChangeDelegate OnVariableChange;
    // Start is called before the first frame update
    void Start()
    {
        OnVariableChange += VariableChangeHandler;
        animator = GetComponent<Animator>();
        _switchStatus = false;
        switchStatus = false;

    }

    private void VariableChangeHandler()
    {
        if (switchStatus == true)
        {
            animator.Play("SwitchTurnTrue");
        }
        else { animator.Play("SwitchTurnFalse");}
    }

    private void OnMouseDown()
    {
        switchStatus = !switchStatus;
        
    }
}
