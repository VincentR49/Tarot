using System;
using System.Collections;
using UnityEngine;


[RequireComponent(typeof(PlayersGenerator))]
public class GameManager : MonoBehaviour
{
	// Library variables (fixed scriptable objects)
	[Tooltip("Reference to the full standard deck")]
	public Deck standardDeck;
	
	// Runtime variables
	[Tooltip("Reference to the current players")]
	public PlayerList players;
	[Tooltip("Reference to the current game deck")]
	public Deck deck;
	[Tooltip("Reference to the current game dog")]
	public Dog dog;

	public IntVariable nPlayer;
	private Player dealer;
	private Player taker;

    private PlayersGenerator playersGenerator;

    private void Awake()
    {
        playersGenerator = GetComponent<PlayersGenerator>();
    }

    private void Start()
    {
        NewGame(nPlayer.Value);
    }


    public void NewGame(int nPlayer)
	{
        playersGenerator.Generate(nPlayer); ;
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


	// TODO faire un DealManager
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