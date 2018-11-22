using UnityEngine;
using UnityEngine.UI;

public class BidButtonsDisplay : MonoBehaviour {

   
    private CanvasGroup canvasGroup;
    public GamePhaseVariable gamePhase;
    bool visible;

    private void Awake()
    {
        canvasGroup = GetComponent<CanvasGroup>();
        Hide();
    }

    private void Update()
    {
        if (gamePhase.Value == GamePhase.Bidding)
        {
            if (!visible) Show();
        }
        else
        {
            if (visible) Hide();
        }
    }

    public void Hide()
    {
        canvasGroup.alpha = 0f;
        canvasGroup.blocksRaycasts = false;
        canvasGroup.interactable = false;
        visible = false;
    }

    public void Show()
    {
        canvasGroup.alpha = 1f; 
        canvasGroup.blocksRaycasts = true;
        canvasGroup.interactable = true;
        visible = true;
    }
}
