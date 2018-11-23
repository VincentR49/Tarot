using System.Collections;
using System.Collections.Generic;
using UnityEngine;

// Met à jour la liste des joueurs
// Réinitialise les scores des joueurs
public class PlayersGenerator : ProcessManager 
{
	protected override string Name => "Players Creation";
    [Tooltip("Reference to the full bank of players")]
    public PlayerList playersBank;
    [Tooltip("Reference to the current players")]
    public PlayerList players;
    public IntVariable nPlayer;
	
    public override void StartProcess()
    {
		base.StartProcess();
        players.Clear();
        GeneratePlayers();
		PreparePlayersForNewGame();
		FinishProcess();
    }
	
	
	private void GeneratePlayers()
	{
		// le human player est ajouté en premier
		Player hPlayer = playersBank.GetFirstHumanPlayer();
		if (hPlayer != null)
		{
			players.Add(hPlayer);
			Debug.Log("Add human Player " + hPlayer.name);
		}
		
		foreach (Player p in playersBank.Items)
		{
			// A voir si on les ajoute aléatoirement pour la suite ...
            if (players.Count >= nPlayer.Value)
            {
                break;
            }
            if (!players.Items.Contains(p))
            {
                players.Add(p);
                Debug.Log("Add cpu Player " + p.name);
            }
		}
	}
	
	// Initialisation des différents joueurs
	private void PreparePlayersForNewGame()
	{
		foreach (Player p in players.Items)
		{
			p.PrepareForNewGame();
		}
	}
}
