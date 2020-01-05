using Photon.Pun;
using Photon;
using System.Collections;
using System.Collections.Generic;
using System.IO;
using UnityEngine;

public class PhotonPlayer : MonoBehaviour
{
    private PhotonView PV;
    public GameObject myAvatar;
    // Start is called before the first frame update
    void Start()
    {
        Debug.Log("instianting");
        PV = GetComponent<PhotonView>();
        int spawnPicker = Random.Range(0, GameSetup.GS.spawnPoints.Length);
        if (PV.IsMine)
        {
            Debug.Log("instianting2");

           //myAvatar = PhotonNetwork.Instantiate(Path.Combine("PhotonPrefabs", "PhotonNetworkPlayer"), GameSetup.GS.spawnPoints[spawnPicker].position, GameSetup.GS.spawnPoints[spawnPicker].rotation, 0);
              
        }
    }
    public void OnPhotonSerializeView()
    {
        Debug.Log("serialized");
    }

    // Update is called once per frame
    void Update()
    {
        
    }
}
