#if ENABLE_UNET
using UnityEngine.SceneManagement;

namespace UnityEngine.Networking
{
	[AddComponentMenu("Network/NetworkManagerHUD")]
	[System.ComponentModel.EditorBrowsable(System.ComponentModel.EditorBrowsableState.Never)]
	public class NetworkManagerHUD : MonoBehaviour
	{
		public NetworkManager manager;
		[SerializeField] public bool showGUI = true;
		[SerializeField] public int offsetX;
		[SerializeField] public int offsetY;

		// Runtime variable
		bool showServer = false;
		GUIStyle mCustom; 
		public VirtualJoystick joystick;
		public GameObject stopButton;
		void Awake()
		{
			manager = GetComponent<Server>();
			mCustom = new GUIStyle("button");
			mCustom.fontSize = 30;
			
		}
	
		
	
		void Start()
		{
			// manager.StartServer();
		}
		void Update()
		{
			if (!showGUI)
				return;

			if (!NetworkClient.active && !NetworkServer.active && manager.matchMaker == null)
			{
				if (Input.GetKeyDown(KeyCode.S))
				{
					manager.StartServer();
				}
				if (Input.GetKeyDown(KeyCode.H))
				{
					manager.StartHost();
				}
				if (Input.GetKeyDown(KeyCode.C))
				{
					manager.StartClient();
				}
			}
			if (NetworkServer.active && NetworkClient.active)
			{
				if (Input.GetKeyDown(KeyCode.X))
				{
					manager.StopHost();
				}
			}
		}

		void OnGUI()
		{
			if (!showGUI)
				return;

			int xpos = 10 + offsetX;
			int ypos = 40 + offsetY;
			int spacing = 124;

			if (!NetworkClient.active && !NetworkServer.active && manager.matchMaker == null)
			{	
				
				if (GUI.Button(new Rect(xpos, ypos, 400, 100), "LAN Host(H)", mCustom))
				{
					joystick.offui();
					manager.StartHost();
					
				}
				ypos += spacing;

				if (GUI.Button(new Rect(xpos, ypos, 205, 100), "LAN Client(C)", mCustom ))
				{
					joystick.offui();
					stopButton.SetActive(true);
					// Debug.Log("active = " + NetworkServer.active); 
					manager.StartClient();
					
				}
				manager.networkAddress = GUI.TextField(new Rect(xpos + 200, ypos, 195, 100), manager.networkAddress, mCustom);
				ypos += spacing;

				if (GUI.Button(new Rect(xpos, ypos, 400, 100), "LAN Server Only(S)", mCustom))
				{
					manager.StartServer();
				}
				ypos += spacing;
			}
			else
			{
				if (NetworkServer.active)
				{
					GUI.Label(new Rect(xpos, ypos, 500, 100), "Server: port=" + manager.networkPort, mCustom );
					ypos += spacing;
				}
				if (NetworkClient.active)
				{
					GUI.Label(new Rect(xpos, ypos, 500, 100), "Client: address=" + manager.networkAddress + " port=" + manager.networkPort, mCustom);
					ypos += spacing;
				}
			}

			if (NetworkClient.active && !ClientScene.ready)
			{
				if (GUI.Button(new Rect(xpos, ypos, 400, 100), "Client Ready", mCustom))
				{
					ClientScene.Ready(manager.client.connection);
				
					if (ClientScene.localPlayers.Count == 0)
					{
						ClientScene.AddPlayer(0);
					}
				}
				ypos += spacing;
			}

			if (NetworkServer.active || NetworkClient.active)
			{
				if (GUI.Button(new Rect(xpos, ypos, 400, 100), "Stop (X)", mCustom))
				{
					manager.StopHost();
					SceneManager.LoadScene(SceneManager.GetActiveScene().buildIndex);
				}
				ypos += spacing;
			}

			if (!NetworkServer.active && !NetworkClient.active)
			{
				ypos += 10;

				if (manager.matchMaker == null)
				{
					if (GUI.Button(new Rect(xpos, ypos, 400, 100), "Enable Match Maker (M)", mCustom))
					{
						manager.StartMatchMaker();
					}
					ypos += spacing;
				}
				else
				{
					if (manager.matchInfo == null)
					{
						if (manager.matches == null)
						{
							if (GUI.Button(new Rect(xpos, ypos, 400, 100), "Create Internet Match"))
							{
								// manager.matchMaker.CreateMatch(manager.matchName, manager.matchSize, true, "", manager.OnMatchCreate);
							}
							ypos += spacing;

							GUI.Label(new Rect(xpos, ypos, 400, 100), "Room Name:", mCustom);
							manager.matchName = GUI.TextField(new Rect(xpos+100, ypos, 200, 100), manager.matchName, mCustom);
							ypos += spacing;

							ypos += 10;

							if (GUI.Button(new Rect(xpos, ypos, 400, 100), "Find Internet Match"))
							{
								// manager.matchMaker.ListMatches(0,20, "", manager.OnMatchList);
							}
							ypos += spacing;
						}
						else
						{
							foreach (var match in manager.matches)
							{
								if (GUI.Button(new Rect(xpos, ypos, 400, 100), "Join Match:" + match.name))
								{
									manager.matchName = match.name;
									manager.matchSize = (uint)match.currentSize;
									// manager.matchMaker.JoinMatch(match.networkId, "", manager.OnMatchJoined);
								}
								ypos += spacing;
							}
						}
					}

					if (GUI.Button(new Rect(xpos, ypos, 400, 100), "Change MM server", mCustom))
					{
						showServer = !showServer;
					}
					if (showServer)
					{
						ypos += spacing;
						if (GUI.Button(new Rect(xpos, ypos, 200, 100), "Local", mCustom))
						{
							manager.SetMatchHost("localhost", 1337, false);
							showServer = false;
						}
						ypos += spacing;
						if (GUI.Button(new Rect(xpos, ypos, 200, 100), "Internet", mCustom))
						{
							manager.SetMatchHost("mm.unet.unity3d.com", 443, true);
							showServer = false;
						}
						ypos += spacing;
						if (GUI.Button(new Rect(xpos, ypos, 200, 100), "Staging", mCustom))
						{
							manager.SetMatchHost("staging-mm.unet.unity3d.com", 443, true);
							showServer = false;
						}
					}

					ypos += spacing;

					GUI.Label(new Rect(xpos, ypos, 500, 100), "MM Uri: " + manager.matchMaker.baseUri, mCustom);
					ypos += spacing;

					if (GUI.Button(new Rect(xpos, ypos, 400, 100), "Disable Match Maker", mCustom))
					{
						manager.StopMatchMaker();
					}
					ypos += spacing;
				}
			}
		}
	}
};
#endif //ENABLE_UNET
