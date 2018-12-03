using System.Collections;
using System.Collections.Generic;
using UnityEngine;

// Gestion du système de jeu
public class TestBestCardOnBoard : MonoBehaviour 
{
	public Player player;
	public Deck standardDeck; 
	private CardList boardCardss;
	
	
	void Start()
	{
		player.PrepareForNewGame();
		Test1();
		Test2();
		Test3();
		Test4();
		Test5();
		Test6();
	}
	
	
	void Test1()
	{
		PrepareTest("Normal color");
		
		AddCardBoard (CardType.Heart, CardRank.Six);
		AddCardBoard (CardType.Heart, CardRank.Roi);
		AddCardBoard (CardType.Heart, CardRank.One);
		AddCardBoard (CardType.Heart, CardRank.Valet);
		
		CheckBestCard();
	}
	
	void Test2()
	{
		PrepareTest("Different colors");
		
		AddCardBoard (CardType.Heart, CardRank.Six);
		AddCardBoard (CardType.Club, CardRank.Roi);
		AddCardBoard (CardType.Diamond, CardRank.One);
		AddCardBoard (CardType.Spade, CardRank.Valet);
		
		CheckBestCard();
	}
	
	void Test3()
	{
		PrepareTest("Excuse");
		
		AddCardBoard (CardType.Excuse, CardRank.None);
		AddCardBoard (CardType.Heart, CardRank.Dame);
		AddCardBoard (CardType.Diamond, CardRank.Roi);
		AddCardBoard (CardType.Heart, CardRank.Valet);
		
		CheckBestCard();
	}
	
	void Test4()
	{
		PrepareTest("Excuse2");
		
		AddCardBoard (CardType.Heart, CardRank.Dame);
		AddCardBoard (CardType.Excuse, CardRank.None);
		AddCardBoard (CardType.Heart, CardRank.Roi);
		AddCardBoard (CardType.Heart, CardRank.Valet);
		
		CheckBestCard();
	}
	
	void Test5()
	{
		PrepareTest("Coupe");
		
		AddCardBoard (CardType.Heart, CardRank.Dame);
		AddCardBoard (CardType.Trump, CardRank.Two);
		AddCardBoard (CardType.Trump, CardRank.One);
		AddCardBoard (CardType.Heart, CardRank.Valet);
		
		CheckBestCard();
	}
	
	void Test6()
	{
		PrepareTest("Trump starting");
		
		AddCardBoard (CardType.Trump, CardRank.Two);
		AddCardBoard (CardType.Heart, CardRank.Dame);
		AddCardBoard (CardType.Trump, CardRank.Ten);
		AddCardBoard (CardType.Heart, CardRank.Valet);
		
		CheckBestCard();
	}
	
	private void PrepareTest(String name)
	{
		Debug.Log("Start check best card board test: " + name);
		boardCards = new CardList();
	}

	private Card GetCard(CardType type, CardRank rank) = > Deck.GetCard(type, rank);

	private void AddCardBoard(CardType type, CardRank, rank) 
	{ 
		Card card = GetCard(type, rank);
		boardCards.Add (card); 
		Debug.Log("Add card to board: " + card);
	}
	
	private void CheckBestCard ()
	{
		Card card = boardCards.GetBestCard();
		Debug.Log("Best card: " + card);
	}
}
