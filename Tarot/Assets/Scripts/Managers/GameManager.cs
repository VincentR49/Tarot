using System;
using System.Collections;
using UnityEngine;


[RequireComponent(typeof(DealManager))]
[RequireComponent(typeof(PlayersGenerator))]
public class GameManager : MonoBehaviour
{
    private PlayersGenerator playersGenerator;
	private DealManager dealManager;
    public IntVariable nPlayer;

    private void Awake()
    {
        playersGenerator = GetComponent<PlayersGenerator>();
		dealManager = GetComponent<DealManager>();
    }

    private void Start()
    {
        NewGame();
    }

    public void NewGame(int nPlayer)
    {
        this.nPlayer.Value = nPlayer;
        NewGame();
    }

    public void NewGame()
	{
        if (nPlayer.Value < 3 || nPlayer.Value > 5)
        {
            Debug.Log("Wrong Players number: should be 3, 4 or 5");
            return;
        }
        playersGenerator.Generate();
		dealManager.SelectRandomDealer();
		dealManager.DealCards();
		// Gestion du chien (dogManager ?)
		
		// Début du jeu
		
		
		// Gestion de la fin du jeu
	}
}