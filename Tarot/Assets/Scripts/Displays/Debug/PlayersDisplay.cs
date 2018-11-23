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
        if (player != null)
        {
            string suffix = "";
            if (player.IsDealer)
            {
                suffix += " (D)";
            }
            if (player.IsTaker)
            {
                suffix += " (T)";
            }
            sb.AppendLine(player.name + suffix);
            sb.AppendLine("Bid :" + player.CurrentBid);
            /*
            if (player.Hand != null)
            {
                sb.AppendLine("(" + player.Hand.Count + " cards)");
                sb.AppendLine(player.Hand.ToString());
            }
            */
        }
        return sb.ToString();
    }
}
