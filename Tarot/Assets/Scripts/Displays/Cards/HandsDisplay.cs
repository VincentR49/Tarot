using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class HandsDisplay : MonoBehaviour {

    public PlayerList players;
    public GameObject handPrefab;
    public List<GameObject> placeHolders;
    private List<GameObject> handContainers;

	public void GenerateHandContainers()
    {
        Clean();
        int nPlayer = players.Count;
        for (int i = 0; i < placeHolders.Count; i++)
        {
            if (i < nPlayer)
            {
                GenerateHandContainer(i);
            }
        }
    }


    private void GenerateHandContainer(int index)
    {
        GameObject go = Instantiate(handPrefab, placeHolders[index].transform);
        HandDisplay handDisplay = go.GetComponent<HandDisplay>();
        handDisplay.player = players.Items[index];
        handContainers.Add(go);
    }


    private void Clean()
    {
        if (handContainers != null)
        {
            foreach (GameObject go in handContainers)
            {
                Destroy(go);
            }
        }
        handContainers = new List<GameObject>();
    }
}
