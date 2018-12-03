using System.Collections;
using System.Collections.Generic;
using UnityEngine;

// Gère l'affichage des cartes en jeu
public class BoardDisplay : MonoBehaviour
{
    public CardListVariable selectedCards;
	public GamePhaseVariable gamePhase;
	public GameObject cardPrefab;
	public List<GameObject> cardPlaceHolders;
	
	private List<GameObject> cards;
	String lastCards = "";
	
	
	void Update()
	{
		if (gamePhase.Value == GamePhase.Play)
		{
			if (selectedCards.Value.ToString() != lastCards)
			{
				lastCards = selectedCards.Value.ToString();
				UpdateDisplay();
			}
		}
		else
		{
			Clean();
		}
	}
	
	
	void Clean()
	{
		if (cards != null && cards.Count > 0)
        {
            foreach (GameObject go in cards)
            {
                Destroy(go);
            }
        }   
        cards = new List<GameObject>();
	}
	
	// à optimiser
	private void UpdateDisplay()
    {
        Clean();   
        if (selectedCards.Value == null) return;
        int count = 0;
        foreach (Card card in selectedCards.Value)
        {
            GameObject cardObject = Instantiate(cardPrefab, cardPlaceHolders[count].transform);
            CardDisplay cardDisplay = cardObject.GetComponent<CardDisplay>();
            cardDisplay.card = card;
            cards.Add(cardObject);
            count++;
        }
    }
}
