using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.EventSystems;
using UnityEngine.UI;
/// <summary>
/// Class responsible for dragging itself.
/// Using EventSystems interfaces.
/// </summary>
public class InventoryItem : MonoBehaviour
{
    public bool canDrop = false;
    Vector3 originPos;
    RectTransform rectTransform;
    private void Awake()
    {
        rectTransform = GetComponent<RectTransform>();
        originPos = rectTransform.anchoredPosition;
    }
    // Reference to the canvas.

    public void OnMouseEnter()
    {
        canDrop = false;
    }
    public void OnMouseDrag()
    {
        Vector3 pos = Camera.main.ScreenToWorldPoint(Input.mousePosition);
        transform.position = new Vector3(pos.x,pos.y, transform.position.z);
    }
    public void OnMouseUp()
    {

       
    }
}