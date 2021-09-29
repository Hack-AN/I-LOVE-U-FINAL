using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.EventSystems;

public class CraftCtrl : MonoBehaviour, IBeginDragHandler, IEndDragHandler
{

    public bool isdragging = false;


    void IBeginDragHandler.OnBeginDrag(PointerEventData eventData)
    {
        isdragging = true;
    }

    void IEndDragHandler.OnEndDrag(PointerEventData eventData)
    {
        isdragging = false;
    }
}
