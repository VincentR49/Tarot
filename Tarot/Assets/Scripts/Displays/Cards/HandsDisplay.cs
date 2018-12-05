using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class HandsDisplay : MonoBehaviour {

    public PlayerList players;
    public GameObject handPrefab;
    public List<GameObject> placeHolders;
    private List<GameObject> hands;

	public void GenerateHands()
    {
        Clean();
        int nPlayer = players.Count;
        for (int i = 0; i < placeHolders.Count; i++)
        {
            if (i < nPlayer)
            {
                GenerateHand(i);
            }
        }
    }


    private void GenerateHand(int index)
    {
        GameObject go = Instantiate(handPrefab, placeHolders[index].transform);
        go.transform.localPosition = Vector3.zero;
        HandDisplay handDisplay = go.GetComponent<HandDisplay>();
        handDisplay.player = players.Items[index];
        hands.Add(go);
    }


    private void Clean()
    {
        if (hands != null)
        {
            foreach (GameObject go in hands)
            {
                Destroy(go);
            }
        }
        hands = new List<GameObject>();
    }
}
