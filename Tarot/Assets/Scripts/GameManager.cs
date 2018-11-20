using System;
using System.Collections;
using UnityEngine;

// Gestion d'une partie
// Player 1 dans la liste est le joueur courant
public class GameManager : MonoBehaviour
{
	// Library variables (fixed scriptable objects)
	[Tooltip("Reference to the full standard deck")]
	public Deck standardDeck;
	[Tooltip("Reference to the full pool of players")]
	public PlayerList playerFullList;
	
	// Runtime variables
	[Tooltip("Reference to the current players")]
	public PlayerList players;
	[Tooltip("Reference to the current game deck")]
	public Deck deck;
	[Tooltip("Reference to the current game dog")]
	public Dog dog;
	
	private int nPlayer = 3;
	private Player dealer;
	private Player taker;
	
	
	public void NewGame(int nPlayer)
	{
		this.nPlayer = nPlayer;
		PreparePlayers();
	}
	
	
	public void NewHand()
	{
		// TODO
		PrepareDeck();
	}
	
	
	public void FinishGame()
	{
		// TODO
	}
	
	
	private void PrepareDeck()
	{
		deck = Instantiate(standardDeck);
		deck.Shuffle();
	}
	
	
	private void PreparePlayers()
	{
		// Player 0 is always the NonAI player
		// If only AI players, take the first one
		players.Clear();
		Player humanPlayer = null;
		foreach (Player p in players)
		{
			if (p.IsHuman())
			{
				humanPlayer = p;
				break;
			}
		}
		if (humanPlayer != null)
		{
			players.Add(humanPlayer);
		}
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