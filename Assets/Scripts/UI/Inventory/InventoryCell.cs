using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.EventSystems;
using UnityEngine.UI;

public class InventoryCell : MonoBehaviour, IDragHandler, IEndDragHandler, IBeginDragHandler
{
    public event Action Ejecting;
    public event Action <string, GameObject> ActionMenuOn;

    [SerializeField]
    GameObject cellGO;

    [SerializeField]
    private Text nameField;
    [SerializeField]
    private Image iconField;

    private Transform draggingParent;
    private Transform originalParent;

    [SerializeField]
    private RectTransform cell;


    public void Init(Transform draggingParent)
    {
        this.draggingParent = draggingParent;
        originalParent = transform.parent;
    }
    public void Render(IItem item)
    {
        nameField.text = item.Name;
        iconField.sprite = item.UIcon;
    }
    public void OnBeginDrag(PointerEventData eventData)
    {
        transform.parent = draggingParent;
    }

    public void OnDrag(PointerEventData eventData)
    {
        transform.position = Input.mousePosition;
    }

    public void OnEndDrag(PointerEventData eventData)
    {
        if (In((RectTransform)originalParent))
            InsertInGrid();
        else
            Eject();
    }

    private bool In(RectTransform originalParent)
    {
        return RectTransformUtility.RectangleContainsScreenPoint(originalParent, transform.position);
    }

    private bool MouseIn(RectTransform originalParent)
    {
        return RectTransformUtility.RectangleContainsScreenPoint(originalParent, Input.mousePosition);
    }

    private void RightClick()
    {
        if (MouseIn((RectTransform)cell))
        {
            ActionMenuOn?.Invoke(nameField.text, cellGO);
        }
    }

    private void Eject()
    {
       Ejecting?.Invoke();
    }

    private void InsertInGrid()
    {
        int closestIndex = 0;

        for (int i = 0; i < originalParent.transform.childCount; i++)
        {
            if (Vector3.Distance(transform.position, originalParent.GetChild(i).position) <
                Vector3.Distance(transform.position, originalParent.GetChild(closestIndex).position))

            {
                closestIndex = i;
            }
        }
        transform.parent = originalParent;
        transform.SetSiblingIndex(closestIndex);
    }
    private void Update()
    {
        if(Input.GetKeyDown(KeyCode.Mouse1))
        {
            RightClick();
        }
    }
}
