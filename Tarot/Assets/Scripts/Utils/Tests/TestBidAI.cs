using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using static Utils;

// Test de l'AI pour les enchères
public class TestBidAI : MonoBehaviour 
{
	// Parameters
	public CpuPlayer player;
	public int nPlayer = 4;
	public int playerPosition = 0;
	public int nRandomTests = 10;
	public Deck standardDeck;
	
	private int NCards => (standardDeck.Count - Dog.GetNumberOfCards(nPlayer)) / nPlayer;

	
	private void Start()
	{
        StartCoroutine (RandomCardCoroutine(nRandomTests));
    }
	
	
	private void StartTestRandomCards()
	{
		Debug.Log("Start test bid AI with random hand");
		Debug.Log("-----------------------------------------------");
        player.PrepareForNewGame();
		List<int> cardIndexes = GetRandomUniqueIndexes (NCards, 0, standardDeck.Count-1);
		foreach (int index in cardIndexes)
		{
			AddCardToHand (standardDeck.Items[index]);
		}
		player.MakeABid (nPlayer, playerPosition, Bid.Pass);
		Debug.Log("Player Bid: " + player.CurrentBid);
		Debug.Log("-----------------------------------------------");
		Debug.Log("");
    }
	

    private IEnumerator RandomCardCoroutine(int nTrials)
    {
        for (int i = 0; i < nRandomTests; i++)
        {
            StartTestRandomCards();
            yield return new WaitForSeconds(0.5f);
        }
    }


	private void AddCardToHand(Card card)
	{
		player.Hand.Add (card); 
		Debug.Log("Add card to hand: " + card);
	}
	
	
	private void AddCardToHand(CardType type, CardRank rank) 
	{ 
		AddCardToHand (GetCard (type, rank));
	}
	
	
	private Card GetCard(CardType type, CardRank rank) => standardDeck.GetCard(type, rank);
}