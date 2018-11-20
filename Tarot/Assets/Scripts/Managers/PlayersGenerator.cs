using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayersGenerator : MonoBehaviour {

    [Tooltip("Reference to the full pool of players")]
    public PlayerList playerFullList;
    
    [Tooltip("Reference to the current players")]
    public PlayerList players;

    public GameObject mainBoard;
    private List<GameObject> cpuBoards;

    private int nPlayer;

    private void Start()
    {
        
    }


    public void Generate(int nPlayer)
    {
        players.Clear();
        this.nPlayer = nPlayer;
        Player humanPlayer = null;
        foreach (Player p in playerFullList.Items)
        {
            if (p is HumanPlayer)
            {
                humanPlayer = p;
                players.Add(humanPlayer);
                break;
            }
        }
       
        while (players.Items.Count < nPlayer)
        {
            foreach (Player p in playerFullList.Items)
            {
                players.Add(p); // n'ajoute pas les doublons
            }
        }
        UpdateBoards();
    }


    private void UpdateBoards()
    {
        Debug.Log("UpdateBoards");
        if (nPlayer <= 0)
        {
            Debug.Log("No players");
            return;
        }
    }


    static Vector2 GetBoardPosition(int playerIndex, int nPlayer)
    {
        return new Vector2(0, 0);
    }

}
