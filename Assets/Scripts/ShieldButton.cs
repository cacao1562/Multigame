using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.EventSystems;
using UnityEngine.UI;

public class ShieldButton : MonoBehaviour, IPointerDownHandler, IPointerUpHandler {

	public bool shielding;
	public Color red, green;
	private Image selfImg;


	void Start()
	{
		selfImg = transform.GetComponent<Image>();
		
	}

	public virtual void OnPointerDown(PointerEventData eventData) {
	
		shielding = true;
		selfImg.color = green;
	}

	public virtual void OnPointerUp(PointerEventData eventData) {

		shielding = false;
		selfImg.color = red;
	}
}
