using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Photon.Pun;
using System.Threading;
using System.Threading.Tasks;

public class Snake : MonoBehaviour
{            /* var anim = gmb.gameObject.GetComponentInChildren<Animation>();
             print(gmb);
             AnimationClip animationClip;
             // define animation curve
             AnimationCurve scaleZ = AnimationCurve.Linear(8f, 1.0f, 2.0f, 3.0f);
             animationClip = new AnimationClip();
             // set animation clip to be legacy
             animationClip.legacy = true;
             animationClip.SetCurve("", typeof(Transform), "localPosition.x", scaleZ);
             anim.AddClip(animationClip, "test");
             anim.Play("test");
             */
    public string name;
    public List<Vector2> body = new List<Vector2>();
    public List<GameObject> gameObjects = new List<GameObject>();
    public GameObject snakeHead = (GameObject) Resources.Load("PhotonPrefabs/SnakeHead1", typeof(GameObject));
    public int fromWhichOneIsItCreated;
    public GameObject snakebody = (GameObject)Resources.Load("PhotonPrefabs/SnakeBody", typeof(GameObject));
    public GameObject destroySnakeManager = GameObject.FindWithTag("SnakeDestroyerTag");
    public bool hasItBeenDestroyed = false;

    public Snake getSnake(string namee)
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

    public void DestroyIO()
    {
        SnakeDestroyer snkDestroyer = destroySnakeManager.AddComponent<SnakeDestroyer>();
        snkDestroyer.StartCoroutine("DestroyIO", gameObjects);
    }

    public void addCube(Vector2 toAdd, bool isHead = false)
    {
        if(body.Count == 0)
        {
            body.Add(toAdd);
            GameObject toInstantiateeee = (GameObject)Instantiate(snakeHead, NormalizePosition(toAdd), Quaternion.identity);
            gameObjects.Add(toInstantiateeee);
            return;
        }


        var oldHead = body[0];

        if (isHead)
        {

            body.Insert(0, toAdd);
        }
        else body.Add( toAdd);

      
            int last = gameObjects.Count - 1;
            var oldPos = gameObjects[last].transform.position;//Get the old Position from the last array
            Destroy(gameObjects[last]);
            gameObjects.Remove(gameObjects[last]); 
            GameObject toInstantiatee = (GameObject)Instantiate(snakebody, oldPos, Quaternion.identity);// reecreateIt with His old pos BUT an other prefab, which is white
            gameObjects.Add(toInstantiatee);

               
            GameObject toInstantiateee = (GameObject)Instantiate(snakeHead, NormalizePosition(toAdd), Quaternion.identity);
            gameObjects.Add(  toInstantiateee);


    }
}
