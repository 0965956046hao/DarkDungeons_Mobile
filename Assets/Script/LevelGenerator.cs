using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;
using Photon.Pun;

public class LevelGenerator : MonoBehaviour
{
    public GameObject layoutRoom;
    public Color startColor, endColor, shopColor, chestRoomColor;

    public int distanceToEnd;
    public bool includeShop;
    public int minDistanceToShop, maxDistanceToShop;
    public bool includeChest;
    public int minDistanceToChestRoom, maxDistanceToChestRoom;

    public Transform generatorPoint;

    public enum Direction { up, right, down, left};
    public Direction selectedDirection;

    public float xOffset = 18f, yOffset = 10f;

    public LayerMask whatIsRoom;

    private GameObject endRoom, shopRoom, chestRoom;

    private List<GameObject> layoutRoomObject = new List<GameObject>();

    public RoomPrefabs rooms;

    private List<GameObject> generateOutLines = new List<GameObject>();

    public RoomCenter centerStart, centerEnd, shopCenter, centerChestRoom;
    public RoomCenter[] potentialCenters;
    public bool cretedLevel;
    public float waitToCreate;



    PhotonView view;

    // Start is called before the first frame update
    void Start()
    {
        waitToCreate = 1;
        cretedLevel = false;
        
    }


            // Update is called once per frame
            void Update()
            {
#if UNITY_EDITOR
                if (Input.GetKey(KeyCode.R))
                {
                    SceneManager.LoadScene(SceneManager.GetActiveScene().name);
                }
#endif
                if (waitToCreate > 0)
                    waitToCreate -= Time.deltaTime;
                else if(cretedLevel == false)
                {
                    view = GetComponent<PhotonView>();
                    if (PhotonNetwork.IsMasterClient)
                    {

                        PhotonNetwork.Instantiate(layoutRoom.name, generatorPoint.position, generatorPoint.rotation).GetComponent<SpriteRenderer>().color = startColor;
                        selectedDirection = (Direction)Random.Range(0, 4);
                        MoveGenerationPoint();



                        for (int i = 0; i < distanceToEnd; i++)
                        {
                            GameObject newRoom = PhotonNetwork.Instantiate(layoutRoom.name, generatorPoint.position, generatorPoint.rotation);

                            layoutRoomObject.Add(newRoom);

                            if (i + 1 == distanceToEnd)
                            {
                                newRoom.GetComponent<SpriteRenderer>().color = endColor;
                                layoutRoomObject.RemoveAt(layoutRoomObject.Count - 1);
                                endRoom = newRoom;
                            }

                            selectedDirection = (Direction)Random.Range(0, 4);
                            MoveGenerationPoint();

                            while (Physics2D.OverlapCircle(generatorPoint.position, 0.2f, whatIsRoom))
                            {
                                MoveGenerationPoint();
                            }
                        }

                        if (includeShop)
                        {
                            int shopSelector = Random.Range(minDistanceToShop, maxDistanceToShop + 1);

                            shopRoom = layoutRoomObject[shopSelector];
                            layoutRoomObject.RemoveAt(shopSelector);
                            shopRoom.GetComponent<SpriteRenderer>().color = shopColor;
                        }

                        if (includeChest)
                        {
                            int chestSelector = Random.Range(minDistanceToChestRoom, maxDistanceToChestRoom);

                            chestRoom = layoutRoomObject[chestSelector];
                            layoutRoomObject.RemoveAt(chestSelector);
                            chestRoom.GetComponent<SpriteRenderer>().color = chestRoomColor;
                        }

                        //create room outline
                        CreateRoomOutLine(Vector3.zero);
                        foreach (GameObject room in layoutRoomObject)
                        {
                            CreateRoomOutLine(room.transform.position);
                        }
                        CreateRoomOutLine(endRoom.transform.position);

                        if (includeShop)
                        {
                            CreateRoomOutLine(shopRoom.transform.position);
                        }

                        if (includeChest)
                        {
                            CreateRoomOutLine(chestRoom.transform.position);
                        }

                        //PhotonNetwork.Instantiate(listRoonOutLine.name, transform.position, transform.rotation).GetComponent<ListRoomOutLine>().listRoomOutLine.AddRange(generateOutLines);
                        foreach (GameObject outline in generateOutLines)
                        {
                            //string name = outline.name.Replace("(Clone)", "");
                            bool generateCenter = true;
                            if (outline.transform.position == Vector3.zero)
                            {
                                //PhotonView.Instantiate(centerStart, outline.transform.position, transform.rotation).theRoom = outline.GetComponent<RoomController>();
                                PhotonNetwork.Instantiate(centerStart.name, outline.transform.position, transform.rotation);//.GetComponent<RoomCenter>().theRoom = outline.GetComponent<RoomController>();
                                                                                                                            //createdCenterStart.theRoom = outline.GetComponent<RoomController>();
                                generateCenter = false;
                            }

                            if (outline.transform.position == endRoom.transform.position)
                            {
                                //PhotonView.Instantiate(centerEnd, outline.transform.position, transform.rotation).theRoom = outline.GetComponent<RoomController>();
                                PhotonNetwork.Instantiate(centerEnd.name, outline.transform.position, transform.rotation);//.GetComponent<RoomCenter>().theRoom = outline.GetComponent<RoomController>();
                                                                                                                          //createdCenterEnd.theRoom = outline.GetComponent<RoomController>();
                                generateCenter = false;
                            }

                            if (includeShop)
                            {
                                if (outline.transform.position == shopRoom.transform.position)
                                {
                                    //PhotonView.Instantiate(shopCenter, outline.transform.position, transform.rotation).theRoom = outline.GetComponent<RoomController>();
                                    PhotonNetwork.Instantiate(shopCenter.name, outline.transform.position, transform.rotation);//.GetComponent<RoomCenter>().theRoom = outline.GetComponent<RoomController>();
                                                                                                                               //createdCenterShop.theRoom = outline.GetComponent<RoomController>();
                                    generateCenter = false;
                                }
                            }

                            if (includeChest)
                            {
                                if (outline.transform.position == chestRoom.transform.position)
                                {
                                    //PhotonView.Instantiate(centerChestRoom, outline.transform.position, transform.rotation).theRoom = outline.GetComponent<RoomController>();
                                    PhotonNetwork.Instantiate(centerChestRoom.name, outline.transform.position, transform.rotation);//.GetComponent<RoomCenter>().theRoom = outline.GetComponent<RoomController>();
                                                                                                                                    //createdCenterChest.theRoom = outline.GetComponent<RoomController>();
                                    generateCenter = false;
                                }
                            }

                            if (generateCenter)
                            {
                                int selectCenter = Random.Range(0, potentialCenters.Length);
                                //PhotonView.Instantiate(potentialCenters[selectCenter], outline.transform.position, transform.rotation).theRoom = outline.GetComponent<RoomController>();
                                PhotonNetwork.Instantiate(potentialCenters[selectCenter].name, outline.transform.position, transform.rotation);//.GetComponent<RoomCenter>().theRoom = outline.GetComponent<RoomController>();
                                                                                                                                               //createdCenterRoom.theRoom = outline.GetComponent<RoomController>();
                            }
                        }
                    }
                    cretedLevel = true;
                }    

            }

            void MoveGenerationPoint()
            {
                switch (selectedDirection)
                {
                    case Direction.up:
                        {
                            generatorPoint.position += new Vector3(0f, yOffset, 0f);
                            break;
                        }


                    case Direction.down:
                        {
                            generatorPoint.position += new Vector3(0f, -yOffset, 0f);
                            break;
                        }


                    case Direction.left:
                        {
                            generatorPoint.position += new Vector3(-xOffset, 0f, 0f);
                            break;
                        }


                    case Direction.right:
                        {
                            generatorPoint.position += new Vector3(xOffset, 0f, 0f);
                            break;
                        }

                }
            }

            void CreateRoomOutLine(Vector3 roomPosittion)
            {
                bool roomAbove = Physics2D.OverlapCircle(roomPosittion + new Vector3(0f, yOffset, 0f), .2f, whatIsRoom);
                bool roomBelow = Physics2D.OverlapCircle(roomPosittion + new Vector3(0f, -yOffset, 0f), .2f, whatIsRoom);
                bool roomLeft = Physics2D.OverlapCircle(roomPosittion + new Vector3(-xOffset, 0f, 0f), .2f, whatIsRoom);
                bool roomRight = Physics2D.OverlapCircle(roomPosittion + new Vector3(xOffset, 0f, 0f), .2f, whatIsRoom);


                int directionCount = 0;
                if (roomAbove)
                {
                    directionCount++;
                }
                if (roomBelow)
                {
                    directionCount++;
                }
                if (roomLeft)
                {
                    directionCount++;
                }
                if (roomRight)
                {
                    directionCount++;
                }

                switch (directionCount)
                {
                    case 0:
                        {
                            Debug.LogError("Found no room");
                            break;
                        }

                    case 1:
                        {
                            if (roomAbove)
                            {
                                generateOutLines.Add(PhotonNetwork.Instantiate(rooms.singleUp.name, roomPosittion, transform.rotation));
                            }
                            if (roomBelow)
                            {
                                generateOutLines.Add(PhotonNetwork.Instantiate(rooms.singleDown.name, roomPosittion, transform.rotation));
                            }
                            if (roomLeft)
                            {
                                generateOutLines.Add(PhotonNetwork.Instantiate(rooms.singeLeft.name, roomPosittion, transform.rotation));
                            }
                            if (roomRight)
                            {
                                generateOutLines.Add(PhotonNetwork.Instantiate(rooms.singleRight.name, roomPosittion, transform.rotation));
                            }

                            break;
                        }


                    case 2:
                        {
                            if (roomAbove && roomLeft)
                            {
                                generateOutLines.Add(PhotonNetwork.Instantiate(rooms.doubleUpLeft.name, roomPosittion, transform.rotation));
                            }
                            if (roomBelow && roomAbove)
                            {
                                generateOutLines.Add(PhotonNetwork.Instantiate(rooms.doubleUpDown.name, roomPosittion, transform.rotation));
                            }
                            if (roomAbove && roomRight)
                            {
                                generateOutLines.Add(PhotonNetwork.Instantiate(rooms.doubleUpRight.name, roomPosittion, transform.rotation));
                            }
                            if (roomRight && roomLeft)
                            {
                                generateOutLines.Add(PhotonNetwork.Instantiate(rooms.doubleRightLeft.name, roomPosittion, transform.rotation));
                            }
                            if (roomBelow && roomLeft)
                            {
                                generateOutLines.Add(PhotonNetwork.Instantiate(rooms.doubleLeftDown.name, roomPosittion, transform.rotation));
                            }
                            if (roomRight && roomBelow)
                            {
                                generateOutLines.Add(PhotonNetwork.Instantiate(rooms.doubleRightDown.name, roomPosittion, transform.rotation));
                            }

                            break;
                        }


                    case 3:
                        {
                            if (roomAbove && roomLeft && roomBelow)
                            {
                                generateOutLines.Add(PhotonNetwork.Instantiate(rooms.tripleUpLeftDown.name, roomPosittion, transform.rotation));
                            }
                            if (roomRight && roomLeft && roomBelow)
                            {
                                generateOutLines.Add(PhotonNetwork.Instantiate(rooms.tripleDownRightLeft.name, roomPosittion, transform.rotation));
                            }
                            if (roomBelow && roomRight && roomAbove)
                            {
                                generateOutLines.Add(PhotonNetwork.Instantiate(rooms.tripleUpRightDown.name, roomPosittion, transform.rotation));
                            }
                            if (roomRight && roomAbove && roomLeft)
                            {
                                generateOutLines.Add(PhotonNetwork.Instantiate(rooms.tripleUpRightLeft.name, roomPosittion, transform.rotation));
                            }
                            break;
                        }

                    case 4:
                        {
                            if (roomRight && roomAbove && roomLeft && roomBelow)
                            {
                                generateOutLines.Add(PhotonNetwork.Instantiate(rooms.fourWays.name, roomPosittion, transform.rotation));
                            }
                            break;
                        }
                }
            }
        

        }
   


[System.Serializable]
public class RoomPrefabs
{
    public GameObject singleUp, singleDown, singleRight, singeLeft,
        doubleUpDown, doubleUpLeft, doubleUpRight, doubleRightLeft, doubleRightDown, doubleLeftDown,
        tripleUpRightDown, tripleUpLeftDown, tripleUpRightLeft, tripleDownRightLeft, fourWays;
}




