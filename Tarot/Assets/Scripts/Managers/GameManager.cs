using System;
using System.Collections;
using UnityEngine;


[RequireComponent(typeof(DealManager))]
[RequireComponent(typeof(PlayersGenerator))]
public class GameManager : MonoBehaviour
{
	public IntVariable nPlayer;
    private PlayersGenerator playersGenerator;
	private DealManager dealManager;
	
    private void Awake()
    {
        playersGenerator = GetComponent<PlayersGenerator>();
		dealManager = GetComponent<DealManager>();
    }

    private void Start()
    {
        NewGame(nPlayer.Value);
    }


    public void NewGame(int nPlayer)
	{
		Debug.Log("Start new Game with " + nPlayer + " players");
        playersGenerator.Generate(nPlayer);
		dealManager.SelectRandomDealer();
		dealManager.Deal();
		// Gestion du chien (dogManager ?)
		
		// Début du jeu
		
		
		// Gestion de la fin du jeu
	}
}