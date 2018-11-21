using System.Collections;
using System.Collections.Generic;
using UnityEngine;

// Met à jour la liste des joueurs
// Réinitialise les scores des joueurs
public class PlayersGenerator : MonoBehaviour {

    [Tooltip("Reference to the full bank of players")]
    public PlayerList playersBank;
    
    [Tooltip("Reference to the current players")]
    public PlayerList players;

    private int nPlayer;
	

    public void Generate(int nPlayer)
    {
		Debug.Log("Player generation: " + nPlayer);
		this.nPlayer = nPlayer;
        players.Clear();
        GeneratePlayers();
		PreparePlayersForNewGame();
    }
	
	
	private void GeneratePlayers()
	{
		if (playersBank.Count < nPlayer)
		{
			Debug.Log("Not enought pre-set players in the current bank to start the game");
			return;
		}
		// le human player est ajouté en premier
		HumanPlayer hPlayer = playersBank.GetFirstHumanPlayer();
		if (hPlayer != null)
		{
			players.Add(p);
			Debug.Log("Add human Player " + p.name);
		}
		while (players.Count < nPlayer)
		{
			foreach (Player p in playersBank.Items)
			{
				// A voir si on les ajoute aléatoirement pour la suite ...
				players.Add(p); // n'ajoute pas les doublons (voir Add)
				Debug.Log("Add cpu Player " + p.name);
			}
		}
	}
	
	// Initialisation des différents joueurs
	private void PreparePlayersForNewGame()
	{
		foreach (Player p in players)
		{
			p.PrepareForNewGame();
		}
	}
}
