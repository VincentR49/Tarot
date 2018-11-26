using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class NewGameButtonsDisplay : CanvasGroupDisplay
{
    public GamePhaseVariable gamePhase;

    private void Update()
    {
        //if (gamePhase.Value == GamePhase.None)
        {
            if (!visible) Show();
        }
        /*
        else
        {
            if (visible) Hide();
        }
        */
    }
}
