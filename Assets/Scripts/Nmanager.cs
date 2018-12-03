using UnityEngine;
using System.Collections;
using UnityEngine.Networking; /* 유니티 네트워킹 API 입니다. */
 
 
public class Nmanager : NetworkManager /* NetworkManager 를 상속합니다. */
{
    NetworkClient myClient;
 
    /* 확장 함수 */
    public override void OnStartServer()
    {
        Debug.Log("OnStartServer( )");
    }
 
    public override void OnStartClient(NetworkClient client)
    {
        Debug.Log("OnStartClient( )");
    }
 
    public override void OnStopClient()
    {
        Debug.Log("OnStopClient( )");
    }
 
    /* 사용자 정의 함수 */
    public void SetupServer()
    {
        Debug.Log("SetupServer()");
        StartServer();    
        NetworkServer.Listen(7777);
        NetworkServer.RegisterHandler(MsgType.Connect, OnConnected);
 
	}
 
    public void SetupClient()
    {
        Debug.Log("SetupClient()");
        StartClient();    
 
        myClient = new NetworkClient ();
        myClient.Connect("127.0.0.1", 7777);
 
    }
    public void OnConnected(NetworkMessage netMsg)
    {
        Debug.Log("Connected to server");
		
    }

	public override void OnServerDisconnect(NetworkConnection conn) {
		// 서버에서 호출
	}
	
	public override void OnClientDisconnect(NetworkConnection conn) {
		// 클라이언트에서 호출
		Debug.Log("bbbbbbbbbbbbb");

	}

	public override void OnServerAddPlayer(NetworkConnection conn, short playerControllerId) {
		Debug.Log("add player ");
	}

	public override void OnServerRemovePlayer(NetworkConnection conn, UnityEngine.Networking.PlayerController player) {
		Debug.Log("remove player ");
	}
 
}