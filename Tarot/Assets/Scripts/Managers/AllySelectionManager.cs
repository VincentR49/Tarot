using System.Collections;
using System.Collections.Generic;
using UnityEngine;

// Sélection du joueur allié au taker
// Pour le jeu à 5 uniquement
public class AllySelectionManager : ProcessManager 
{
    protected override string Name => "Ally Selection";
	public PlayerList players;
    public CardVariable allyCard;
    public CardListVariable selectedCard;
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
                if (selectedCard.Count >= 1)
                {
                    ChooseCalledCard(selectedCard.Value[0]);
                }
			}
		}
	}
	
	
	public override void StartProcess()
	{
		base.StartProcess();
        selectedCard.Clear();
		timer = 0f;
        if (Taker is CpuPlayer)
		{
            CpuPlayer cpuPlayer = (CpuPlayer) Taker;
			ChooseCalledCard (cpuPlayer.SelectCalledCard(standardDeck));
		}
	}


	private void ChooseCalledCard(Card card)
	{
		Debug.Log("Choose ally card: " + card);
        allyCard.Value = card;
        Debug.Log("Ally card: " + allyCard);
		FinishProcess();
	}
}
