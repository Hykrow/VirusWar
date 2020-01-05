using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GameSetup : MonoBehaviour
{
    public static GameSetup GS;
    public Transform[] spawnPoints;
    // Start is called before the first frame update
    private void OnEnable()
    {
        Debug.Log("hello");
        if(GameSetup.GS == null)
        {
            GameSetup.GS = this;
        }

    }
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        
    }
}
