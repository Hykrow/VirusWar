using Photon.Pun;
using Photon.Realtime;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;
using System.IO;
using System;
using Random = UnityEngine.Random;
using System.Text;

public class PhotonLobby : MonoBehaviourPunCallbacks, ILobbyCallbacks
{
    public string userId;
    public GameObject connectButton;
    public GameObject soloButton;
    public GameObject quitButton;
    public GameObject joinRoomButton;
    public GameObject createRoomButton;
    public GameObject joinRandomRoomButton;
    public GameObject seeRoomsButton;
    public GameObject inviteFriendsButton;
    public GameObject multButton;
    public GameObject roomList;
    public GameObject selectModeBackground;
    public GameObject emptyBackground;

    public GameObject Background;
    public GameObject musicBackground;
    //public GameObject downloadButton;
    public GameObject inputUrlButton;
    public GameObject InstantiatedBackground;
    public string finalObject ="main";
    public RoomNameScript rns;
    List<RoomInfo> createdRooms = new List<RoomInfo>();

    public static PhotonLobby lobby;
    private Hashtable hm;

    public void OnMusicButtonClicked()
    {
        finalObject = "music";
        Destroy(InstantiatedBackground);
        soloButton.SetActive(false);
        quitButton.SetActive(false);
        multButton.SetActive(false);
        connectButton.SetActive(false);

        InstantiatedBackground = Instantiate(musicBackground);
    }
    public void OnLeaveMusicButtonClicked()
    {
        //if( finalObject == "music")
        //{
        inputUrlButton.SetActive(false);
        print("leave music button clicked");
        Destroy(InstantiatedBackground);
        InstantiatedBackground = Instantiate(Background);
        //    } 
        connectButton.SetActive(true);
        quitButton.SetActive(true);
        soloButton.SetActive(true);
        multButton.SetActive(true);

        if (finalObject == "empty")
        {

            joinRandomRoomButton.SetActive(false);
            joinRoomButton.SetActive(false);

            seeRoomsButton.SetActive(false);
            createRoomButton.SetActive(false);

        }
    }
    
    private void Awake()
    {
        //userId = "" + Random.Range(-1000000, 1000000);

        lobby = this;
    }
    // Start is called before the first frame update
    void Start()
    {
        InstantiatedBackground = Instantiate(Background);
        //PhotonNetwork.AuthValues = new AuthenticationValues(userId);

        PhotonNetwork.ConnectUsingSettings();
        //PhotonNetwork.ConnectToRegion("eu");
    }
    public override void OnConnectedToMaster()
    {
        PhotonNetwork.AutomaticallySyncScene = true;
        multButton.SetActive(true);
        connectButton.SetActive(false);

        Debug.Log("connected to master");

    }
    public void OnMultiplayerButtonClicked()
    {
        finalObject = "empty";
        Destroy(InstantiatedBackground);
        InstantiatedBackground = Instantiate(selectModeBackground);

        connectButton.SetActive(false);
        quitButton.SetActive(false);
        soloButton.SetActive(false);
        multButton.SetActive(false);

        joinRandomRoomButton.SetActive(true);
        joinRoomButton.SetActive(true);

        seeRoomsButton.SetActive(true);
        //inviteFriendsButton.SetActive(true);
        createRoomButton.SetActive(true);




    }

    public void OnSeeRoomsClicked()
    {
        PhotonNetwork.JoinLobby();
        
        
    }
    public override void OnRoomListUpdate(List<RoomInfo> roomList)
    {
        Debug.Log("We have received the Room list");
        //After this callback, update the room list
        createdRooms = roomList;
        Debug.Log(createdRooms);
    }
    public void OnCreateRoomClicked()
    {
        CreateRoom();
    }
    public void OnJoinRandomRoomClicked()
    {
        PhotonNetwork.JoinRandomRoom();
    }
    public void OnBattleButtonClicked()
    {
        PhotonNetwork.JoinRandomRoom();
    }
    void CreateRoom()
    {
        RoomNameScript rns = GetComponent<RoomNameScript>();


        int randomRoomName = Random.Range(0, 9999);
    
        RoomOptions roomOps = new RoomOptions() { IsVisible = true, IsOpen = true, MaxPlayers = 4};
        PhotonNetwork.CreateRoom("" + randomRoomName, roomOps);

        Debug.Log("SUccessfully joined a room");
        //SceneManager.LoadScene("Lobby");
    }
public override void OnJoinRandomFailed(short returnCode, string message)
    {
        base.OnJoinRandomFailed(returnCode, message);
        Debug.Log(message);
        Debug.Log("Creating rooms since no one is online.");
        CreateRoom();

    }
    public override void OnJoinedRoom()
    {
        base.OnJoinedRoom();
        Debug.Log("now Inside a room");
        //PhotonNetwork.PlayerList;

    }


    void Update()
    {
        if (Input.GetKey(KeyCode.Escape))
        {
            OnLeaveMusicButtonClicked();
        }
    }
}
