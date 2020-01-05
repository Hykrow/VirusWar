using Photon.Pun;
using System.Collections;
using System.Collections.Generic;
using System.IO;
using UnityEngine;
using UnityEngine.UI;

public class PhotonSpawner : MonoBehaviourPunCallbacks
{
    public Transform spawnPointP1;
    public Transform spawnPointP2;
    public Transform spawnPointP3;
    public Transform spawnPointP4;
    public GameObject startBTN;
    public GameObject readyBTN;
    public GameObject cancelBTN;
    public int playerRank = 1;
    void Awake()
    {
        DontDestroyOnLoad(this.gameObject);
    }
    // Start is called before the first frame update
    void Start()
    {
        Debug.Log("Creating a player");
        test();

    }
    public void test()
    {
        Debug.Log(PhotonNetwork.CurrentRoom.PlayerCount);
        if (PhotonNetwork.CurrentRoom.PlayerCount == 1)
        {

            PhotonNetwork.Instantiate(Path.Combine("PhotonPrefabs", "BluePlayer1"), spawnPointP1.transform.position, spawnPointP1.transform.rotation, 0);
            playerRank = 1;
            mainPlayer();

        }
        else if (PhotonNetwork.CurrentRoom.PlayerCount == 2)
        {
            PhotonNetwork.Instantiate(Path.Combine("PhotonPrefabs", "BluePlayer2"), spawnPointP2.transform.position, spawnPointP2.transform.rotation, 0);

            playerRank = 2;
            notMainPlayer();
        }
        else if (PhotonNetwork.CurrentRoom.PlayerCount == 3)
        {
            PhotonNetwork.Instantiate(Path.Combine("PhotonPrefabs", "BluePlayer3"), spawnPointP3.transform.position, spawnPointP3.transform.rotation, 0);
            playerRank = 3;
            notMainPlayer();
        }
        else if (PhotonNetwork.CurrentRoom.PlayerCount == 4)
        {
            PhotonNetwork.Instantiate(Path.Combine("PhotonPrefabs", "BluePlayer4"), spawnPointP4.transform.position, spawnPointP4.transform.rotation, 0);
            playerRank = 4;
            notMainPlayer();
        }


    }
    public void mainPlayer()
    {
        startBTN.SetActive(true);
        cancelBTN.SetActive(false);
        readyBTN.SetActive(false);
    }
    public void notMainPlayer()
    {
        startBTN.SetActive(false);
        cancelBTN.SetActive(false);
        readyBTN.SetActive(true);
    }
    // Update is called once per frame
    void Update()
    {
        
    }
}
