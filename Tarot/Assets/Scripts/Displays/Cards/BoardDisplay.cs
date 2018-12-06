using System.Collections;
using System.Collections.Generic;
using UnityEngine;

// Gère l'affichage des cartes en jeu
public class BoardDisplay : MonoBehaviour
{
    public CardListVariable playedCards;
	public GamePhaseVariable gamePhase;
	public GameObject cardPrefab;
	public PlayerList players;
	public List<GameObject> cardPlaceHolders;
	
	private List<GameObject> cards;
	string lastCards = "";
	
	
	void Update()
	{
		if (gamePhase.Value == GamePhase.Play)
		{
			if (playedCards.Value.ToString() != lastCards)
			{
				lastCards = playedCards.Value.ToString();
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
        if (playedCards.Value == null) return;
        int count = 0;
        foreach (Card card in playedCards.Value)
        {
            GameObject cardObject = Instantiate(cardPrefab, cardPlaceHolders[GetPlayerIndex(count)].transform);
            cardObject.transform.localPosition = Vector3.zero;
            CardDisplay cardDisplay = cardObject.GetComponent<CardDisplay>();
            cardDisplay.card = card;
            cards.Add(cardObject);
            count++;
        }
    }
	
	
	private int GetPlayerIndex(int boardCardIndex)
	{
		Player firstPlayer = players.GetFirstPlayerThisTurn();
		int firstPlayerIndex = players.Items.IndexOf(firstPlayer);
		int nPlayer = players.Count;
		int index = (firstPlayerIndex + boardCardIndex) % nPlayer;
		return index;
	}
}
