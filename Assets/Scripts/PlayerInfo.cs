using Photon.Pun;
using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;

public class PlayerInfo : MonoBehaviour
{
    public GameObject tmp;
    // Start is called before the first frame update
    public bool isPlayerTxtChanged = false;
    public int playerNumber;
    void Start()
    {
        playerNumber = PhotonNetwork.CurrentRoom.PlayerCount;
        /*TextMeshPro tmpTXT;
        tmpTXT = tmp.GetComponent<TextMeshPro>();*/
        TextMeshPro tmpTXT = GetComponent<TextMeshPro>();
        tmpTXT.text = "Player " + PhotonNetwork.CurrentRoom.PlayerCount;

        Debug.Log("heheh1");
        isPlayerTxtChanged = true;
         //Destroy(GetComponent<PlayerInfo>());
        //Destroy(GetComponent<TextMeshPro>()); 
        Debug.Log("heheh");
        
        Debug.Log("heheh");

    }

    // Update is called once per frame
    void Update()
    {
        
    }
}
