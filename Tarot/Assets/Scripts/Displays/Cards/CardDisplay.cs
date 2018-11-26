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
    public float rotateDegreePerSec = 180f;

    private bool flipped = false;
    private static float animationFPS = 60;
	private Image image;
    private bool isAnimationProcessing = false;
    private bool flippedBeforeAnimation = false;

	private void Awake()
	{
		image = GetComponent<Image>();
	}	
	
	
	private void Start()
	{
		UpdateSprite();
	}


    public void OnPointerDown(PointerEventData eventData)
    {
        FlipCard();
    }

    public void UpdateSprite()
    {
        UpdateSprite(flipped);
    }
        

    public void UpdateSprite(bool flipped)
	{
	    image.sprite = flipped ? cardBackSprite.Value : card.sprite;	
	}
	

    public void FlipCard()
    {
        if (!isAnimationProcessing)
        {
            flippedBeforeAnimation = flipped;
            StartCoroutine(FlipAnimation());
        } 
    }

	
	public void SetFlipped(bool flipped)
	{
        if (flipped != this.flipped)
        {
            this.flipped = flipped;
            UpdateSprite(flipped);
        }
	}


    IEnumerator FlipAnimation()
    {
        //Debug.Log("Start flip animation");
        isAnimationProcessing = true;
        bool done = false;
        if (flipped)
        {
            transform.Rotate(new Vector3(0, 180, 0));
        }
        float initAngle = transform.rotation.eulerAngles.y;
        while (!done)
        {
            float degree = rotateDegreePerSec * Time.deltaTime;
            transform.Rotate(new Vector3(0, degree, 0));
            float angle = transform.rotation.eulerAngles.y - initAngle;
            if (flippedBeforeAnimation == flipped)
            {
                if (angle > 90)
                {
                    SetFlipped(!flipped);
                }
            }
            if (angle > 180 || angle < 0)
            {
                transform.rotation = Quaternion.identity;
                done = true;
            }
            yield return new WaitForSeconds(1.0f / animationFPS);
        }
        isAnimationProcessing = false;
        //Debug.Log("Stop flip animation");
    }
}