using UnityEngine;
using UnityEngine.UI;

public class BidButtonsDisplay : CanvasGroupDisplay
{
    public PlayerList players;
    public Button passButton;
    public Button priseButton;
    public Button gardeButton;
    public Button gardeSansButton;
    public Button gardeContreButton;

    private new void Awake()
    {
        base.Awake();
        Hide();
    }

    private void Start()
    {
        passButton.onClick.AddListener(() => SetBid(Bid.Pass));
        priseButton.onClick.AddListener(() => SetBid(Bid.Prise));
        gardeButton.onClick.AddListener(() => SetBid(Bid.Garde));
        gardeSansButton.onClick.AddListener(() => SetBid(Bid.GardeSans));
        gardeContreButton.onClick.AddListener(() => SetBid(Bid.GardeContre));
    }


    private void SetBid(Bid bid)
    {
        Player player = players.GetFirstHumanPlayer();
        player.SetBid(bid);
        Hide();
    }


    public override void Show()
    {
        EnableButtons();
        base.Show();
    }

    private void EnableButtons()
    {
        Bid maxBid = GetCurrentMaxBid();
        passButton.interactable = true;
        priseButton.interactable = maxBid < Bid.Prise;
        gardeButton.interactable = maxBid < Bid.Garde;
        gardeSansButton.interactable = maxBid < Bid.GardeSans;
        gardeContreButton.interactable = maxBid < Bid.GardeContre;
    }


    private Bid GetCurrentMaxBid()
    {
        Bid maxBid = Bid.None;
        foreach (Player p in players.Items)
        {
            if (p.CurrentBid > maxBid)
            {
                maxBid = p.CurrentBid;
            }
        }
        return maxBid;
    }
}
