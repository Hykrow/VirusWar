﻿using Photon.Pun;
using Photon.Realtime;
using System.Collections;
using System.Collections.Generic;
using System.IO;
using UnityEngine;
using UnityEngine.SceneManagement;

public class PhotonRoom : MonoBehaviourPunCallbacks, IInRoomCallbacks
{
    //Room info
    public static PhotonRoom room;
    private PhotonView PV;
    public int playerCount;
    // public bool isGameLoaded;
    public int currectScene;
    public int MultiplayerScene;
    public Transform spawnPoint;
    public Transform spawnPointP1;
    public Transform spawnPointP2;
    public Transform spawnPointP3;
    public Transform spawnPointP4;
    public GameObject background;
    public GameObject myAvatar;
    /*
    // Player info
    Player[] photonPlayers;

    public int playersInRoom;
    public int myNumberInRoom;
    public int playersInGame;
    */

    private void Awake()
    {
        if (PhotonRoom.room == null)
        {
            PhotonRoom.room = this;
        }
        else
        {
            if (PhotonRoom.room != this)
            {
                UnityEngine.Object.Destroy(PhotonRoom.room.gameObject);
                PhotonRoom.room = this;
            }
        }

        DontDestroyOnLoad(this.gameObject);
        PV = GetComponent<PhotonView>();
    }

    void Start()
    {

    }


    public override void OnEnable()
    {
        //subscribe to functions
        base.OnEnable();
        PhotonNetwork.AddCallbackTarget(this);
        SceneManager.sceneLoaded += OnSceneFinishedLoading;

    }
    public override void OnDisable()
    {
        base.OnDisable();
        PhotonNetwork.RemoveCallbackTarget(this);
        SceneManager.sceneLoaded -= OnSceneFinishedLoading;
    }

    public override void OnJoinedRoom()
    {
        base.OnJoinedRoom();
        Debug.Log("now Inside a room");
        if (!PhotonNetwork.IsMasterClient)
            return;
        StartGame();

        /* photonPlayers = PhotonNetwork.PlayerList;
          playersInRoom = photonPlayers.Length;
         myNumberInRoom = playersInRoom;
         PhotonNetwork.NickName = myNumberInRoom.ToString();
         */

    }

    void StartGame()
    {

        Debug.Log("Loading Level");
        PhotonNetwork.LoadLevel(MultiplayerScene);
    }


    void OnSceneFinishedLoading(Scene scene, LoadSceneMode mode)
    {
        if(scene.buildIndex == 1)
        Instantiate(background);

        currectScene = scene.buildIndex;
        if (currectScene == MultiplayerScene)
        {
            Debug.Log("Starting function create a player");

            CreatePlayer();
        }

    }

    private void CreatePlayer()
    {




    }

    public void OnPhotonSerializeView()
    {
        Debug.Log("serialized");
    }

}