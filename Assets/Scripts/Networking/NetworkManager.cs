using Photon.Pun;
using Photon.Realtime;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class NetworkManager : MonoBehaviour
{
    public string roomName = "dd";
    public string playerPrefabName = "Player2";
    public GameObject spawnPoint;
    // Start is called before the first frame update
    void Start()
    {
        PhotonNetwork.ConnectUsingSettings();

        /*
        PhotonNetwork.OfflineMode = false;           //true would "fake" an online connection
        PhotonNetwork.NickName = "PlayerName";       //to set a player name
        PhotonNetwork.AutomaticallySyncScene = true; //to call PhotonNetwork.LoadLevel()
        PhotonNetwork.GameVersion = "v1";            //only people with the same game version can play together

        //PhotonNetwork.ConnectToMaster(ip,port,appid); //manual connection
        if (!PhotonNetwork.OfflineMode)
            PhotonNetwork.ConnectUsingSettings();
            */
    }
    void OnJoinedLobby()
    {
        RoomOptions roomOptions = new RoomOptions() { IsVisible = false, MaxPlayers = 4 };
        PhotonNetwork.JoinOrCreateRoom(roomName, roomOptions, TypedLobby.Default);

    }

    void OnJoinedRoom()
    {
        PhotonNetwork.Instantiate(playerPrefabName, spawnPoint.transform.position, spawnPoint.transform.rotation, 0);
    }

    // Update is called once per frame
    void Update()
    {
        
    }
}
