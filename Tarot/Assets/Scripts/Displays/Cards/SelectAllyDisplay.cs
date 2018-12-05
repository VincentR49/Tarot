using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

// Gère l'affichage du choix de l'allié pour le jeu à 5 joueurs
public class SelectAllyDisplay : MonoBehaviour
{
    public PlayerList players;
	public GameObject cardPrefab;
	public GamePhaseVariable gamePhase;

	private Player Taker => players.GetTaker();
	private List<GameObject> cards;
	private boolean buttonsShown;
	
	
    private void Awake()
    {
       text = GetComponent<Text>();
	   image = GetComponent<Image>();
	   cards = new List<GameObject>();
    }


    private void Update()
    {
		if (gamePhase.Value == GamePhase.SelectAlly)
		{
			if (cards.Count == 0)
			{
				GenerateCards();
			}	
		}
		else if (cards.Count > 0)
		{
			Clean();
		}
    }
		
	public void GenerateCards()
	{	
		// TODO
	}
	
		
	void Clean()
	{
		if (cards != null)
        {
            foreach (GameObject go in cards)
            {
                Destroy(go);
            }
        }   
        cards = new List<GameObject>();
	}
}