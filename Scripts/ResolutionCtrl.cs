using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class ResolutionCtrl : MonoBehaviour
{
    RectTransform trans;
    CanvasScaler scale;

    private void Awake()
    {
        trans = this.GetComponent<RectTransform>();
        scale = this.GetComponent<CanvasScaler>();

        if (trans.rect.height < 2560)
        {
            //scale.screenMatchMode = CanvasScaler.ScreenMatchMode.Expand;
            scale.matchWidthOrHeight = 1f;
        }

    }

}
