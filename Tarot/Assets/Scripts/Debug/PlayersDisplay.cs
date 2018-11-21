using System.Collections;
using System.Collections.Generic;
using System.Text;
using UnityEngine;
using UnityEngine.UI;

public class PlayersDisplay : MonoBehaviour {

    public List<Text> texts;
    public PlayerList players;

    private void Update()
    {
        int nPlayer = players.Count;
        for (int i = 0; i < texts.Count; i++)
        {
            Player p = null;
            if (i < nPlayer)
            {
                p = players.Items[i];
            }
            texts[i].text = GetString(p);
        }
    }

    private static string GetString(Player player)
    {
        StringBuilder sb = new StringBuilder();
        if (player != null && player.GetHand() != null)
        {
            Hand hand = player.GetHand();
            sb.AppendLine(player.name + ":");
            sb.AppendLine("(" + hand.Count + " cards)");
            sb.AppendLine(player.GetHand().ToString());
        }
        return sb.ToString();
    }
}
