using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Networking;

public class ServerCheck : NetworkBehaviour {

	
	[SyncVar]
	public bool check = false;
	void Start () {
		
	}
	
	// Update is called once per frame
	void Update () {
		
	}

	[ClientRpc]
	public void RpcsetCheck() {
		check = true;
	}
}
