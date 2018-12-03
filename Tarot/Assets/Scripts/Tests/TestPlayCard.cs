using System.Collections;
using System.Collections.Generic;
using UnityEngine;

// Gestion du système de jeu
public class TestPlayCard : MonoBehaviour 
{
	public Player player;
	public Deck standardDeck; 
	private CardList boardsCards;
	
	
	void Start()
	{
		player.PrepareForNewGame();
		Test1();
		Test2();
		Test3();
		Test4();
		Test5();
	}
	
	
	void Test1()
	{
		PrepareTest("Peut jouer la couleur");
		
		AddCardBoard (CardType.Heart, CardRank.Six);
		AddCardBoard (CardType.Trump, CardRank.Twelve);

		AddCardHand (CardType.Heart, CardRank.Roi);
		AddCardHand (CardType.Spade, CardRank.Roi);
		AddCardHand (CardType.Trump, CardRank.Eleven);
		AddCardHand (CardType.Trump, CardRank.Sixteen);
		
		for (int i = 0; i < player.Hand.Count; i++) 
			CheckPlayCard (i);
	}
	
	void Test2()
	{
		PrepareTest("Coupe");
		
		AddCardBoard (CardType.Heart, CardRank.Six);
		AddCardBoard (CardType.Heart, CardRank.Valet);

		AddCardHand (CardType.Spade, CardRank.Roi);
		AddCardHand (CardType.Trump, CardRank.Eleven);
		AddCardHand (CardType.Trump, CardRank.Sixteen);
		
		for (int i = 0; i < player.Hand.Count; i++) 
			CheckPlayCard (i);
	}
	
	void Test3()
	{
		PrepareTest("Surcoupe");
		
		AddCardBoard (CardType.Heart, CardRank.Six);
		AddCardBoard (CardType.Trump, CardRank.Twelve);

		AddCardHand (CardType.Spade, CardRank.King);
		AddCardHand (CardType.Trump, CardRank.Eleven);
		AddCardHand (CardType.Trump, CardRank.Sixteen);
		
		for (int i = 0; i < player.Hand.Count; i++) 
			CheckPlayCard (i);
	}

	void Test4()
	{
		PrepareTest("Sous-coupe");
		
		AddCardBoard (CardType.Heart, CardRank.Six);
		AddCardBoard (CardType.Trump, CardRank.Twelve);

		AddCardHand (CardType.Spade, CardRank.King);
		AddCardHand (CardType.Trump, CardRank.One);
		AddCardHand (CardType.Trump, CardRank.Eleven);
		
		for (int i = 0; i < player.Hand.Count; i++) 
			CheckPlayCard (i);
	}

	void Test5()
	{
		PrepareTest("Cannot coupe");
		
		AddCardBoard (CardType.Heart, CardRank.Six);
		AddCardBoard (CardType.Trump, CardRank.Twelve);

		AddCardHand (CardType.Spade, CardRank.King);
		AddCardHand (CardType.Club, CardRank.Ten);
		AddCardHand (CardType.Diamond, CardRank.Valet);
		
		for (int i = 0; i < player.Hand.Count; i++) 
			CheckPlayCard (i);
	}
	
	void Test6()
	{
		PrepareTest("Excuse first card");
		
		AddCardBoard (CardType.Excuse, CardRank.None);
		AddCardBoard (CardType.Heart, CardRank.Six);
		AddCardBoard (CardType.Trump, CardRank.Twelve);

		AddCardHand (CardType.Spade, CardRank.King);
		AddCardHand (CardType.Club, CardRank.Ten);
		AddCardHand (CardType.Diamond, CardRank.Valet);
		
		for (int i = 0; i < player.Hand.Count; i++) 
			CheckPlayCard (i);
	}
	
	void Test7()
	{
		PrepareTest("Excuse in hand");

		AddCardBoard (CardType.Heart, CardRank.Six);
		AddCardBoard (CardType.Trump, CardRank.Twelve);

		AddCardHand (CardType.Excuse, CardRank.None);
		AddCardHand (CardType.Spade, CardRank.King);
		AddCardHand (CardType.Club, CardRank.Ten);
		AddCardHand (CardType.Diamond, CardRank.Valet);
		
		for (int i = 0; i < player.Hand.Count; i++) 
			CheckPlayCard (i);
	}
	
	
	private void PrepareTest(String name)
	{
		Debug.Log("Start play test: " + name);
		boardsCard = new CardList();
		player.PrepareForNewHand();
	}

	private Card GetCard(CardType type, CardRank rank) = > Deck.GetCard(type, rank);
	
	private void AddCardHand(CardType type, CardRank, rank) 
	{ 
		Card card = GetCard(type, rank);
		player.Hand.Add (card); 
		Debug.Log("Add card to hand: " + card);
	}
	
	private void AddCardBoard(CardType type, CardRank, rank) 
	{ 
		Card card = GetCard(type, rank);
		boardsCard.Add (card); 
		Debug.Log("Add card to board: " + card);
	}
	
	
	private void CheckPlayCard (int handIndex)
	{
		bool canPlay = player.CanPlayCard(player.Hand[handIndex]);
		Debug.Log("Player can play " + card + ": " + canPlay);
	}
}
