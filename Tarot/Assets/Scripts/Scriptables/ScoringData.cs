using System;
using System.Collections.Generic;
using System.Text;
using UnityEngine;

[CreateAssetMenu(menuName="Scriptable Objects/ScoringData")]
// A mettre � jour pour le d�compte des scores
public class ScoringData : ScriptableObject
{
	// Parameters ingame
	// Usefull to make tests
	public Bid bid = Bid.Garde;
	public float takerPoints = 42;
	public int nOudlerTaker = 2;
	public bool petitAuBout = false;
	public bool petitWithTaker = false;
	public bool chelemDone = false;
	public bool chelemAnnounced = false;
	public Poignee poignee1 = Poignee.None;
	public Poignee poignee2 = Poignee.None; // extremely rare
	
	// Properties (deduced from parameters)
	public float DefenderPoints => 91 - takerPoints;
	public bool TakerHasWon => takerPoints >= pointsRequiredPerOudler[nOudlerTaker];
	public bool PetitWithWinner => (TakerHasWon && petitWithTaker) || (!TakerHasWon && !petitWithTaker);
	public float BonusPoigneeTotal => bonusPoignee[poignee1] + bonusPoignee[poignee2];
	public float DeltaContract => (float) Math.Ceiling (Math.Abs (takerPoints - pointsRequiredPerOudler[nOudlerTaker]));

    // Will not change (core rules)
    private const float contractBasePoint = 25;
	private const float bonusPetitBout = 10;
	private const float bonusChelemAnnounced = 200; 
	private const float bonusChelemDone = 200;
	
	private Dictionary<Bid,float> bidMultiplicator = new Dictionary<Bid,float>
		{
			{ Bid.Prise, 1f },
			{ Bid.Garde, 2f },
			{ Bid.GardeSans, 4f },
			{ Bid.GardeContre, 6f },
		};
	
	private Dictionary<Poignee,float> bonusPoignee  = new Dictionary<Poignee,float>
		{
			{ Poignee.None, 0f },
			{ Poignee.Single, 20f },
			{ Poignee.Double, 30f },
			{ Poignee.Triple, 40f },
		};
	
	private Dictionary<int,float> pointsRequiredPerOudler = new Dictionary<int,float>
		{
			{ 0, 56f },
			{ 1, 51f },
			{ 2, 41f },
			{ 3, 36f },
		};
		
	
	public float GetWinnerBasePoints()
	{
		float petitPoint = petitAuBout ? bonusPetitBout : 0;
        petitPoint *= PetitWithWinner ? 1 : -1;
		float basePoints = contractBasePoint + DeltaContract + petitPoint;
		float chelemPoints = chelemDone ? bonusChelemDone : 0;
		chelemPoints += chelemAnnounced ? bonusChelemAnnounced : 0;
		float winerBasePoints = basePoints * bidMultiplicator[bid] + BonusPoigneeTotal + chelemPoints;
		return winerBasePoints;
	}


    public void PrintSummary()
    {
        Debug.LogFormat("ScoringData:\n" +
            "Taker won: {0}, by {1} points\n" +
            "Bid: {2}\n" +
            "Taker Points: {3}\n" + 
            "Defender Points: {4}\n" + 
            "Oudlers: {5}\n" + 
            "Petit au bout: {6}\n" + 
            "Petit with winner: {7}\n" + 
            "Chelem Done: {8}\n" + 
            "Chelem Announced: {9}\n" + 
            "Poign�es: {10}, {11}\n",
            TakerHasWon, DeltaContract, 
            bid, takerPoints, DefenderPoints, nOudlerTaker,
            petitAuBout, PetitWithWinner,
            chelemDone, chelemAnnounced,
            poignee1, poignee2);
    }
}