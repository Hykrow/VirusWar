using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Photon.Pun;
using System.IO;

public class PlayerManager : MonoBehaviour
{
    public GameObject spawnPointP1;
    public GameObject spawnPointP2;

    public  bool isAnyPlayerAlreadyMoved;
    // Start is called before the first frame update
    void Start()
    {
        SnakeSpawner();

    }
    void SnakeSpawner()
    {
        PhotonSpawner phSpawner = new PhotonSpawner();
        if (phSpawner.playerRank == 1)
        {
            PhotonNetwork.Instantiate(Path.Combine("PhotonPrefabs", "SnakeHead1"), spawnPointP1.transform.position, spawnPointP1.transform.rotation, 0);

        }
        if (phSpawner.playerRank == 2)
        {
            PhotonNetwork.Instantiate(Path.Combine("PhotonPrefabs", "SnakeHead2"), spawnPointP2.transform.position, spawnPointP2.transform.rotation, 0);

        }
    }
    // Update is called once per frame
    void Update()
    {
        
    }
}
