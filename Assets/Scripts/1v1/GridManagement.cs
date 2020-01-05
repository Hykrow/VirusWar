using Photon.Pun;
using System;
using System.Collections;
using System.Collections.Generic;
using System.IO;
using UnityEngine;

public class GridManagement : MonoBehaviour
{

    public Vector2 worldBottomLeft = new Vector2(-1.7f, 23.4f);
    public Vector2 worldBottomRight = new Vector2(-9.1f, 23.4f);
    public bool creatingSnake = false;
    int[,] coordinates = new int[,] { { 0, 0 }, { 3, 4 }, { 5, 6 }, { 7, 8 } };
    public GameObject[] snakeTrained;
    List<GameObject> snakeTail;
    public GameObject prefab;
    public GameObject snakeTailPrefab;
    int playerRank;
    public bool canPlay;
    public GameObject cubesRoot;
    //[numeroDuSnake -> [CubeDuSnake[0:posX,1: posY,2: isLastHead? ]]
    public Dictionary<string, List<float[]>> CreatingSnakes = new Dictionary<string, List<float[]>>();

    //List<float> lastPosToMove = new List<float>();
    public string snakeNumber;
    //[numeroDuSnake -> [CubeDuSnake[0:posX,1: posY,2: isLastHead? ]]
    public Dictionary<string, List<float[]>> ExistingSnakes = new Dictionary<string, List<float[]>>();

    //public List<List<float[]>> ExistingSnakes = new List<List<float[]>>(); 

    // Start is called before the first frame update
    void Start()
    {
        canPlay = true;
        snakeTail = new List<GameObject>();
        // GameObject.FInd
        GameObject player = GameObject.FindGameObjectWithTag("LobbySpawner");
        playerRank = player.GetComponent<PhotonSpawner>().playerRank;


    }


    bool isAnythingNearbyToTheHead(int WhichOne, int[] positionINT)
    {

        if (WhichOne == 0)
        {
            foreach (string str in CreatingSnakes.Keys)
            {
                for (int i = 0; getSnake(str, WhichOne).Count > i; i++)
                {
                    print(getSnake(str, WhichOne)[i][2]);
                    float[] getSnakee = getSnake(str, 0)[i];

                    
                        if ((getSnakee[0] == positionINT[0] - 1 || getSnakee[0] == positionINT[0] + 1 || getSnakee[0] == positionINT[0]) &&
                            (getSnakee[1] == positionINT[1] - 1 || getSnakee[1] == positionINT[1] + 1 || getSnakee[1] == positionINT[1]) &&
                             getSnakee[2] == 1)
                        {
                        return true;
                        }

                }
                //if(getSnake(str, WhichOne)[0][3] == 1)

            }
        }

        else if (WhichOne == 1)
        {
            foreach (string str in ExistingSnakes.Keys)
            {
                for (int i = 0; getSnake(str, WhichOne).Count > i; i++)
                {
                    print(getSnake(str, WhichOne)[i][2]);
                    float[] getSnakee = getSnake(str, 1)[i];
                    if ((getSnakee[0] == positionINT[0] - 1 || getSnakee[0] == positionINT[0] + 1 || getSnakee[0] == positionINT[0]) &&
                        (getSnakee[1] == positionINT[1] - 1 || getSnakee[1] == positionINT[1] + 1 || getSnakee[1] == positionINT[1]) &&
                         getSnakee[2] == 1)
                    {
                        return true;
                    }

                }
                //if(getSnake(str, WhichOne)[0][3] == 1)

            }
        }


        else if (WhichOne == 2)
        {
            foreach (string str in CreatingSnakes.Keys)
            {
                for (int i = 0; getSnake(str, WhichOne).Count > i; i++)
                {
                    print(getSnake(str, WhichOne)[i][2]);
                    float[] getSnakee = getSnake(str, 0)[i];
                    if ((getSnakee[0] == positionINT[0] - 1 || getSnakee[0] == positionINT[0] + 1 || getSnakee[0] == positionINT[0]) &&
                        (getSnakee[1] == positionINT[1] - 1 || getSnakee[1] == positionINT[1] + 1 || getSnakee[1] == positionINT[1]) &&
                         getSnakee[2] == 1)
                    {
                        return true;
                    }

                }
                //if(getSnake(str, WhichOne)[0][3] == 1)

            }
            foreach (string str in ExistingSnakes.Keys)
            {
                for (int i = 0; getSnake(str, WhichOne).Count > i; i++)
                {
                    print(getSnake(str, WhichOne)[i][2]);
                    float[] getSnakee = getSnake(str, 1)[i];
                    if ((getSnakee[0] == positionINT[0] - 1 || getSnakee[0] == positionINT[0] + 1 || getSnakee[0] == positionINT[0]) &&
                        (getSnakee[1] == positionINT[1] - 1 || getSnakee[1] == positionINT[1] + 1 || getSnakee[1] == positionINT[1]) &&
                         getSnakee[2] == 1)
                    {
                        return true;
                    }

                }
                //if(getSnake(str, WhichOne)[0][3] == 1)

            }
        }


        return false;
    }



    bool isAnythingHere(int WhichOne,  int[] positionINT)
    {
        if(WhichOne == 2)
        {
            foreach (string str in CreatingSnakes.Keys)
            {
                for (int i = 0; getSnake(str, WhichOne).Count > i; i++)
                {
                    print(getSnake(str, WhichOne)[i][2]);
                    float[] getSnakee = getSnake(str, 0)[i];
                    if (getSnakee[0] == positionINT[0] && getSnakee[1] == positionINT[1])
                    {
                        return true;
                    }

                }
                //if(getSnake(str, WhichOne)[0][3] == 1)

            }
            foreach (string str in ExistingSnakes.Keys)
            {
                for (int i = 0; getSnake(str, WhichOne).Count > i; i++)
                {
                    print(getSnake(str, WhichOne)[i][2]);
                    float[] getSnakee = getSnake(str, 1)[i];
                    if (getSnakee[0] == positionINT[0] && getSnakee[1] == positionINT[1])
                    {
                        return true;
                    }

                }
                //if(getSnake(str, WhichOne)[0][3] == 1)

            }
        }else if(WhichOne == 0)
        {
            foreach (string str in CreatingSnakes.Keys)
            {
                for (int i = 0; getSnake(str, WhichOne).Count > i; i++)
                {
                    print(getSnake(str, WhichOne)[i][2]);
                    float[] getSnakee = getSnake(str, 0)[i];
                    if (getSnakee[0] == positionINT[0] && getSnakee[1] == positionINT[1])
                    {
                        return true;
                    }

                }
                //if(getSnake(str, WhichOne)[0][3] == 1)

            }
        }
        else if(WhichOne == 1)
        {
            foreach (string str in ExistingSnakes.Keys)
            {
                for (int i = 0; getSnake(str, WhichOne).Count > i; i++)
                {
                    print(getSnake(str, WhichOne)[i][2]);
                    float[] getSnakee = getSnake(str, 1)[i];
                    if (getSnakee[0] == positionINT[0] && getSnakee[1] == positionINT[1])
                    {
                        return true;
                    }

                }
                //if(getSnake(str, WhichOne)[0][3] == 1)

            }
        }
        return false;
    }
    string getSnakeName(int WhichOne, int[] positionFLOAT)
    {
        if (WhichOne == 0)
        {
            foreach (string str in CreatingSnakes.Keys)
            {
                for (int i = 0; getSnake(str, WhichOne).Count > i; i++)
                {
                    print(getSnake(str, WhichOne)[i][2]);
                    float[] getSnakee = getSnake(str, WhichOne)[i];
                    if (getSnakee[0] == positionFLOAT[0] && getSnakee[1] == positionFLOAT[1])
                    {
                        return (str);
                    }

                }
                //if(getSnake(str, WhichOne)[0][3] == 1)

            }
        }
        else
        {


        }

        return null;
    }


    string getSnakeHeadName(int WhichOne, int[] positionFLOAT)
    {
        if (WhichOne == 0)
        {
            foreach (string str in CreatingSnakes.Keys)
            {
                for(int i = 0; getSnake(str, WhichOne).Count > i; i++)
                {
                    print(getSnake(str, WhichOne)[i][2]);
                    float[] getSnakee = getSnake(str, WhichOne)[i];
                    if (getSnakee[0] == positionFLOAT[0] &&  getSnakee[1] == positionFLOAT[1] && getSnakee[2] ==1)
                    {
                        return (str);
                    }

                }
                //if(getSnake(str, WhichOne)[0][3] == 1)

            }
        }
        else
        {


        }

            return null;
    }

    List<float[]> getSnake(string GiveTheSnakeName, int WhichOne)
    {
        if(WhichOne == 0)
        {

        
        foreach (string str in CreatingSnakes.Keys)
        {
            // Get the value for our Key.
            List < float[]> value = CreatingSnakes[str];

            // If the Key is the desired Key, append to its list.
            if (str == GiveTheSnakeName)
            {
                return value;
            }

            // print the first item in each of the Lists.
            Debug.Log(value[0]);
        }
        }
        else
        {
            foreach (string str in ExistingSnakes.Keys)
            {
                // Get the value for our Key.
                List<float[]> value = ExistingSnakes[str];

                // If the Key is the desired Key, append to its list.
                if (str == GiveTheSnakeName)
                {
                    return value;
                }

                // print the first item in each of the Lists.
                Debug.Log(value[0]);
            }
        }
        return null;
    }


    float[] getSnakeHead(int WhichOne, int[] positionINT) {
        if (WhichOne == 0)
        {
            foreach (string str in CreatingSnakes.Keys)
            {
                for (int i = 0; getSnake(str, WhichOne).Count > i; i++)
                {
                    print(getSnake(str, WhichOne)[i][2]);
                    float[] getSnakee = getSnake(str, 0)[i];
                    if (getSnakee[0] == positionINT[0] && getSnakee[1] == positionINT[1])
                    {
                        return getSnakee;
                    }

                }
                //if(getSnake(str, WhichOne)[0][3] == 1)
            }
        }

        else if (WhichOne == 1)
        {
            foreach (string str in ExistingSnakes.Keys)
            {
                for (int i = 0; getSnake(str, WhichOne).Count > i; i++)
                {
                    print(getSnake(str, WhichOne)[i][2]);
                    float[] getSnakee = getSnake(str, 1)[i];
                    if (getSnakee[0] == positionINT[0] && getSnakee[1] == positionINT[1])
                    {
                        return getSnakee;
                    }

                }
                //if(getSnake(str, WhichOne)[0][3] == 1)

            }
        }
        return null;
       }
    int[] FindPosition(Vector3 position)
    {
        float xPos = -1900;
        float yPos = 1900;

        for (int x = 0; x <= 40; x++)
        {
            if (position.x < xPos + 95 && position.x > xPos)
            {
                for (int y = 0; y <= 40; y++)
                {
                    // print(y);

                    if (position.y > yPos - 95 && position.y < yPos)
                    {
                        //print("found the good case: x: " + x + ", y :" + y);
                        //break;
                        int[] coords = new int[] { x, y };
                        return (coords);

                    }
                    else yPos -= 95;


                }
                break;

            }

            else
            {
                //print("not the good case, increasing the numb");
                xPos += 95;

            }


        }
        int[] coordss = new int[] { 600, 600 };

        return coordss;
    }

     int[] findCase()
    {
        float xPos = 491f;
        float yPos = 995f;

        //print(Input.mousePosition);
        //491, 995
        //1392
        for (int x = 0; x <= 40; x++)
        {
            if (Input.mousePosition.x < xPos + 22.525f && Input.mousePosition.x > xPos)
            {
               // print(x);
                for (int y = 0; y <= 40; y++)
                {
                    // print(y);

                    if (Input.mousePosition.y > yPos - 22.525f && Input.mousePosition.y < yPos)
                    {
                        //print("found the good case: x: " + x + ", y :" + y);
                        //break;
                        int[] coords = new int[] { x, y };
                        return (coords);

                    }
                    else yPos -= 22.525f;


                }
                break;

            }

            else
            {
                //print("not the good case, increasing the numb");
                xPos += 22.525f;

            }


        }
        return null;

    }

    public void createSnake()
    {
        creatingSnake = true;
        
    }
    public void turnManager()
    {
        canPlay = true;

    }
    public void EndTurn()
    {
        //float[] toAdd = new float[] { lastPosToMove[0], lastPosToMove[1] };
        //snakeHeadPos.Add(toAdd);
    }
    // Update is called once per frame

    float[] NormalizePosition(Vector3 positionn)
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
        float[] numbToReturn = { snakePosX, snakePosY };
        return numbToReturn; 
    }
    void Update()
    {

        print(canPlay);
        if (canPlay)
        {

            if (Input.GetMouseButton(0))
            {

                int tailLimit = 1;
                Vector3 pos = Camera.main.ScreenToWorldPoint(Input.mousePosition);
                pos.z = 0;
                if (FindPosition(pos)[0] == 600) return;

                if (creatingSnake)
                {

                    Vector3 pz = Camera.main.ScreenToWorldPoint(Input.mousePosition);
                    pz.z = 0;
                    gameObject.transform.position = pz;
                    print(pz);
                    bool goodCase = false;
                    int findCaseX = Mathf.RoundToInt(FindPosition(pz)[0]);
                    int findCaseY = Mathf.RoundToInt(FindPosition(pz)[1]);
                    print(findCaseX);
                    print(findCaseY);
                    print(playerRank);
                    if (playerRank == 1)
                    {


                        if (findCaseX == 18 && findCaseY == 35)
                        {
                            goodCase = true;
                            snakeNumber = "a";

                        }
                        if (findCaseX == 18 && findCaseY == 36)
                        {
                            goodCase = true;
                            snakeNumber = "b";

                        }


                        if (findCaseX == 19 && findCaseY == 37)
                        {
                            goodCase = true;
                            snakeNumber = "c";

                        }
                        if (findCaseX == 19 && findCaseY == 34)
                        {
                            goodCase = true;
                            snakeNumber = "d";

                        }


                        if (findCaseX == 20 && findCaseY == 37)
                        {
                            goodCase = true;
                            snakeNumber = "e";

                        }
                        if (findCaseX == 20 && findCaseY == 34)
                        {
                            goodCase = true;
                            snakeNumber = "f";

                        }


                        if (findCaseX == 21 && findCaseY == 36)
                        {
                            goodCase = true;
                            snakeNumber = "g";

                        }
                        if (findCaseX == 21 && findCaseY == 35)
                        {
                            goodCase = true;
                            snakeNumber = "h";

                        }


                    }
                    else if (playerRank == 2)
                    {
                        print("this is player2");


                        if (findCaseX == 18 && findCaseY == 35 - 32)
                        {
                            goodCase = true;
                            snakeNumber = "a";

                        }
                        if (findCaseX == 18 && findCaseY == 36 - 32)
                        {
                            goodCase = true;
                            snakeNumber = "b";

                        }


                        if (findCaseX == 19 && findCaseY == 37 - 32)
                        {
                            goodCase = true;
                            snakeNumber = "c";

                        }
                        if (findCaseX == 19 && findCaseY == 34 - 32)
                        {
                            goodCase = true;
                            snakeNumber = "d";

                        }


                        if (findCaseX == 20 && findCaseY == 37 - 32)
                        {
                            goodCase = true;
                            snakeNumber = "e";

                        }
                        if (findCaseX == 20 && findCaseY == 34 - 32)
                        {
                            goodCase = true;
                            snakeNumber = "f";

                        }


                        if (findCaseX == 21 && findCaseY == 36 - 32)
                        {
                            goodCase = true;
                            snakeNumber = "g";

                        }
                        if (findCaseX == 21 && findCaseY == 35 - 32)
                        {
                            goodCase = true;
                            snakeNumber = "h";

                        }
                    }




                    print(goodCase);

                    if (getSnake(getSnakeName(1, FindPosition(pos)), 1).Count == 0)
                    {
                        print("succ");
                        float snakePosX;
                        if (FindPosition(pz)[0] <= 19)
                        {
                            snakePosX = -1900 + (95 / 2) + (FindPosition(pz)[0]) * (95);

                        }
                        else snakePosX = (95 / 2) + (FindPosition(pz)[0] - 20) * 95;


                        float snakePosY;
                        if (FindPosition(pz)[1] <= 19)
                        {
                            snakePosY = 1900 - (95 / 2) - (FindPosition(pz)[1]) * (95);

                        }
                        else snakePosY = -(95 / 2) - (FindPosition(pz)[1] - 20) * 95;
                        //int snakePosX = (95 / 2) + FindPosition(pz)[0] * 95;
                        //int snakePosY = (95 / 2) + FindPosition(pz)[1] * 95;
                        if (goodCase)
                        {
                            Vector3 snakePos = new Vector3(snakePosX, snakePosY, 0);
                            GameObject gmd = PhotonNetwork.Instantiate(Path.Combine("PhotonPrefabs", "SnakeHead1"), snakePos, Quaternion.identity, 0);
                            float[] floatToAdd = new float[] { FindPosition(pz)[0], FindPosition(pz)[1], 1 };

                            ExistingSnakes[snakeNumber] = new List<float[]>() { floatToAdd };

                            //canPlay = false;
                        }




                        //peut creer n'importe ou autour
                    }
                    else
                    {

                        bool isSmthAlreadyHere = false;
                        List<float[]> mySnake = getSnake(getSnakeName(1, FindPosition(pos)), 1);
                        for (int i = 0; i < mySnake.Count; i++)
                        {
                            List<float[]> snakeFromClickedPos = getSnake(getSnakeHeadName(0, FindPosition(pos)), 0);
                            //if (Mathf.RoundToInt(FindPosition(snakeTail[i].transform.position)[0]) == Mathf.RoundToInt(FindPosition(pz)[0]) && Mathf.RoundToInt(FindPosition(snakeTail[i].transform.position)[1]) == Mathf.RoundToInt(FindPosition(pz)[1]))
                            if(isAnythingHere(1, FindPosition(pos)))
                            {
                                //snake trained exists at this case
                                print("smth already exists here");
                                isSmthAlreadyHere = true;
                                break;

                            }

                        }
                        if (!isSmthAlreadyHere)
                        {
                            float snakePosX;
                            if (FindPosition(pz)[0] <= 19)
                            {
                                snakePosX = -1900 + (95 / 2) + (FindPosition(pz)[0]) * (95);

                            }
                            else snakePosX = (95 / 2) + (FindPosition(pz)[0] - 20) * 95;


                            float snakePosY;
                            if (FindPosition(pz)[1] <= 19)
                            {
                                snakePosY = 1900 - (95 / 2) - (FindPosition(pz)[1]) * (95);

                            }
                            else snakePosY = -(95 / 2) - (FindPosition(pz)[1] - 20) * 95;
                            //int snakePosX = (95 / 2) + FindPosition(pz)[0] * 95;
                            //int snakePosY = (95 / 2) + FindPosition(pz)[1] * 95;
                            if (goodCase)
                            {
                                Vector3 snakePos = new Vector3(snakePosX, snakePosY, 0);
                                GameObject gmd = PhotonNetwork.Instantiate(Path.Combine("PhotonPrefabs", "SnakeHead1"), snakePos, Quaternion.identity, 0);
                                //snakeTail.Add(gmd);
                                float[] toAdd = new float[] { FindPosition(pz)[0], FindPosition(pz)[1], 1 };
                                ExistingSnakes[snakeNumber] = new List<float[]>() { toAdd };

                                // snakeHeadPos.Add(toAdd);
                                //canPlay = false;
                            }
                            //create the snake
                            //GameObject gmd = PhotonNetwork.Instantiate(Path.Combine("PhotonPrefabs", "SnakeTrained1"), spawnPointP4.transform.position, spawnPointP4.transform.rotation, 0);

                            // snakeTrained.Add()

                        }
                    }
                    creatingSnake = false;

                }



            }


                //Si aucun cube ce tour a été crée, alors ce truc ce met en route
                /*if (posToMove.Count == 0)
                {
                    pos = Camera.main.ScreenToWorldPoint(Input.mousePosition);
                    //float[] addValue =  ;
                    float[] findPosFloat = new float[] { FindPosition(pos)[0], FindPosition(pos)[1] };
                    //Vector3 posToMoove = new Vector3(findPosFloat[0], findPosFloat[1], 0);
                    posToMove.Add(findPosFloat);
                    print("xCase: " + findPosFloat[0] + "yCase: " + findPosFloat[1]);

                    print("posToMoveCount = 0");
                    lastPosToMove.Clear();
                    lastPosToMove.Add(findPosFloat[0]);
                    lastPosToMove.Add(findPosFloat[1]);

                    
                }




                //si deja un cube a été cree mais moins que 6 alors ca se met en route
                else if(posToMove.Count < 6)
                {

                    bool anythingHere = false;
                    bool nearToLast = false;

                    //Check if anything is on the clicked position
                    for (int i = 0; posToMove.Count > i; i++)
                    {
                        float xPos = posToMove[i][0];
                        float yPos = posToMove[i][1];
                        pos = Camera.main.ScreenToWorldPoint(Input.mousePosition);
                        Vector3 vectPos = new Vector3(FindPosition(pos)[0], FindPosition(pos)[1]);
                        print(xPos);
                        if ((vectPos[0] == xPos && vectPos[1] == yPos) && posToMove.Count < 6)
                        {
                            anythingHere = true;
                            print("something is on this pos");

                        }

                    }

                    //ICI MODIF LE SCRIPT SI COUNT = 0
                    if (lastPosToMove.Count == 0)
                    {
                        // si tout est réuni, instantiate la tete et mettre la lastHead a cette place
                        pos = Camera.main.ScreenToWorldPoint(Input.mousePosition);
                        float[] findPosFloat = new float[] { FindPosition(pos)[0], FindPosition(pos)[1] };
                        if ((!anythingHere)&& posToMove.Count < 5)
                        {
                            lastPosToMove.Clear();
                            lastPosToMove.Add(findPosFloat[0]);
                            lastPosToMove.Add(findPosFloat[1]);

                            print("posToMove count = " +posToMove.Count);

                            print("xCase: " + findPosFloat[0] + "yCase: " + findPosFloat[1]);
                            posToMove.Add(findPosFloat);
                            tailLimit++;
                            print(tailLimit);

                        }
                    }


                    pos = Camera.main.ScreenToWorldPoint(Input.mousePosition);
                    //check if the clicked position is near to the last created snake head
                    //                    Xpos -1                                   Xpos +1                                                  Xpos            
                    int xPosF = FindPosition(pos)[0];
                    int yPosF = FindPosition(pos)[1];


                    for (int i = 0; ExistingCubes.Count < i; i++)
                    {
                        if ((ExistingCubes[i][0] == xPosF - 1 || ExistingCubes[i][0] == xPosF + 1 || ExistingCubes[i][0] == xPosF) &&
                            (ExistingCubes[i][1] == yPosF - 1 || ExistingCubes[i][1] == yPosF + 1 || ExistingCubes[i][1] == yPosF) &&
                             ExistingCubes[i][2] == 1)
                        {
                            ExistingCubes.RemoveAt(i);
                            nearToLast = true;
                            print("NearToLast");
                        }
                        else print("notNEar");
                    }
                    //lastPosToMove.Clear();



                    print(nearToLast);

                    pos = Camera.main.ScreenToWorldPoint(Input.mousePosition);

                    // si tout est réuni, instantiate la tete et mettre la lastHead a cette place
                    float[] findPosFloatr = new float[]{ FindPosition(pos)[0], FindPosition(pos)[1] };
                    if ((!anythingHere) && nearToLast && posToMove.Count < 5)
                    {
                        print(posToMove.Count);
                        print("Each of the conditions are reunited");
                        posToMove.Add(findPosFloatr);
                        print(findPosFloatr);
                        Vector3 normPos = new Vector3(findPosFloatr[0], findPosFloatr[1]);
                        print(NormalizePosition(normPos));
                        tailLimit++;
                        print(tailLimit);

                    }
                    else print("some of these conditions aint reunited");






                }


                for(int i = 0; i <= posToMove.Count; i++)
                {
                    float xPos = posToMove[i][0];
                    float yPos = posToMove[i][1];
                    // Reset la List lastPos pour l'update avec celle clickée
                   // lastPosToMove.Clear();
                    //astPosToMove.Add(xPos);
                    //lastPosToMove.Add(yPos);
                   
                    //si i = la le nombre de la list, alors mettre cette pos comme last:

                    
                    Vector3 MovingPos = new Vector3(xPos, yPos, 0);

                    print("instantiating (" + i + ")");
                    print("xPos :" + NormalizePosition(MovingPos)[0] + "yPos: " + NormalizePosition(MovingPos)[1]);

                    instantiate(MovingPos);
                }
                float[] flt = new float[] { lastPosToMove[0], lastPosToMove[1] };
                //if(NormalizePosition(pos))

            }*/

        }
        //if()


    }
    void instantiate(Vector3 position)
    {
       // Vector3 posssToMove = new Vector3(FindPosition(position)[0], FindPosition(position)[1]);
        Vector3 normalizedPosToMove = new Vector3(NormalizePosition(position)[0], NormalizePosition(position)[1]);
        print(normalizedPosToMove[0] + " " + normalizedPosToMove[1]);
        Instantiate(snakeTailPrefab, normalizedPosToMove, Quaternion.identity );
    }
            }

public class PosToWalk
{

    public bool walkable;
    public float[] coords;

    public PosToWalk(float[] _coords)
    {
        coords = _coords;
    }
}
