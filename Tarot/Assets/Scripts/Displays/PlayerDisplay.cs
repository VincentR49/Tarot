using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class PlayerDisplay : MonoBehaviour {

    public Player player;
    public Text playerText;


	void Start ()
    {
        RefreshContent();
    }
	

    public void RefreshContent()
    {
        playerText.text = player.name;
    }


    public void SetPlayer(Player player)
    {
        this.player = player;
        RefreshContent();
    }
}
