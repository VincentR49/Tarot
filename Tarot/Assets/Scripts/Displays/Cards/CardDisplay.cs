using System;
using System.Collections;
using UnityEngine;
using UnityEngine.EventSystems;
using UnityEngine.UI;

// Gère l'affichage d'une carte
[RequireComponent(typeof(Image))]
public class CardDisplay : MonoBehaviour, IPointerDownHandler
{
	public Card card;
	public SpriteVariable cardBackSprite;
    public float flipAnimationSec = 1f;
    private float RotateDegreePerSec =>  180 / flipAnimationSec;
    private bool flipped = false;
	private Image image;
    private bool isFlipAnimation = false;
    private float initAnimationAngle = 0f;
    private Sprite GetCardSprite(bool flipped) => flipped ? cardBackSprite.Value : card.sprite;


    private void Awake()
	{
		image = GetComponent<Image>();
	}	
	
	
	private void Start()
	{
		UpdateSprite();
	}


    private void Update()
    {
        if (isFlipAnimation)
        {
            FlipAnimation();
        }
    }

    public void OnPointerDown(PointerEventData eventData)
    {
        FlipCardAnimated();
    }

    public void UpdateSprite()
    {
        image.sprite = GetCardSprite(flipped);
    }
        

    public void FlipCardAnimated()
    {
        if (!isFlipAnimation)
        {
            if (flipped)
            {
                transform.Rotate(new Vector3(0, 180, 0));
            }
            initAnimationAngle = transform.rotation.eulerAngles.y;
            FlipAnimation();
        } 
    }

	
	public void SetFlipped(bool flipped)
	{
        if (flipped != this.flipped)
        {
            this.flipped = flipped;
            UpdateSprite();
        }
	}


    void FlipAnimation()
    {
        isFlipAnimation = true;
        float degree = RotateDegreePerSec * Time.deltaTime;
        transform.Rotate(new Vector3(0, degree, 0));
        float angle = transform.rotation.eulerAngles.y - initAnimationAngle;
        if (angle > 90 && image.sprite != GetCardSprite(!flipped))
        {
            image.sprite = GetCardSprite(!flipped);
        }
        if (angle > 180 || angle < 0)
        {
            transform.rotation = Quaternion.identity;
            isFlipAnimation = false;
            SetFlipped(!flipped);
        }
    }
}