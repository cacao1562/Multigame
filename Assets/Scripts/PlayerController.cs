using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Networking;
using UnityEngine.UI;



public class PlayerController : NetworkBehaviour {

	public VirtualJoystick joystick;
	public int index;
	
	public GameObject selfParticle;
	private ShootButton shootButton;
	private ShieldButton shieldButton;
	private CircleButton circleButton;
	// public bool shooting;
	// public Text tt2, tt3;

	public Transform ballPos;

	// [SyncVar]
	// public SyncListInt pushIndex = new SyncListInt();
	[SyncVar (hook = "getSync")]
	public string syncStr;
	[SyncVar (hook = "getShield")]
	public string syncShield;
	[SyncVar (hook = "getSpin")]
	public string syncSpin;
	[SyncVar (hook = "setUserName")]
	public string syncUserName;
	public ParticleSystem ps;
	private Rigidbody rb;
	public Camera selfCamera;
	public TextMesh selfTextMesh;
	public GameObject enemy;
	public GameObject shield;
	public GameObject spinCube;
	void Awake()
	{
		// pushIndex.Callback = OnchangeIndex;
	
	}


	
	void Start()
	{
		
		Screen.sleepTimeout = SleepTimeout.NeverSleep;
		// transform.position = new Vector3(0,-1,0);
		// selfParticle = transform.GetChild(3).gameObject;
		// selfParticle = transform.parent.GetChild(1).gameObject;
		rb = GetComponent<Rigidbody>();
		// tt2 = GameObject.Find("Text2").GetComponent<Text>();
		// tt3 = GameObject.Find("Text3").GetComponent<Text>();
		shootButton = GameObject.Find("ShootButton").GetComponent<ShootButton>();
		shieldButton = GameObject.Find("ShieldButton").GetComponent<ShieldButton>();
		circleButton = GameObject.Find("CircleButton").GetComponent<CircleButton>();
		joystick = GameObject.Find("Background").GetComponent<VirtualJoystick>();
		joystick.connectUser();
		index = joystick.getIndex();
		joystick.myindex = index;

		if (isLocalPlayer) {
			CmdgetName();
			// StartCoroutine( showEnemy() );
		}
		

	}
	
	// IEnumerator showEnemy() {

	// 	for (int i=0; i<150; i++) {
	// 		float x = Random.Range(-23f, 23f);
	// 		float z = Random.Range(-27f, 27f);
	// 		Vector3 v = new Vector3(x, 0f , z);
	// 		GameObject e = Instantiate(enemy, v , Quaternion.identity);
	// 		e.transform.LookAt(Vector3.zero);
	// 		// e.GetComponent<Rigidbody>().AddForce(e.transform.forward * 10f);
	// 		e.GetComponent<Rigidbody>().velocity = e.transform.forward * 16f;
	// 		NetworkServer.Spawn(e);
	// 		Destroy(e, 5f);
	// 		yield return new WaitForSeconds(3f);
	// 	}
	// }

	[Command]
	void CmdgetName() {
		// syncUserName = PlayerPrefs.GetString("name", index + " player" );
		if (joystick.userName == "") {
			syncUserName = "player " + index;
			return;
		}
		syncUserName = joystick.userName;
	}
	
	
	[Command]
	 void CmdaddIndex() {
		syncStr = "start";
		// GameObject b = Instantiate(ball, ballPos.position, ballPos.rotation);
		// b.GetComponent<Rigidbody>().velocity = ballPos.forward * 6f;
		// NetworkServer.Spawn(b);
		// Destroy(b, 2f);
	
	}

	[Command]
	 void CmdremovedIndex() {
		syncStr = "stop";
	
		
	}

	[Command]
	void CmdshowShield() {
		syncShield = "show";
	}

	[Command]
	void CmdhideShield() {
		syncShield = "hide";
	}

	[Command]
	void CmdshowCube() {
		syncSpin = "show";
	}
	
	[Command]
	void CmdhideCube() {
		syncSpin = "hide";
	}

	void setDepth() {
		selfCamera.depth = 1;
		depthCheck = false;
	}
	private bool depthCheck = true;
	void Update () {

	
		if (!isLocalPlayer) { //로컬 플레이어가 아닐때 (다른유저일때)
			
			return;
		}
		if (depthCheck) {
			 setDepth();
		}
		
		
		// if (joystick.newUser) { //새로운 유저 접속했을때
		// 	// joystick.cameraOff(index);
		// }
		

		if (shootButton.shooting) {

			CmdaddIndex();
		
		}else {
			
			CmdremovedIndex();
			
		}

		if (shieldButton.shielding) {

			CmdshowShield();
		
		}else {
			
			CmdhideShield();
			
		}

		if (circleButton.spinning) {
			
			CmdshowCube();

		}else {

			CmdhideCube();
		}



	

		if (joystick.drag) {
		
		float x = joystick.Horizontal();
		float z = joystick.Vertical();
		x *= Time.deltaTime * 94f;
		z *= Time.deltaTime * 9f;
		// var x = Input.GetAxis("Horizontal") * Time.deltaTime * 150.0f; 
		// var z = Input.GetAxis("Vertical") * Time.deltaTime * 3.0f;
 
		transform.Rotate(0f, x, 0f);
		// rb.velocity = transform.forward * 9f;
		// rb.AddRelativeForce(transform.forward * z, ForceMode.VelocityChange);
		transform.Translate(0f, 0f, z);
		float xx = transRadian(90f);
		float yy = transRadian(0f);
		ps.startRotation3D = new Vector3(xx,yy,0f);
		// Vector3 v3 = transform.forward;
		// v3.x += z;
		// v3.Normalize();
		// rb.velocity = v3;
		// Vector3 vv = new Vector3(x,0,0);
		// Quaternion qq = Quaternion.LookRotation(vv);
		
		
		// rb.AddForce( v3, ForceMode.VelocityChange );
		// rb.MovePosition(rb.position + v3 );
		// rb.rotation = Quaternion.Slerp(transform.rotation, qq, Time.deltaTime * 5);
		}
		
	}



	public List<int> iList = new List<int>();
	
	string zz;

	// private void OnchangeIndex(SyncListInt.Operation op, int i) {
	// 	// Debug.Log("OOOOOOOO = " + op + "\n  , " + i );

	// 	if (!isLocalPlayer) {
	// 		if (op == SyncListInt.Operation.OP_ADD) {
	// 			transform.GetChild(3).gameObject.SetActive(true);
	// 			return;
	// 		}else if (op == SyncListInt.Operation.OP_REMOVE) {
	// 			transform.GetChild(3).gameObject.SetActive(false);
	// 			return;
	// 		}
	// 	}
		
	

		
	// 	// List<int> ll = new List<int>(new HashSet<int>(iList));

	void getSync(string s) {
		// Debug.Log("zzzz");
		// tt2.text = s;
		if (s == "start") {
			selfParticle.SetActive(true);
			selfParticle.transform.position = ballPos.position;
			// rb.AddForce(Vector3.down * 10f, ForceMode.Impulse );

			float x = transRadian(90f);
			float y = transRadian(0f);
			ps.startRotation3D = new Vector3(x,y,0f);

		}else if (s == "stop") {
			selfParticle.SetActive(false);
		}

	}

	void getShield(string s) {

		if (s == "show") {
			shield.SetActive(true);
		}else if(s == "hide") {
			shield.SetActive(false);
		}
	}

	void getSpin(string s) {

		if (s == "show") {
			spinCube.SetActive(true);
		}else if (s == "hide") {
			spinCube.SetActive(false);
		}
	}

	float transRadian(float ff) {

		var dir = (ff > 180) ? -1 : 1;
        var angleY = (dir > 0) ? ff : (360 - ff) * dir;
        var radian = (Mathf.PI / 180) * angleY;

		return radian;
	}

	void setUserName(string name) {
		selfTextMesh.text = name;
	}

	

}
