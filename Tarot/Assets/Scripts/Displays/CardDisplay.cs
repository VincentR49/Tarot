using UnityEngine;
using UnityEngine.UI;

// Gère l'affichage d'une carte
[RequireComponent(typeof(Image))]
public class CardDisplay : MonoBehaviour
{
	public Card card;
	public SpriteVariable cardBackSprite;
	private bool flipped = false;
	private Image image;
	
	
	private void Awake()
	{
		image = GetComponent<Image>();
	}	
	
	
	private void Start()
	{
		Debug.Log(card);
		UpdateSprite();
	}
	
	
	private void UpdateSprite()
	{
		if (flipped)
		{
			image.sprite = cardBackSprite.Value;
		}
		else
		{
			image.sprite = card.sprite;
		}
	}
	
	
	public void SetFlipped(bool flipped)
	{
		this.flipped = flipped;
		UpdateSprite();
	}
}