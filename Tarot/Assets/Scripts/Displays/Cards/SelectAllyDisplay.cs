using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

// Gère l'affichage du choix de l'allié pour le jeu à 5 joueurs
public class SelectAllyDisplay : MonoBehaviour
{
    public PlayerList players;
    public Deck standardDeck;
    public CardListVariable selectedCard;
	public GameObject cardButtonPrefab;
	public GamePhaseVariable gamePhase;

	private Player Taker => players.GetTaker();
	private List<GameObject> cards;
	private bool buttonsShown;
	
	
    private void Awake()
    {
	   cards = new List<GameObject>();
    }


    private void Update()
    {
		if (gamePhase.Value == GamePhase.AllySelection)
		{
			if (cards.Count == 0)
			{
				GenerateCards();
			}	
		}
		else if (cards.Count > 0)
		{
			Clean();
		}
    }
	

    private void GenerateCards()
    {
        Clean();
        CardRank minRank = Taker.GetMinRankCallable();
        CardRank rank = CardRank.Roi;
        while (rank >= minRank)
        {
            GenerateCard(CardType.Heart, rank);
            GenerateCard(CardType.Club, rank);
            GenerateCard(CardType.Diamond, rank);
            GenerateCard(CardType.Spade, rank);
            rank--;
        }    
    }


    private GameObject GenerateCard(CardType type, CardRank rank)
    {
        Card card = standardDeck.GetCard(type, rank);
        GameObject go = Instantiate(cardButtonPrefab, transform);
        CardButtonDisplay cardButtonDisplay = go.GetComponent<CardButtonDisplay>();
        cardButtonDisplay.SetCard (card);
        cardButtonDisplay.GetButton().onClick.AddListener(() => SelectCard(card));
        cards.Add(go);
        return go;
    }


    private void SelectCard(Card card)
    {
        Debug.Log("Select Card: " + card);
        selectedCard.Clear();
        selectedCard.Add(card);
    }


	void Clean()
	{
		if (cards != null)
        {
            foreach (GameObject go in cards)
            {
                Destroy(go);
            }
        }   
        cards = new List<GameObject>();
	}
}