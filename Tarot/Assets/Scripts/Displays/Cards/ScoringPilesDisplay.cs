using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ScoringPilesDisplay : MonoBehaviour {

    public PlayerList players;
    public GameObject scoringPilePrefab;
    public List<GameObject> placeHolders;
    private List<GameObject> scoringPiles;

    public void GenerateScoringPiles()
    {
        Clean();
        int nPlayer = players.Count;
        for (int i = 0; i < placeHolders.Count; i++)
        {
            if (i < nPlayer)
            {
                GenerateScoringPile(i);
            }
        }
    }


    private void GenerateScoringPile(int index)
    {
        GameObject go = Instantiate(scoringPilePrefab, placeHolders[index].transform);
        go.transform.localPosition = Vector3.zero;
        ScoringPileDisplay scoringPileDisplay = go.GetComponent<ScoringPileDisplay>();
        scoringPileDisplay.SetPlayer (players.Items[index]);
        scoringPiles.Add(go);
    }


    private void Clean()
    {
        if (scoringPiles != null)
        {
            foreach (GameObject go in scoringPiles)
            {
                Destroy(go);
            }
        }
        scoringPiles = new List<GameObject>();
    }
}
