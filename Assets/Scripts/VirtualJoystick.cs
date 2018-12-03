using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.EventSystems;

public class VirtualJoystick : MonoBehaviour, IDragHandler, IPointerUpHandler, IPointerDownHandler {

	private Image bgImg;
	private Image joystickImg;
	private Vector3 inputVecor;
	// public PlayerController pc;
	public bool drag;
	public bool newUser;
	public int ii = 0;
	public int myindex;
	public List<GameObject> userlist = new List<GameObject>();
	public List<PlayerController> playList = new List<PlayerController>();
	public List<GameObject> particleList = new List<GameObject>();
	public InputField inputField;
	public GameObject inf, bt, back;
	public string userName;
	void Start()
	{
		bgImg = GetComponent<Image>();
		joystickImg = transform.GetChild(0).GetComponent<Image>();
		
	}

	public virtual void OnDrag(PointerEventData ped) {
		
		Vector2 pos;
		if (RectTransformUtility.ScreenPointToLocalPointInRectangle( bgImg.rectTransform
																	, ped.position
																	, ped.pressEventCamera
																	, out pos)) 
		{

			pos.x = (pos.x / bgImg.rectTransform.sizeDelta.x);
			pos.y = (pos.y / bgImg.rectTransform.sizeDelta.y);
			
			inputVecor = new Vector3((pos.x-0.5f) * 2 , 0 , (pos.y-0.5f) * 2 );
			inputVecor = (inputVecor.magnitude > 1.0f) ? inputVecor.normalized : inputVecor;
			// Debug.Log(inputVecor);
			// Move Joystick Img
			joystickImg.rectTransform.anchoredPosition = new Vector3(inputVecor.x * (bgImg.rectTransform.sizeDelta.x / 3)
																	,inputVecor.z * (bgImg.rectTransform.sizeDelta.y / 3));

			drag = true;
		}
	}
	public virtual void OnPointerDown(PointerEventData ped) {
		
		OnDrag(ped);
		
	}
	public virtual void OnPointerUp(PointerEventData ped) {
		
		inputVecor = Vector3.zero;
		joystickImg.rectTransform.anchoredPosition = Vector3.zero;
		drag = false;
	}
	
	public float Horizontal() {

		if(inputVecor.x != 0) {
			return inputVecor.x;
		}else {
			return Input.GetAxis("Horizontal");
		}
	}

	public float Vertical() {
		
		if(inputVecor.z != 0) {
			return inputVecor.z;
		}else {
			return Input.GetAxis("Vertical");
		}
	}

	// public void FindPlayer() {

	// 	// pc = GameObject.Find("Player(Clone)").GetComponent<PlayerController>();
	// 	pc = GameObject.FindObjectOfType<PlayerController>();
		
	// }

	public void cameraOff(int index) {

		for (int i = 0; i < userlist.Count; i++) {

			if (i==index-1) {
				continue;
			}
			if (userlist[i] != null) {

				userlist[i].transform.GetChild(1).gameObject.SetActive(false);
			}
			
		}
		newUser = false;
	}

	public int getIndex() {
		return ii;
	}

	public void connectUser() {
		ii++;
		newUser = true;
	}

	public void clickSaveButton() {

		// PlayerPrefs.SetString("name",inputField.text );
		// PlayerPrefs.Save();
		userName = inputField.text;
		offui();
	}

	public void offui() {
		inf.SetActive(false);
		bt.SetActive(false);
		back.SetActive(false);
		
	}

	public void showParticle(int userindex) {

		if (userlist[userindex-1] != null ) {
			userlist[userindex-1].transform.GetChild(3).gameObject.SetActive(true);
		}
		
	}

	public void hideParticle(int userindex) {

		if (userlist[userindex-1] != null ) {
			userlist[userindex-1].transform.GetChild(3).gameObject.SetActive(false);
		}
	}

	public void particleShow(List<int> pushlist) {
	
		for(int i=0; i<particleList.Count; i++) {

			particleList[i].SetActive(false);

			for(int j=0; j<pushlist.Count; j++) {

				if (i == pushlist[j]-1 ) {
					particleList[i].SetActive(true);
				}
			}
		}
	}

	// void Update()
	// {
	// 	if (playList != null) {

	// 		for(int i=0; i<playList.Count; i++) {

	// 			if (playList[i].shooting) {
	// 				userlist[i].transform.GetChild(3).gameObject.SetActive(true);
	// 			}else {
	// 				userlist[i].transform.GetChild(3).gameObject.SetActive(false);
	// 			}
	// 		}
	// 	}
		
	// }

	

}
