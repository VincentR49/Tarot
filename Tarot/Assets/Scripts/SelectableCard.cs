using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.EventSystems;
using UnityEngine.UI;

public class SelectableCard : Selectable {

   
    protected override void Start () {
        base.Start();
	}

    public override void OnSelect(BaseEventData eventData)
    {
        base.OnSelect(eventData);
        Debug.Log("On selected");
    }
}
