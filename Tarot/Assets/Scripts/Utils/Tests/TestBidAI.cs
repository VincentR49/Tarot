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
		TestGarde();
		TestPasse();
		TestGardeSans();
		TestGardeContre();
        StartCoroutine (RandomCardCoroutine(nRandomTests));
    }
	

	/*  
		Test with (58pts: GARDE):
		A	21 19 12 6 1 EX
		R C 10 8 1
		V 8
		D C 7
		6 4
	*/
	private void TestGarde()
	{
		Debug.Log("Start test bid AI with specific hand: expected GARDE with: 58pts");
		player.PrepareForNewGame();
		// Hand
		AddCard (CardType.Trump, CardRank.TwentyOne);
		AddCard (CardType.Trump, CardRank.Nineteen);
		AddCard (CardType.Trump, CardRank.Twelve);
		AddCard (CardType.Trump, CardRank.Six);
		AddCard (CardType.Trump, CardRank.One);
		AddCard (CardType.Excuse, CardRank.None);
		AddCard (CardType.Heart, CardRank.Roi);
		AddCard (CardType.Heart, CardRank.Cavalier);
		AddCard (CardType.Heart, CardRank.Ten);
		AddCard (CardType.Heart, CardRank.Height);
		AddCard (CardType.Heart, CardRank.One);
		AddCard (CardType.Spade, CardRank.Valet);
		AddCard (CardType.Spade, CardRank.Eight);
		AddCard (CardType.Diamond, CardType.Dame);
		AddCard (CardType.Diamond, CardType.Cavalier);
		AddCard (CardType.Diamond, CardType.Seven);
		AddCard (CardType.Club, CardType.Six);
		AddCard (CardType.Club, CardType.Four);
		PrintHand();
		player.MakeABid (nPlayer, playerPosition, Bid.Pass);
		Debug.Log("Player Bid: " + player.CurrentBid);
		Debug.Log("-----------------------------------------------");
	}
	
	
	/*  
		Test with (34pts: Passe):
		A	20 18 14 13 9 8 6
		R 10 5 4
		R D 
		7 8
		8 7 5
	*/
	private void TestPass()
	{
		Debug.Log("Start test bid AI with specific hand: expected Passe with 34pts");
		player.PrepareForNewGame();
		// Hand
		AddCard (CardType.Trump, CardRank.Twenty);
		AddCard (CardType.Trump, CardRank.Eighteen);
		AddCard (CardType.Trump, CardRank.Fourteen);
		AddCard (CardType.Trump, CardRank.Thirteen);
		AddCard (CardType.Trump, CardRank.Nine);
		AddCard (CardType.Trump, CardRank.Eight);
		AddCard (CardType.Trump, CardRank.Six);
		AddCard (CardType.Heart, CardRank.Roi);
		AddCard (CardType.Heart, CardRank.Ten);
		AddCard (CardType.Heart, CardRank.Five);
		AddCard (CardType.Heart, CardRank.Four);
		AddCard (CardType.Spade, CardRank.Roi);
		AddCard (CardType.Spade, CardRank.Dame);
		AddCard (CardType.Diamond, CardType.Seven);
		AddCard (CardType.Diamond, CardType.Eight);
		AddCard (CardType.Club, CardType.Eight);
		AddCard (CardType.Club, CardType.Seven);
		AddCard (CardType.Club, CardType.Five);
		PrintHand();
		player.MakeABid (nPlayer, playerPosition, Bid.Pass);
		Debug.Log("Player Bid: " + player.CurrentBid);
		Debug.Log("-----------------------------------------------");
	}
	
	
	/*  
		Test with (72 pt: GARDE SANS)
		A	21 20 19 16 14 12 8 EX
		R D 8 5 2
		4
		R
		8 9 3
	*/
	private void TestGardeSans()
	{
		Debug.Log("Start test bid AI with specific hand: expected Garde Sans with 72pts");
		player.PrepareForNewGame();
		// Hand
		AddCard (CardType.Trump, CardRank.TwentyOne);
		AddCard (CardType.Trump, CardRank.Twenty);
		AddCard (CardType.Trump, CardRank.Nineteen);
		AddCard (CardType.Trump, CardRank.Sixteen);
		AddCard (CardType.Trump, CardRank.Fourteen);
		AddCard (CardType.Trump, CardRank.Twelve);
		AddCard (CardType.Trump, CardRank.Eight);
		AddCard (CardType.Excuse, CardRank.None);
		AddCard (CardType.Heart, CardRank.Roi);
		AddCard (CardType.Heart, CardRank.Dame);
		AddCard (CardType.Heart, CardRank.Eight);
		AddCard (CardType.Heart, CardRank.Five);
		AddCard (CardType.Heart, CardRank.Two);
		AddCard (CardType.Spade, CardRank.Four);
		AddCard (CardType.Diamond, CardType.Roi);
		AddCard (CardType.Club, CardType.Eight);
		AddCard (CardType.Club, CardType.Nine);
		AddCard (CardType.Club, CardType.Three);
		PrintHand();
		player.MakeABid (nPlayer, playerPosition, Bid.Pass);
		Debug.Log("Player Bid: " + player.CurrentBid);
		Debug.Log("-----------------------------------------------");
	}
	
	
	/*  
		Test with (89pts: GardeContre): (--- manque une carte)
		A	21 18 17 11 7 1 EX
		R D C V 10 8 5 
		R
		D 5 
	*/
	private void TestGardeContre()
	{
		Debug.Log("Start test bid AI with specific hand: expected GardeContre with 89pts");
		player.PrepareForNewGame();
		// Hand
		AddCard (CardType.Trump, CardRank.TwentyOne);
		AddCard (CardType.Trump, CardRank.Eighteen);
		AddCard (CardType.Trump, CardRank.Seventeen);
		AddCard (CardType.Trump, CardRank.Eleven);
		AddCard (CardType.Trump, CardRank.Seven);
		AddCard (CardType.Trump, CardRank.One);
		AddCard (CardType.Excuse, CardRank.None);
		AddCard (CardType.Heart, CardRank.Roi);
		AddCard (CardType.Heart, CardRank.Dame);
		AddCard (CardType.Heart, CardRank.Cavalier);
		AddCard (CardType.Heart, CardRank.Valet);
		AddCard (CardType.Heart, CardRank.Ten);
		AddCard (CardType.Heart, CardRank.Eight);
		AddCard (CardType.Heart, CardRank.Five);
		AddCard (CardType.Spade, CardRank.Roi);
		AddCard (CardType.Diamond, CardType.Dame);
		AddCard (CardType.Diamond, CardType.Five);
		PrintHand();
		player.MakeABid (nPlayer, playerPosition, Bid.Pass);
		Debug.Log("Player Bid: " + player.CurrentBid);
		Debug.Log("-----------------------------------------------");
	}
	
	
	private void StartTestRandomCards()
	{
		Debug.Log("Start test bid AI with random hand");
        player.PrepareForNewGame();
		List<int> cardIndexes = GetRandomUniqueIndexes (NCards, 0, standardDeck.Count-1);
		foreach (int index in cardIndexes)
		{
			player.Hand.Add (standardDeck.Items[index]);
		}
        player.SortHand();
        PrintHand();
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
            yield return new WaitForSeconds(0.2f);
        }
    }

	
	private void AddCard(CardType type, CardRank rank) 
	{ 
		player.Hand.Add (GetCard (type, rank));
	}
	
	
	private Card GetCard(CardType type, CardRank rank) => standardDeck.GetCard(type, rank);
	private void PrintHand()
	{
		Debug.Log("Hand: " + player.Hand.ToSimpleString());
	}
}