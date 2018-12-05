using System.Collections;
using System.Collections.Generic;
using UnityEngine;

// Sélection du joueur allié au taker
// Pour le jeu à 5 uniquement
public class AllySelectionManager : ProcessManager 
{
    protected override string Name => "Ally Selection";
	public PlayerList players;
	public Card calledCard;
	public Deck standardDeck;
	public float limitAnswerTimeSec = 30f;
	
	private Player Taker => players.GetTaker();
	private float timer;
	
	private void Update()
	{
		if (status == ProcessState.Running)
		{
			if (Taker is HumanPlayer)
			{
				timer += Time.deltaTime;
				if (timer >= limitAnswerTimeSec)
				{
					Card card = standardDeck.GetCard(CardType.Heart, CardRank.Roi);
					ChooseCalledCard (card);
				}
			}
		}
	}
	
	
	public override void StartProcess()
	{
		base.StartProcess();
		calledCard = null; // to be sure
		timer = 0f;
		if (Taker is CpuPlayer)
		{
			Card card = (CpuPlayer) Taker.SelectCalledCard();
			ChooseCalledCard (card);
		}
	}
	
	private void ChooseCalledCard(Card card)
	{
		Debug.Log("Choose called card: " + card);
		calledCard = card;
		FinishProcess();
	}
}
