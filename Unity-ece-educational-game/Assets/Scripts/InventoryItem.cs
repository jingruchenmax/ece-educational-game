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
    public bool canDrop = true;
    Vector3 originPos;
    RectTransform rectTransform;
    RectTransform rectTextTransform;
    Transform textBoxObject;
    Vector3 originScale;
    Vector3 originTextScale;
    private void Awake()
    {
        if (textBoxObject == null)
            textBoxObject = transform.GetChild(0);
        rectTransform = GetComponent<RectTransform>();
        rectTextTransform = textBoxObject.GetComponent<RectTransform>();
        originPos = rectTransform.localPosition;
        textBoxObject.gameObject.SetActive(false);
        originScale = rectTransform.localScale;
        originTextScale = rectTextTransform.localScale;
    }
    // Reference to the canvas.

    public void OnMouseEnter()
    {
        textBoxObject.gameObject.SetActive(true);
    }
    public void OnMouseExit()
    {
        textBoxObject.gameObject.SetActive(false);
    }
    public void OnMouseDown()
    {
        rectTransform.localScale = originScale*1.2f;
        textBoxObject.localScale = originTextScale/1.2f;
    }
    public void OnMouseDrag()
    {
        Vector3 pos = Camera.main.ScreenToWorldPoint(Input.mousePosition);
        transform.position = new Vector3(pos.x,pos.y, transform.position.z);
    }
    public void OnMouseUp()
    {
        if (!canDrop)
        {
            rectTransform.localPosition = originPos;
        }
        rectTransform.localScale = originScale;
        textBoxObject.localScale = originTextScale;
    }
}