using Photon.Pun;
using System;
using System.Collections;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.UI;

public class Grid : MonoBehaviour
{
    public bool creatingSnake;
    public bool canPlay;
    public int playerRank;
    public string creatingSnakeName;
    public List<Snake> Snakes = new List<Snake>();
    public List<CreatingSnakes> CreatingSnakesList = new List<CreatingSnakes>();
    string[] wholeAlphabet = new string[] { "a", "b", "c", "d", "e", "f", "g", "h", "i", "j", "k", "l", "m", "n", "o", "p" };

    string[] alphabet = new string[] {"a", "b", "c", "d", "e", "f", "g", "h", };
    string[] alphabet2 = new string[] {"i", "j", "k", "l", "m", "n", "o", "p"};
    public GameObject textYouCanPlay;
    public int tailLimit = 0;
    public string snakeLock;
    int WhichTurn = 5;
    PhotonView photonView;
    public GameObject popup;
    public GameObject closeThePopupButton;
    public GameObject abortButton;
    public bool canValidate = false;

    public Text winText;
    public Text uCanPlayText;
    public GameObject playAgainButton;
    public GameObject yellowBackground;
    public GameObject goMainMenu;
    public int whoWon2;
    public void Start()
    {
        canPlay = true;
        Debug.Log(PhotonNetwork.CurrentRoom);
        photonView = GetComponent<PhotonView>();
        PhotonNetwork.CurrentRoom.IsOpen = false;
        InstantiateSnakeList();
        snakeLock = null;
        print("canPlay first time = " + canPlay);
        GameObject player = GameObject.FindGameObjectWithTag("LobbySpawner");
        playerRank = player.GetComponent<PhotonSpawner>().playerRank;
        WhichTurn = playerRank;
        OnEndTurnNotNetworked();
        // GameObject.FInd


    }

    public void InstantiateSnakeList()
    {
 
            foreach(string str in wholeAlphabet)
            {
                Snake instantiatingSnake = new Snake();
                instantiatingSnake.name = str;
                Snakes.Add(instantiatingSnake);
            }
        

    }

    Vector2 NormalizePosition(Vector2 positionn)
    {

        Vector3 pos = Camera.main.ScreenToWorldPoint(Input.mousePosition);
        float snakePosX = 0;

        if (positionn[0] <= 19)
        {
            snakePosX = -1900 + (95 / 2) + (positionn[0]) * (95);

        }
        else snakePosX = (95 / 2) + (positionn[0] - 20) * 95;


        float snakePosY = 0;
        if (positionn[1] <= 19)
        {
            snakePosY = 1900 - (95 / 2) - (positionn[1]) * (95);

        }

        else snakePosY = -(95 / 2) - (positionn[1] - 20) * 95;
        Vector2 numbToReturn = new Vector2( snakePosX, snakePosY );
        return numbToReturn;
    }

    public void OnResetValues()
    {
        print("cancelling");
        if (CreatingSnakesList.Count != 0 )
        {
            canValidate = false;

            if (snakeLock == null)
            {
                if (CreatingSnakesList.Count == 0) return;
                print("this shit");

                CreatingSnakes CreatingSnakeToDelete = GetCreatingSnakesFromName(creatingSnakeName);
                print(creatingSnakeName);
                print(CreatingSnakeToDelete.name);

                CreatingSnakeToDelete.DestroyIO();
                CreatingSnakeToDelete.body.Clear();
                CreatingSnakesList.Clear();
            }
            else
            {
                CreatingSnakes CreatingSnakeToDelete = GetCreatingSnakesFromName(snakeLock);
                CreatingSnakeToDelete.DestroyIO();
                CreatingSnakeToDelete.body.Clear();
                CreatingSnakesList.Clear();

            }

        }
        creatingSnake = false;

        snakeLock = null;
        canPlay = true;
        tailLimit = 0;


    }

    Vector2 FindPosition(Vector2 position)
    {
        float xPos = -1900;
        float yPos = 1900;

        for (int x = 0; x <= 40; x++)
        {
            if (position.x < xPos + 95 && position.x > xPos)
            {
                for (int y = 0; y <= 40; y++)
                {

                    if (position.y > yPos - 95 && position.y < yPos)
                    {
                        Vector2 coords = new Vector2( x, y );
                        return (coords);

                    }
                    else yPos -= 95;


                }
                break;

            }

            else
            {
                xPos += 95;

            }


        }
        Vector2 coordss = new Vector2( 600, 600);

        return coordss;
    }

    bool isAnyThingHere(Vector2 position)
    {
        foreach (Snake snk in Snakes)
        {
            foreach (Vector2 pos in snk.body)
            {
                if (pos[0] == position[0] && pos[1] == position[1] && !snk.hasItBeenDestroyed) return true;
            }
        }
        foreach (CreatingSnakes snk in CreatingSnakesList)

        {
            foreach (Vector2 pos in snk.body)
            {
                if (pos[0] == position[0] && pos[1] == position[1]) return true;
            }
        }
        return  false;
    }

    string nameOfTheSnakeNearToTheHead(Vector2 position)
    {
        foreach (Snake snk in Snakes)
        {

            var pos = snk.body[0];
            if ((pos[0] == position[0] - 1 || pos[0] == position[0] + 1 || pos[0] == position[0]) &&
                (pos[1] == position[1] - 1 || pos[1] == position[1] + 1 || pos[1] == position[1]) && !snk.hasItBeenDestroyed)
                return snk.name;

        }

        return null;
    }

    bool isAnyThingNearToTheHead(Vector2 position)
    {
        foreach (Snake snk in Snakes)
        {
            if (snk.body.Count != 0) {
                var pos = snk.body[0];

                if (pos[0] == position[0])
                {
                    if (pos[1] == position[1] - 1 || pos[1] == position[1] + 1 && !snk.hasItBeenDestroyed) return true;
                }
                else if (pos[1] == position[1])
                {
                    if (pos[0] == position[0] - 1 || pos[0] == position[0] + 1 && !snk.hasItBeenDestroyed) return true;

                }
            } 

            //return false;

        }
        foreach (CreatingSnakes snk in CreatingSnakesList)
        {
            if (snk.body.Count == 0) return false;
            var pos = snk.body[0];
            /* if ((pos[0] == position[0] - 1 || pos[0] == position[0] + 1 || pos[0] == position[0]) &&
                 (pos[1] == position[1] - 1 || pos[1] == position[1] + 1 || pos[1] == position[1]))
                 return true;*/

            if (pos[0] == position[0])
            {
                if (pos[1] == position[1] - 1 || pos[1] == position[1] + 1) return true;
            }
            else if (pos[1] == position[1])
            {
                if (pos[0] == position[0] - 1 || pos[0] == position[0] + 1) return true;

            }

        }
        return false;
    }

    Snake GetSnakeFromPosition(Vector2 position)
    {
        foreach(Snake snk in Snakes)
        {

            if(snk.body.Count != 0)
            {
                print("looks good for now");

                print(snk.name + " is the snake name");
                var pos = snk.body[0];
                if (pos[0] == position[0])
                {
                    if (Mathf.RoundToInt(pos[1]) == Mathf.RoundToInt(position[1] - 1) || Mathf.RoundToInt(pos[1]) == Mathf.RoundToInt(position[1] + 1) && !snk.hasItBeenDestroyed) return snk;
                }
                else if (pos[1] == position[1])
                {
                    if (Mathf.RoundToInt(pos[0]) == Mathf.RoundToInt(position[0] - 1) || Mathf.RoundToInt(pos[0]) == Mathf.RoundToInt(position[0] + 1) && !snk.hasItBeenDestroyed) return snk;

                }
            }
        }
        print("hella returning shit fk tyhis");
        Snake newSnake = new Snake();
        newSnake.name = "nope";
        return newSnake;
    }

    Snake GetSnakesFromStrictPositionButNotOur(Vector2 position)
    {
        foreach (Snake snk in Snakes)
        {
            foreach (Vector2 bdy in snk.body)
            {
                if (bdy[0] == position[0] && bdy[1] == position[1] && !IsThisSnakeOurs(snk.name) && !snk.hasItBeenDestroyed) return snk;
            }
        }

        Snake newSnake = new Snake();
        newSnake.name = "nope";
        return newSnake;
    }

    Snake GetSnakesFromStrictPositionButOur(Vector2 position)
    {
        foreach (Snake snk in Snakes)
        {
            foreach (Vector2 bdy in snk.body)
            {
                if (bdy[0] == position[0] && bdy[1] == position[1] && IsThisSnakeOurs(snk.name) && !snk.hasItBeenDestroyed) return snk;
            }
        }

        return null;
    }

    CreatingSnakes GetCreatingSnakesFromStrictPositionButOur(Vector2 position)
    {
        foreach (CreatingSnakes snk in CreatingSnakesList)
        {
            foreach (Vector2 bdy in snk.body)
            {
                if (bdy[0] == position[0] && bdy[1] == position[1] && IsThisSnakeOurs(snk.name)) return snk;
            }
        }

        CreatingSnakes newSnake = new CreatingSnakes();
        newSnake.name = "nope";
        return newSnake;
    }

    Snake GetSnakesFromStrictPosition(Vector2 position)
    {
        foreach (Snake snk in  Snakes)
        {
            foreach (Vector2 bdy in snk.body)
            {
                if (bdy == position && !snk.hasItBeenDestroyed) return snk;
            }
        }

        Snake newSnake = new Snake();
        newSnake.name = "nope";
        return newSnake;
    }
    Snake GetSnakesFromStrictPositionButEvenTheDestroyed(Vector2 position)
    {
        foreach (Snake snk in  Snakes)
        {
            foreach (Vector2 bdy in snk.body)
            {
                if (bdy == position ) return snk;
            }
        }

        Snake newSnake = new Snake();
        newSnake.name = "nope";
        return newSnake;
    }

    CreatingSnakes GetCreatingSnakesFromStrictPosition(Vector2 position)
    {
        foreach (CreatingSnakes snk in CreatingSnakesList)
        {
            foreach (Vector2 bdy in snk.body)
            {
                if (bdy[0] == position[0] && bdy[1] == position[1]) return snk;
            }
        }

        return null;
    }

    CreatingSnakes GetCreatingSnakesFromPositionButTheWholeOne(Vector2 position)
     {
         foreach (CreatingSnakes snk in CreatingSnakesList)
         {
                 foreach(Vector2 pos in snk.body)
             {

                 if (pos[1] == position[1] - 1 || pos[1] == position[1] + 1) return snk;



                 else if (pos[0] == position[0] - 1 || pos[0] == position[0] + 1) return snk;
             }



         }

         CreatingSnakes newSnake = new CreatingSnakes();
         newSnake.name = "nope";
         return newSnake;
     }
     
    bool IsAnyCreatingSnakesFromPositionButTheWholeOneBool(Vector2 position)
    {
        foreach (CreatingSnakes snk in CreatingSnakesList)
        {
            foreach (Vector2 pos in snk.body)
            {

                if (pos[1] == position[1] - 1 || pos[1] == position[1] + 1) return true;



                else if (pos[0] == position[0] - 1 || pos[0] == position[0] + 1) return true;
            }



        }


        return false;
    }
    public void OnPlayAgainButtonClicked()
    {
        SceneManager.LoadScene("Lobby");
    }
    public void OnGoMainMenuAgainButtonClicked()
    {
        PhotonNetwork.LeaveRoom();
        SceneManager.LoadScene("Menu");
    }
    public void WinText() {

        // call it after everything is destroytyed 
        uCanPlayText.text = "";
        //winText.text = "aaaaaa";
        winText.gameObject.SetActive(true);
        yellowBackground.SetActive(true);
        playAgainButton.SetActive(true);
        //goMainMenu.SetActive(true);
        print(whoWon2);
        print(playerRank);
        yellowBackground.SetActive(true);
        if (whoWon2+1 == playerRank)
        {

            winText.text = "Congrats, You won !";
        }
        else
        {
            winText.text = "Noo, You lost :(";
        }
    }

    [PunRPC]
    public void Win(int _whoWon)
    {
        whoWon2 = _whoWon;


        foreach(CreatingSnakes snk in CreatingSnakesList.ToArray())
        {
            snk.DestroyIO();
            snk.body.Clear();
            CreatingSnakesList.Clear();
        }

        foreach (Snake snk in Snakes.ToArray())
        {

            snk.DestroyIO();
            snk.body.Clear();
            Snakes.Clear();
            InstantiateSnakeList();
        }
    }

    CreatingSnakes GetCreatingSnakesFromPosition(Vector2 position)
    {
        foreach (CreatingSnakes snk in CreatingSnakesList)
        {
            if (snk.body.Count == 0) break;
            var pos = snk.body[0];
            if (pos[0] == position[0])
            {

                print(snk.name);
                if (Mathf.RoundToInt(pos[1]) == Mathf.RoundToInt(position[1] - 1) || Mathf.RoundToInt(pos[1] )== Mathf.RoundToInt((position[1] + 1))) { print("SKKKKTRTRT");return(snk); }
                print("WTF");
            }
            else if (pos[1] == position[1])
            {
                print("hello yeeeeee");

                if (pos[0] == position[0] - 1 || pos[0] == position[0] + 1) return snk;

            }
        }

        CreatingSnakes newSnake = new CreatingSnakes();
        newSnake.name = "nope";
        return newSnake;
    }

    public bool isAnyCreatingSnakesHere(Vector2 position) {
        
        foreach(CreatingSnakes snk in CreatingSnakesList)
        {
            foreach(Vector2 pos in snk.body)
            {
                if (pos[0] == position[0] && pos[1] == position[1]) return true;
            }
        }

        return false;
    }

    CreatingSnakes GetCreatingSnakesFromName(string name)
    {
        foreach (CreatingSnakes snk in CreatingSnakesList)
        {
            if (snk.name == name)
            {

                return snk;
            }
        }

        return null;
    }

    [PunRPC]
    public void InstantiateToSnake(string nameToInstantiate, Vector2 toAdd, bool isHeaded)
    {

        foreach (Snake snk in Snakes)
        {
            if (Equals(snk.name, nameToInstantiate))
            {

                snk.addCube(toAdd, isHeaded);
                break;

            }



        }
    }

    [PunRPC]
    public void DestroySnake(string nameToDestroy)
    {

        foreach (Snake snk in Snakes)
        {
            if (Equals(snk.name, nameToDestroy))
            {
                snk.hasItBeenDestroyed = true;
                //snk.DestroyIO();
                //snk.body.Clear();
                //CreatingSnakesList.Clear();
                //creatingSnk.DestroyIO();
                break;

            }



        }
    }

    public Snake GetSnakesFromStrictPositionButNotOur2(string snkName, Vector2 position)
    {
        foreach(Snake snk in Snakes)
        {
            if(snk.name != snkName)
            {
                foreach (Vector2 bdy in snk.body)
                {
                    if (bdy[0] == position[0] && bdy[1] == position[1] && !IsThisSnakeOurs(snk.name) && !snk.hasItBeenDestroyed) return snk;
                }
            }
        }
        Snake newSnake = new Snake();
        newSnake.name = "nope";
        return newSnake;
    }
    public void TransformCreatingToExisting()
    {
        canValidate = false; 
        print("transform creating to existing");
        bool isSomeOneWon = false;

        foreach (CreatingSnakes creatingSnk in CreatingSnakesList)
        {
            print(creatingSnk.name + " is the creating snake name");
            foreach (Snake snk in Snakes)
            {
                print(snk.name + " is the snake name");

                if (Equals(snk.name, creatingSnk.name ))
                {
                    print("BOTH NAMES ARE EQUALS");

                    bool isHeaded = true;
                    var bodyReversed = Enumerable.Reverse(creatingSnk.body);

                    foreach (Vector2 vect2 in bodyReversed)
                    {
                        if(GetSnakesFromStrictPosition(vect2).name != "nope")
                        {
                            
                            var ennemy = GetSnakesFromStrictPosition(vect2);


                            print("this shit ain null");
                            //IF our pos is the ennemy's head, we destroy it.
                            if(ennemy.body[0] == vect2)
                            {
                                print("its the ennemys head");

                                //TODO: replace destroySnake method to make it destroy the body of the snake or set hasItBeenDeleted to true ,
                                photonView.RPC("DestroySnake", RpcTarget.All, ennemy.name);
                                //ennemy.DestroyIO();
                                //ennemy.body.Clear();
                            }else
                            {// if our pos is the ennemy's tail, we destroy ourselves
                                print("its the ennemys tail");

                                //snk.DestroyIO();
                                //snk.body.Clear();
                                photonView.RPC("DestroySnake", RpcTarget.All, snk.name);

                                CreatingSnakesList.Clear();
                                creatingSnk.DestroyIO();

                                return; // we call return so it doesnt continue to create snake to the asked position
                            }
                        }
                        var FoundPosition = vect2;
                        if (playerRank == 1)
                        {


                            Vector2 firstCheck = new Vector2(19, 4);
                            Vector2 secCheck = new Vector2(20, 4);

                            Vector2 thirdCheck = new Vector2(19, 3);
                            Vector2 fourthCheck = new Vector2(20, 3);


                            if (FoundPosition == firstCheck || FoundPosition == secCheck || FoundPosition == thirdCheck || FoundPosition == fourthCheck)
                            {
                                isSomeOneWon = true;


                            }

                        }
                        else
                        {
                            Vector2 firstCheck = new Vector2(19, 35);
                            Vector2 secCheck = new Vector2(20, 35);

                            Vector2 thirdCheck = new Vector2(19, 36);
                            Vector2 fourthCheck = new Vector2(20, 36);


                            if (FoundPosition == firstCheck || FoundPosition == secCheck || FoundPosition == thirdCheck || FoundPosition == fourthCheck)
                            {
                                isSomeOneWon = true;



                            }


                        }

                        if (creatingSnk.body[0] == vect2) isHeaded = true;
                        else isHeaded = false;

                        photonView.RPC("InstantiateToSnake", RpcTarget.All, snk.name, vect2, isHeaded);
                        //snk.addCube(vect2, isHeaded);
                        isHeaded = false;


                    }
                    break;

                }



            }

            creatingSnk.DestroyIO();
        }
        if(isSomeOneWon)
            photonView.RPC("Win", RpcTarget.All, playerRank);

        CreatingSnakesList.Clear();
    }

    public void CreateSnake()
    {

        Vector3 pos = Camera.main.ScreenToWorldPoint(Input.mousePosition);
        pos.z = 0;
        if (FindPosition(pos)[0] == 600) return;
        if (GetSnakesFromStrictPositionButEvenTheDestroyed(FindPosition(pos)).name != "nope") return;
        if (creatingSnake && canPlay)
        {

            Vector3 pz = Camera.main.ScreenToWorldPoint(Input.mousePosition);

            bool goodCase = false;

            int findCaseX = Mathf.RoundToInt(FindPosition(pz)[0]);
            int findCaseY = Mathf.RoundToInt(FindPosition(pz)[1]);

            print(playerRank);

            if (playerRank == 1)
            {


                if (findCaseX == 18 && findCaseY == 35)
                {
                    goodCase = true;
                    creatingSnakeName = "a";

                }
                if (findCaseX == 18 && findCaseY == 36)
                {
                    goodCase = true;
                    creatingSnakeName = "b";

                }


                if (findCaseX == 19 && findCaseY == 37)
                {
                    goodCase = true;
                    creatingSnakeName = "c";

                }
                if (findCaseX == 19 && findCaseY == 34)
                {
                    goodCase = true;
                    creatingSnakeName = "d";

                }


                if (findCaseX == 20 && findCaseY == 37)
                {
                    goodCase = true;
                    creatingSnakeName = "e";

                }
                if (findCaseX == 20 && findCaseY == 34)
                {
                    goodCase = true;
                    creatingSnakeName = "f";

                }


                if (findCaseX == 21 && findCaseY == 36)
                {
                    goodCase = true;
                    creatingSnakeName = "g";

                }
                if (findCaseX == 21 && findCaseY == 35)
                {
                    goodCase = true;
                    creatingSnakeName = "h";

                }


            }
            else if (playerRank == 2)
            {
                print("this is player2");


                if (findCaseX == 18 && findCaseY == 35 - 32)
                {
                    goodCase = true;
                    creatingSnakeName = "i";

                }
                if (findCaseX == 18 && findCaseY == 36 - 32)
                {
                    goodCase = true;
                    creatingSnakeName = "j";

                }


                if (findCaseX == 19 && findCaseY == 37 - 32)
                {
                    goodCase = true;
                    creatingSnakeName = "k";

                }
                if (findCaseX == 19 && findCaseY == 34 - 32)
                {
                    goodCase = true;
                    creatingSnakeName = "l";

                }


                if (findCaseX == 20 && findCaseY == 37 - 32)
                {
                    goodCase = true;
                    creatingSnakeName = "m";

                }
                if (findCaseX == 20 && findCaseY == 34 - 32)
                {
                    goodCase = true;
                    creatingSnakeName = "n";

                }


                if (findCaseX == 21 && findCaseY == 36 - 32)
                {
                    goodCase = true;
                    creatingSnakeName = "o";

                }
                if (findCaseX == 21 && findCaseY == 35 - 32)
                {
                    goodCase = true;
                    creatingSnakeName = "p";

                }
            }






                if (!isAnyThingHere(FindPosition(pz)))
                {
                    print("creating second head");
                    if (goodCase && snakeLock == null)
                    {
                        Vector2 snakePos = NormalizePosition(FindPosition(pz));
                        //Create a snake in the list CreatingSnakes, so it can be cancelled before validating the turn
                        CreatingSnakes creatingSnakes = new CreatingSnakes();
                        creatingSnakes.name = creatingSnakeName;
                        creatingSnakes.addCube(FindPosition(pz), true);
                        print("creating snake's name is " + creatingSnakes.name);

                        CreatingSnakesList.Add(creatingSnakes);
                        canValidate = true;
                        canPlay = false;
                    }


                }
            
            creatingSnake = false;

        }
    }

    public bool IsThisSnakeOurs(string str)
    {
        if(playerRank == 1)
        {
            foreach (string strTwo in alphabet)
            {
                if (strTwo == str) return true;
            }
        }else if(playerRank == 2)
        {
            foreach (string strTwo in alphabet2)
            {
                if (strTwo == str) return true;

            }
        }

        return false;
    }


    public void ExtendSnake(Vector2 pos)
    {
        print("canPlay: " + canPlay);
        if (!canPlay) return;

        Vector2 FoundPosition = FindPosition(pos);
        if (playerRank == 1)
        {
            Vector2 firstCheck = new Vector2(19, 35);
            Vector2 secCheck = new Vector2(20, 35);

            Vector2 thirdCheck = new Vector2(19, 36);
            Vector2 fourthCheck = new Vector2(20, 36);

            if (FoundPosition == firstCheck || FoundPosition == secCheck || FoundPosition == thirdCheck || FoundPosition == fourthCheck) return;
        }
        else
        {
            Vector2 firstCheck = new Vector2(19, 4);
            Vector2 secCheck = new Vector2(20, 4);

            Vector2 thirdCheck = new Vector2(19, 3);
            Vector2 fourthCheck = new Vector2(20, 3);

            if (FoundPosition == firstCheck || FoundPosition == secCheck || FoundPosition == thirdCheck || FoundPosition == fourthCheck) return;

        }
        Snake SnakeToAdd = GetSnakeFromPosition(FoundPosition);
        CreatingSnakes CreatingSnakesToAdd = GetCreatingSnakesFromPosition(FoundPosition);
        CreatingSnakes CreatingSnakesButTheWhole = GetCreatingSnakesFromPositionButTheWholeOne(FoundPosition);
        print("ExtendingSnake");
        bool goodSnake = true;
        print("creating snakes values: " + GetCreatingSnakesFromPosition(FoundPosition));
        if (CreatingSnakesToAdd != null) print("creating snakes is null");
        else print("it is not nullllllsqdqdq");
        //these conditions are used to check if we dont create a tail part on ourselves.
        if (tailLimit < 5 && !isAnyCreatingSnakesHere(FoundPosition))
        {
    
            
            // here maybe replce it by the CreatingSnakesToAdd.name
            if (CreatingSnakesButTheWhole.name == "nope" )
            {

                if (CreatingSnakesList.Count != 0) return;
                print("creating a new creating snake");

                //we initalize the snakeLock so they cant move 2 snakes at the same time;
                if (SnakeToAdd.name != "nope" && snakeLock == null)
                {
                    if (!IsThisSnakeOurs(SnakeToAdd.name)) return;

                    snakeLock = SnakeToAdd.name;
                    print("snakeName: " + SnakeToAdd.name);
                }

                print("snakeToAddName: " + snakeLock);
                //if there are no creatingSnakes near, we create a new one instead;
                if (!IsThisSnakeOurs(snakeLock)) return;
                CreatingSnakes newCreatingSnakes = new CreatingSnakes();
                newCreatingSnakes.name = snakeLock;
                newCreatingSnakes.addCube(FoundPosition, true);
                CreatingSnakesList.Add(newCreatingSnakes);
                tailLimit++;
                canValidate = true;

            }

            else
            {
                //whenever a creating snake is already created at this pos, this get callled
                print("a creating snake is already created");
                //we need to add this here so it doesnt pick the head of the snake but the one of the Creating Snakes 
                bool crtSnk = false;
                if (CreatingSnakesButTheWhole.name != "nope")
                {
                    print("Creating snakes but the whole name is not null");
                    if (IsAnyCreatingSnakesFromPositionButTheWholeOneBool(FoundPosition)) { print("this shits good fn"); crtSnk = true; }
                    else print("this shit isnt gooddd");
                 }
                else print("The name is null");


                if (snakeLock == CreatingSnakesToAdd.name && crtSnk)
                {

                    CreatingSnakesToAdd.addCube(FoundPosition, true);
                    tailLimit++;
                    canValidate = true;

                }
            }
            //}
        }
        print("taillimit is ok");

        }

    [PunRPC]
    public void onEndTurn()
    {
        //Instantiate who plays first when the WhichTurn isnt initialized

        if (WhichTurn == 1)
        {
            textYouCanPlay.SetActive(true);

            canPlay = true;
            
            WhichTurn = 2;
        }
        else if (WhichTurn == 2)
        {
            textYouCanPlay.SetActive(false);

            TransformCreatingToExisting();
            canPlay = false;
            WhichTurn = 1;

        }
        creatingSnake = false;
       
        snakeLock = null;
        tailLimit = 0;

        print(WhichTurn);
        print(canPlay);

    }
    public void Abort()
    {
        popup.SetActive(false);
        abortButton.SetActive(false);
        closeThePopupButton.SetActive(false);
        int playerNum;
        if (playerRank == 1) playerNum = 0;
        else playerNum = 1;
        photonView.RPC("Win", RpcTarget.All, playerNum);
    }
    public void CloseThePopup()
    {
        popup.SetActive(false);
        abortButton.SetActive(false);
        closeThePopupButton.SetActive(false);

    }

    public void onValidateee()
    {
        if(!canValidate)
        {
            popup.SetActive(true);
            abortButton.SetActive(true);
            closeThePopupButton.SetActive(true);
            tailLimit = 0;
            creatingSnake = false;
            return; 
        }
        creatingSnake = false;

        snakeLock = null;
        tailLimit = 0;
        print(WhichTurn);

        //TO REMOVE AFTER
        //canPlay = true;
        //TransformCreatingToExisting();
        // = true;
        if (WhichTurn == 2)

            photonView.RPC("onEndTurn", RpcTarget.All);

    }

    public void OnEndTurnNotNetworked()
    {
        //Instantiate who plays first when the WhichTurn isnt initialized

        if (WhichTurn == 1)
        {
            textYouCanPlay.SetActive(true);
            canPlay = true;
            WhichTurn = 2;
        }
        else if(WhichTurn == 2)
        {
            textYouCanPlay.SetActive(false);
            canPlay = false;
            WhichTurn = 1;

        }
        print(WhichTurn);
        print(canPlay);

    }
        

    public void OnClickedCreateSnake()
    {
        creatingSnake = true;

    }

    void Update()
    {

        Vector3 pos = Camera.main.ScreenToWorldPoint(Input.mousePosition);
        if (Input.GetMouseButton(0))
        {
            if (creatingSnake) CreateSnake();


            if (isAnyThingNearToTheHead(FindPosition(pos)))
            {
                ExtendSnake(pos);

            }

        }
        else if (Input.GetKey(KeyCode.KeypadEnter) || Input.GetKey(KeyCode.Return))
            onValidateee();
                


    }
}