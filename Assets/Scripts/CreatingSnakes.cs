using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Photon.Pun;

public class CreatingSnakes : MonoBehaviour
{
    public string name;
    public List<Vector2> body = new List<Vector2>();
    public List<GameObject> gameObjects = new List<GameObject>();
    public GameObject prefab = (GameObject)Resources.Load("PhotonPrefabs/SnakeNotSure", typeof(GameObject));
    public CreatingSnakes getSnake(string namee)
    {
        if (Equals(name, namee))
        {

            return this;


        }
        else return null;

    }

    public Vector2 getHeadPos()
    {
        return body[0];
    }
    Vector2 NormalizePosition(Vector2 positionn)
    {
        //print("position: " + positionn.x);
        //print("position: " + positionn[0]);
        Vector3 pos = Camera.main.ScreenToWorldPoint(Input.mousePosition);
        //print(pos[0] + " " + pos[1]);
        //gameObject.transform.position = pz;
        float snakePosX = 0;
        //print(FindPosition(positionn)[0]);
        if (positionn[0] <= 19)
        {
            snakePosX = -1900 + (95 / 2) + (positionn[0]) * (95);

        }
        else snakePosX = (95 / 2) + (positionn[0] - 20) * 95;
        //print(snakePosX);

        float snakePosY = 0;
        if (positionn[1] <= 19)
        {
            snakePosY = 1900 - (95 / 2) - (positionn[1]) * (95);

        }

        else snakePosY = -(95 / 2) - (positionn[1] - 20) * 95;
        Vector2 numbToReturn = new Vector2(snakePosX, snakePosY);
        return numbToReturn;
    }

    public void addCube(Vector2 toAdd, bool isHead = false)
    {
        if (isHead)
        {
            body.Insert(0, toAdd);
        }
        else body.Add(toAdd);
        GameObject toInstantiate = (GameObject)Instantiate(prefab, NormalizePosition(toAdd), Quaternion.identity);
        gameObjects.Add(toInstantiate);
    }

    public void transferToExisting(List<Snake> listSnake)
    {
        foreach( Snake snk in listSnake)
        {
            if (Equals(snk.name, name)){
                bool isHeaded = true;
                foreach(Vector2 vect2 in body)
                {  
                    snk.addCube(vect2, isHeaded);
                    isHeaded = false;
                }
            }
        }
        DestroyIO();
        body.Clear();
    }

    public void DestroyIO()
    {

        foreach ( GameObject gmb in gameObjects)
        {

            Destroy(gmb);
        }
    }
}
