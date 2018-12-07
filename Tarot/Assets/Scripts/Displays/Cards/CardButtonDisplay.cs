using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

[RequireComponent(typeof(Image))]
[RequireComponent(typeof(Button))]
// Affichage d'une carte pouvant être activée comme un bouton
public class CardButtonDisplay : MonoBehaviour
{
    private Card card;
    private Image image;
    private Button button;

    private void Awake()
    {
        image = GetComponent<Image>();
        button = GetComponent<Button>();
    }

    public void UpdateDisplay()
    {
        image.sprite = card.sprite;
    }


    public void SetCard(Card card)
    {
        this.card = card;
        UpdateDisplay();
    }


    public Button GetButton() => button;
}
