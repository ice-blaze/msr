using UnityEngine;
using System.Collections;

public class MainMenuVik : MonoBehaviour
{
	const int WIDTH = 150;
    void Awake()
    {
		HighscoreManager.SetPath(Application.dataPath+"/");
        //PhotonNetwork.logLevel = NetworkLogLevel.Full;

        //Connect to the main photon server. This is the only IP and port we ever need to set(!)
        if (!PhotonNetwork.connected)
            PhotonNetwork.ConnectUsingSettings("v1.0"); // version of the game/demo. used to separate older clients from newer ones (e.g. if incompatible)

        //Load name from PlayerPrefs
        PhotonNetwork.playerName = PlayerPrefs.GetString("playerName", "Guest" + Random.Range(1, 9999));

        //Set camera clipping for nicer "main menu" background
        Camera.main.farClipPlane = Camera.main.nearClipPlane + 0.1f;

    }

	void Update()
	{
		if(Input.GetButton("Cancel"))
		{
			Application.LoadLevel(0);
		}
	}

	private Vector2 scrollPos = Vector2.zero;

    void OnGUI()
    {
        if (!PhotonNetwork.connected)
        {
            ShowConnectingGUI();
            return;   //Wait for a connection
        }


        if (PhotonNetwork.room != null)
            return; //Only when we're not in a Room

        GUILayout.BeginArea(new Rect((Screen.width - 400) / 2, (Screen.height - 300) / 2, 400, 300));

        //Player name
        GUILayout.BeginHorizontal();
        GUILayout.Label("YOUR NAME :", GUILayout.Width(WIDTH));
        PhotonNetwork.playerName = GUILayout.TextField(PhotonNetwork.playerName);
        if (GUI.changed)//Save name
            PlayerPrefs.SetString("playerName", PhotonNetwork.playerName);
        GUILayout.EndHorizontal();

        GUILayout.Space(15);

		GUILayout.BeginHorizontal();
		GUILayout.Label("SOLO PLAYER :",GUILayout.Width(WIDTH));
		if (GUILayout.Button("Play!"))
		{
			PhotonNetwork.CreateRoom(PhotonNetwork.playerName, new RoomOptions() { maxPlayers = 1 }, TypedLobby.Default);
		}
		GUILayout.EndHorizontal();

		GUILayout.Space(15);

        //Create a room (fails if exist!)
        GUILayout.BeginHorizontal();
        GUILayout.Label("TWO  PLAYER :", GUILayout.Width(WIDTH));
		if (GUILayout.Button("Play!"))
		{
			// using null as TypedLobby parameter will also use the default lobby
			PhotonNetwork.CreateRoom(PhotonNetwork.playerName, new RoomOptions() { maxPlayers = 2 }, TypedLobby.Default);
		}
//        roomName = GUILayout.TextField(name);
        GUILayout.EndHorizontal();


        GUILayout.Space(30);
        GUILayout.Label("ROOM LISTING:");
        if (PhotonNetwork.GetRoomList().Length == 0)
        {
            GUILayout.Label("...no games available...");
        }
        else
        {
            //Room listing: simply call GetRoomList: no need to fetch/poll whatever!
            scrollPos = GUILayout.BeginScrollView(scrollPos);
            foreach (RoomInfo game in PhotonNetwork.GetRoomList())
            {
                GUILayout.BeginHorizontal();
                GUILayout.Label(game.name + " " + game.playerCount + "/" + game.maxPlayers);
                if (GUILayout.Button("JOIN"))
                {
                    PhotonNetwork.JoinRoom(game.name);
                }
                GUILayout.EndHorizontal();
            }
            GUILayout.EndScrollView();
        }

        GUILayout.EndArea();
    }


    void ShowConnectingGUI()
    {
        GUILayout.BeginArea(new Rect((Screen.width - 400) / 2, (Screen.height - 300) / 2, 400, 300));

        GUILayout.Label("Connecting to Photon server.");
        GUILayout.Label("Hint: This demo uses a settings file and logs the server address to the console.");

        GUILayout.EndArea();
    }
}
