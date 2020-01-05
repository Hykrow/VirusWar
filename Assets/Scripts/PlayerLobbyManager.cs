using Photon.Pun;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class PlayerLobbyManager : MonoBehaviour
{
    public GameObject RoomTitle;
    public GameObject RoomName;
    public GameObject readyBTN;
    public GameObject cancelBTN;
    public int readyScore;
    // Start is called before the first frame update
    void Start()
    {

          Text RoomNameText;
          RoomNameText = RoomName.GetComponent<Text>();
          RoomNameText.text = "#" + PhotonNetwork.CurrentRoom.Name;



          Debug.Log("room length : " + PhotonNetwork.CurrentRoom.Name);
          Debug.Log(PhotonNetwork.CurrentRoom);
          //PhotonNetwork.CurrentRoom.PlayerCount 
    }


      public void OnStartButtonClicked()
     {
         //if (PhotonNetwork.CurrentRoom.PlayerCount == 1) return;

         Debug.Log(readyScore);
         Debug.Log(PhotonNetwork.CurrentRoom.PlayerCount);
        if (PhotonNetwork.CurrentRoom.PlayerCount == 1)
        {
            Debug.Log("load1");
            PhotonNetwork.LoadLevel(2);
        }
        if (PhotonNetwork.CurrentRoom.PlayerCount == 2 && readyScore == 1)
         {
             Debug.Log("load1");
             PhotonNetwork.LoadLevel(2);
         }
         else if (PhotonNetwork.CurrentRoom.PlayerCount == 3 && readyScore == 2)
         {
             PhotonNetwork.LoadLevel(3);

        }
        else if (PhotonNetwork.CurrentRoom.PlayerCount == 4 && readyScore == 3)
         {

            PhotonNetwork.LoadLevel(3);

         }
         Debug.Log("load2");

     }
     public void OnReadyButtonClicked()
     {
         //readyBTN = GameObject.Find("Start");
         PhotonView photonView = GetComponent<PhotonView>();
         photonView.RPC("readyBtnClicked", RpcTarget.All);
         readyBTN.SetActive(false);
         //readyScore++;


         cancelBTN.SetActive(true);
         Debug.Log(readyScore);
     }

    [PunRPC]
     void readyBtnClicked()
     {
         readyScore++;
         Debug.Log(readyScore);

     }


     [PunRPC]
     void cancelBtnClicked()
     {
         readyScore--;
         Debug.Log(readyScore);

     }
     public void OnCancelButtonClicked()
     {
         PhotonView photonView = PhotonView.Get(this);
         photonView.RPC("cancelBtnClicked", RpcTarget.All);

         cancelBTN.SetActive(false);
         //readyScore--;
         readyBTN.SetActive(true);

     }



     // Update is called once per frame
     void Update()
     {

     }
}
