using System;
using System.Collections;
using UnityEngine;

// Gestion d'une partie
// Player 1 dans la liste est le joueur courant
public class GameManager : MonoBehaviour
{
	public int nPlayer = 3;
	
	[Tooltip("Reference to the full standard deck")]
	public Deck standardDeck;
	
	[Tooltip("Reference to the current player List")]
	public PlayerList playerList;
	
	
	// runtime references
	[Tooltip("Reference to the current game deck")]
	public Deck deck;
	[Tooltip("Reference to the current game dog")]
	public Dog dog;
	
	public Player dealer;
	
	private void Awake()
	{
	
	}
	
	
	private void Start()
	{
	
	}
	
	
	public void NewGame(int nPlayer)
	{
		this.nPlayer = nPlayer;
		ResetDeck();
		ResetHands();
	}
	
	
	private void ResetDeck()
	{
		deck = Instantiate(standardDeck);
		deck.Shuffle();
	}
	
	
	private void ResetHands()
	{
		
	}
	
	
	public void Deal()
	{
		if (deck.Items.Count != standardDeck.Items.Count)
		{
			Debug.Log("Le deck courant n'a pas le bon nombre de carte");
		}
		else
		{
			
		}
	}
}