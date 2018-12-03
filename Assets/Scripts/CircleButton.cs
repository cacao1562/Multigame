using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.EventSystems;
using UnityEngine.UI;

public class CircleButton : MonoBehaviour, IPointerDownHandler, IPointerUpHandler {

	public bool spinning;
	public Color green, purple;
	private Image selfImg;


	void Start()
	{
		selfImg = transform.GetComponent<Image>();
		
	}

	public virtual void OnPointerDown(PointerEventData eventData) {
	
		spinning = true;
		selfImg.color = purple;
	}

	public virtual void OnPointerUp(PointerEventData eventData) {

		spinning = false;
		selfImg.color = green;
	}
}
