using System;
using UnityEngine;

[CreateAssetMenu(menuName="Scriptable Objects/ScoringData")]
// A mettre à jour pour le décompte des scores
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
	
	// Will not change (core rules)
	private static const float contractBasePoint = 25;
	private static const float bonusPetitBout = 10;
	private static const float bonusChelemAnnounced = 200; 
	private static const float bonusChelemDone = 200;
	
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
			{ Poignee.Simple, 20f },
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
		float pointsNeeded = pointsRequiredPerOudler[nOudlerTaker];
		float deltaContract = Math.Ceil(Math.Abs(takerPoints - pointsNeeded)); // arrondi supérieur
		float petitPoint = petitAuBout ? bonusPetitBout : 0;
		petitPoints *= (PetitWithWinner) 1 : -1;
		float basePoints = contractBasePoint + deltaContract + petitPoints;
		float chelemPoints = chelemDone ? bonusChelemDone : 0;
		chelemPoints += chelemAnnounced ? bonusChelemAnnounced : 0;
		float winerBasePoints = basePoints * bidMultiplicator[bid] + BonusPoigneeTotal + chelemPoints;
		return winerBasePoints;
	}
}