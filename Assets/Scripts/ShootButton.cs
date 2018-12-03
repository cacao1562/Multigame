using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.EventSystems;
using UnityEngine.UI;

public class ShootButton : MonoBehaviour, IPointerDownHandler, IPointerUpHandler {

	public bool shooting;
	public Color blue, yellow;
	private Image selfImg;


	void Start()
	{
		selfImg = transform.GetComponent<Image>();
		
	}

	public virtual void OnPointerDown(PointerEventData eventData) {
	
		shooting = true;
		selfImg.color = yellow;
	}

	public virtual void OnPointerUp(PointerEventData eventData) {

		shooting = false;
		selfImg.color = blue;
	}
}
