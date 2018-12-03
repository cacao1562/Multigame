using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Networking;
using UnityEngine.Networking.NetworkSystem;


public class Server : NetworkManager {

	// [SyncVar]
	// public bool serverActive = false;
	// void Awake()
	// {
	// 	StartServer();
	// 	StopClient();
	// }
	// void Start()
	// {
	// 	// networkAddress = "127.0.0.1";
	// 	// networkPort = 7777;
	// 	StartServer();
		
	// }
	public ServerCheck sc;
	public GameObject enemy;
	
	void Start()
	{
		// maxConnections = 1;
		// sc = transform.GetChild(0).GetComponent<ServerCheck>();
	}
	public override void OnStartServer(){

		base.OnStartServer();
		Debug.Log("[OnStartServer] Start Server success \n ip = " + networkAddress + " : " + networkPort);
		if (NetworkServer.active) {
			// NetworkServer.active = true;
			// sc.CmdsetCheck();
			Debug.Log("server active");
			// CmdsetActive();
		}else {
			Debug.Log("server not active");
		}
	
		
		// onlineScene = "second";
		
		
	}

	

	IEnumerator showEnemy() {

		for (int i=0; i<300; i++) {
			
			float x = Random.Range(-23f, 23f);
			float z = Random.Range(-27f, 27f);
			Vector3 v = new Vector3(x, 0f , z);
			GameObject e = Instantiate(enemy, v , Quaternion.identity);
			e.transform.LookAt(Vector3.zero);
			// e.GetComponent<Rigidbody>().AddForce(e.transform.forward * 10f);
			e.GetComponent<Rigidbody>().velocity = e.transform.forward * 16f;
			NetworkServer.Spawn(e);
			Destroy(e, 5f);
			yield return new WaitForSeconds(3f);
		}
	}

	//클라이언트가 들어왔을때 서버에서 호출
	public override void OnServerConnect(NetworkConnection conn) {
		
		base.OnServerConnect(conn);
		Debug.Log("[OnServerConnect] Client Connect ip = " + conn.address );
		StartCoroutine( showEnemy() );
		NetworkServer.dontListen = true;
		var x = NetworkClient.allClients;
		Debug.Log("client lenght = " + x.Count );
	}

	//클라이언트가 나갔을때 서버에서 호출
	public override void OnServerDisconnect(NetworkConnection conn) {
		
		base.OnServerDisconnect(conn);
		Debug.Log("[OnServerDisconnect] Client Disconnect ip = " + conn.address );

	}

	public override void OnServerError(NetworkConnection conn, int errorCode) {

		// base.OnServerError(conn,errorCode);
		Debug.Log("[OnServerError] Server Error , Error code = " + errorCode );
		// offlineScene = "main";
	}

	public override void OnClientConnect(NetworkConnection conn) {

		base.OnClientConnect(conn);
		Debug.Log("[OnClientConnect] server ip =" + conn.address);
		Debug.Log("server check22 = " + NetworkServer.active);
	}


	public override void OnStartClient(NetworkClient client) {
		base.OnStartClient(client);
		Debug.Log("start client");
		client.RegisterHandler(MsgType.Disconnect, ConnectionError);
		
		// if (sc.check) {
		// 	Debug.Log("[OnStartClient] check true");
		// }else {
		// 	Debug.Log("[OnStartClient] check false");
		// }
		// onlineScene = "set";
		// offlineScene = "set2";
		// Debug.Log("server check = " + NetworkServer.active);
		// if (serverActive) {
		// 	Debug.Log("[OnStartClient] server Active true");
		// }else {
		// 	Debug.Log("[OnStartClient] server Active false");	
		// }
		
	}



	public override void OnClientError(NetworkConnection conn, int errorCode) {

		base.OnClientError(conn, errorCode);
		Debug.Log("[OnClientError] error code = " + errorCode );
	}

	public override void OnClientDisconnect(NetworkConnection conn) {

		base.OnClientDisconnect(conn);
		Debug.Log("OnClientDisconnect ip = " + conn.address );
		
	}

	public override void OnClientNotReady(NetworkConnection conn) {

		Debug.Log("[OnClientNotReady] ddd");
	}
	public override void OnServerReady(NetworkConnection conn) {
		Debug.Log("[OnServerReady] ddd");
	}

	public virtual void OnClientNotReady() {
		Debug.Log("ddddddd");
	}
	
	public void Stop() {
		StopClient();
	}

	public void ConnectionError(NetworkMessage netMsg) {
		Debug.Log("connection error");
	}

	bool repeat = true;
	// void Update()
	// {
	// 	if (NetworkServer.active && repeat) {
	// 		sc.RpcsetCheck();
	// 		// Debug.Log("aaaaaaa");
	// 		repeat = false;
	// 	}else {
	// 		// Debug.Log("bbbbbbbbb");
	// 	}

	// 	if (isNetworkActive) {
	// 		Debug.Log("tttt");
	// 	}else {
	// 		Debug.Log("fffff");
	// 	}
	// }

}
