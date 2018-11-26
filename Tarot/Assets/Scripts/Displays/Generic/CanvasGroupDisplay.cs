using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[RequireComponent(typeof(CanvasGroup))]
public class CanvasGroupDisplay : MonoBehaviour {

    protected CanvasGroup canvasGroup;
    protected bool visible;

    protected void Awake()
    {
        canvasGroup = GetComponent<CanvasGroup>();
        visible = canvasGroup.alpha != 0f;
    }

    public virtual void Hide()
    {
        canvasGroup.alpha = 0f;
        canvasGroup.blocksRaycasts = false;
        canvasGroup.interactable = false;
        visible = false;
    }

    public virtual void Show()
    {
        canvasGroup.alpha = 1f;
        canvasGroup.blocksRaycasts = true;
        canvasGroup.interactable = true;
        visible = true;
    }
}
