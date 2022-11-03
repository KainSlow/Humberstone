using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class LevelGeneration : MonoBehaviour
{
    [SerializeField] GameObject oPlayer;
    [SerializeField] GameObject oCam;
    [SerializeField] public GameObject[] rooms; // index 0 --> closed, index 1 --> LR, index 2 --> LRB, index 3 --> LRT, index 4 --> LRBT
    [SerializeField] GameObject FillRoom;
    [SerializeField] LayerMask whatIsRoom;
    [SerializeField] float moveIncrement;
    [SerializeField] int matrixWidth;
    [SerializeField] int matrixHeight;

    [SerializeField] GameObject[] objs;

    enum Rooms
    {
        Closed,
        LR,
        LRB,
        LRT,
        LRBT
    };

    private int direction; // 1 & 2 -> right, 3 & 4 -> left, 5 -> down
    public bool stopGeneration;
    private int[,] matrix;

    private Vector3 startPos; 


    private  int cX;
    private  int cY;

    bool genObj;
    int ObjPosX;
    int ObjPosY;


    private void Start()
    {
        matrix = new int[matrixHeight, matrixWidth];

        GenMatrix();
        GeneratePath();
        VerifyPath();
        FillMatrix();
        ReSizeMatrix();

        oPlayer.transform.position = startPos;
        oCam.transform.position = startPos;

        cX = 0;
        cY = 0;


        if (PlayerGlobals.Instance.Day % 2 == 0)
        {
            genObj = true;
        }
    }

    private void Update()
    {
        /*if (Input.GetKeyDown(KeyCode.Space))
        {
            SceneManager.LoadScene(SceneManager.GetActiveScene().buildIndex);
        }
        */


        if (cY < matrixHeight+2)
        {
            if (cX < matrixWidth+2)
            {
                if(cY == 0 || cX == 0 || cX == matrixWidth+1 || cY == matrixHeight +1)
                {
                    Instantiate(FillRoom, new Vector3(cX * moveIncrement, -cY * moveIncrement, 0f), Quaternion.identity, transform);
                }
                else
                {
                    if(matrix[cY,cX] == 0)
                    {
                        Instantiate(FillRoom, new Vector3(cX * moveIncrement, -cY * moveIncrement, 0f), Quaternion.identity, transform);
                    }
                    else
                    {
                        Instantiate(rooms[matrix[cY, cX]], new Vector3(cX * moveIncrement, -cY * moveIncrement, 0f), Quaternion.identity, transform);

                        if (genObj)
                        {
                            int objType = 0;
                            for(int i = 0; i < 3; i++)
                            {
                                if(PlayerGlobals.Instance.isObjCollected[i] == false)
                                {
                                    objType = i;
                                    break;
                                }
                            }
                            Instantiate(objs[objType], new Vector3( (ObjPosX * moveIncrement), (-ObjPosY * moveIncrement), 0f), Quaternion.identity, null);
                            genObj = false;
                        }
                    }
                }

                cX++;
            }
            else
            {
                cY++;
                cX = 0;
            }
        }
        else
        {
            stopGeneration = true;
        }
    }


    private void GenMatrix()
    {
        for(int i = 0; i < matrixHeight; i++)
        {
            for(int k = 0; k < matrixWidth; k++)
            {
                //matrix[i, k] = Random.Range(0, 5);
                matrix[i, k] = 0;
            }
        }
    }

    private void GeneratePath()
    {
        cY = 0;
        cX = Random.Range(0, matrixWidth);
        startPos = new Vector3( (cX+1) * moveIncrement, -moveIncrement, 0);

        direction = GetNextMove();


        while (cY < matrixHeight)
        {
            matrix[cY, cX] = -1;

            if(cY == matrixHeight/2)
            {
                ObjPosX = cX + 1;
                ObjPosY = cY + 1;
            }


            if(direction == 1 || direction == 2) //MOVE RIGHT
            {
                if(cX < matrixWidth-1)
                {
                    cX++;

                    direction = GetNextMove();

                    if(direction == 3)
                    {
                        direction = 1;
                    }
                    else if(direction == 4)
                    {
                        direction = 5;
                    }

                }
                else
                {
                    direction = 5;
                }
            }
            else if(direction == 3 || direction == 4)
            {
                if(cX > 0)
                {
                    cX--;

                    direction = Random.Range(3,6);

                }
                else
                {
                    direction = 5;
                }
            }
            else if(direction == 5)
            {
                cY++;
                direction = GetNextMove();
            }

        }

    }

    private void VerifyPath()
    {
        for(int i = 0; i < matrixHeight; i++)
        {
            for(int k = 0; k < matrixWidth; k++)
            {
                if(matrix[i,k] == -1)
                {
                    matrix[i, k] = VerifyCell(k, i);
                }
            }
        }
    }

    private int GetNextMove()
    {
        return Random.Range(1, 6);
    }

    private int VerifyCell(int x, int y)
    {
        int roomTyme = -1;

        if (y == 0)
        {
            if (x == 0)
            {
                if (matrix[y, x + 1] != 0 && matrix[y + 1, x] != 0)
                {
                    roomTyme = (int)Rooms.LRB;
                }
                else if (matrix[y, x + 1] != 0)
                {
                    roomTyme = (int)Rooms.LR;
                }
                else if (matrix[y + 1, x] != 0)
                {
                    roomTyme = (int)Rooms.LRB;
                }

            }
            else if (x == matrixWidth - 1)
            {
                if (matrix[y, x - 1] != 0 && matrix[y + 1, x] != 0)
                {
                    roomTyme = (int)Rooms.LRB;
                }
                else if (matrix[y, x - 1] != 0)
                {
                    roomTyme = (int)Rooms.LR;
                }
                else if (matrix[y + 1, x] != 0)
                {
                    roomTyme = (int)Rooms.LRB;

                }
            }
            else
            {
                if (matrix[y, x - 1] != 0 && matrix[y, x + 1] != 0 && matrix[y + 1, x] != 0)
                {
                    roomTyme = (int)Rooms.LR;
                }
                else if (matrix[y, x - 1] != 0 && matrix[y, x + 1] != 0)
                {
                    roomTyme = (int)Rooms.LR;
                }
                else if (matrix[y, x - 1] != 0 || matrix[y, x + 1] != 0)
                {
                    if (matrix[y + 1, x] != 0)
                    {
                        roomTyme = (int)Rooms.LRB;
                    }
                    else
                    {
                        roomTyme = (int)Rooms.LR;
                    }
                }
                else if(matrix[y+1,x] != 0)
                {
                    roomTyme = (int)Rooms.LRB;
                }

            }
        }
        else if (y == matrixHeight - 1)
        {
            if (x == 0)
            {
                if (matrix[y, x + 1] != 0 && matrix[y - 1, x] != 0)
                {
                    roomTyme = (int)Rooms.LRT;
                }
                else if (matrix[y, x + 1] != 0)
                {
                    roomTyme = (int)Rooms.LR;
                }
                else if (matrix[y - 1, x] != 0)
                {
                    roomTyme = (int)Rooms.LRT;
                }

            }
            else if (x == matrixWidth - 1)
            {
                if (matrix[y, x - 1] != 0 && matrix[y - 1, x] != 0)
                {
                    roomTyme = (int)Rooms.LRT;
                }
                else if (matrix[y, x - 1] != 0)
                {
                    roomTyme = (int)Rooms.LR;
                }
                else if (matrix[y - 1, x] != 0)
                {
                    roomTyme = (int)Rooms.LRT;

                }
            }
            else
            {
                if (matrix[y, x - 1] != 0 && matrix[y, x + 1] != 0 && matrix[y - 1, x] != 0)
                {
                    roomTyme = (int)Rooms.LR;
                }
                else if (matrix[y, x - 1] != 0 && matrix[y, x + 1] != 0)
                {
                    roomTyme = (int)Rooms.LR;
                }
                else if (matrix[y, x - 1] != 0 || matrix[y, x + 1] != 0)
                {
                    if (matrix[y - 1, x] != 0)
                    {
                        roomTyme = (int)Rooms.LRT;
                    }
                    else
                    {
                        roomTyme = (int)Rooms.LR;
                    }
                }
                else if(matrix[y-1,x] != 0)
                {
                    roomTyme = (int)Rooms.LRT;
                }
            }
        }
        else
        {
            if (x == 0)
            {
                if (matrix[y - 1, x] != 0 && matrix[y + 1, x] != 0 && matrix[y, x + 1] != 0)
                {
                    //Left Upper/Lower corner
                    roomTyme = (int)Rooms.LRBT;

                }
                else if (matrix[y - 1, x] != 0 && matrix[y + 1, x] != 0)
                {
                    roomTyme = (int)Rooms.LRBT;
                }
                else if (matrix[y - 1, x] != 0 && matrix[y, x + 1] != 0)
                {
                    roomTyme = (int)Rooms.LRT;
                }
                else if (matrix[y + 1, x] != 0 && matrix[y, x + 1] != 0)
                {
                    roomTyme = (int)Rooms.LRB;
                }

            }
            else if (x == matrixWidth - 1)
            {
                if (matrix[y - 1, x] != 0 && matrix[y + 1, x] != 0 && matrix[y, x - 1] != 0)
                {
                    //Right Upper/Lower corner
                    roomTyme = (int)Rooms.LRBT;

                }
                else if (matrix[y - 1, x] != 0 && matrix[y + 1, x] != 0)
                {
                    roomTyme = (int)Rooms.LRBT;
                }
                else if (matrix[y - 1, x] != 0 && matrix[y, x - 1] != 0)
                {
                    roomTyme = (int)Rooms.LRT;
                }
                else if (matrix[y + 1, x] != 0 && matrix[y, x - 1] != 0)
                {
                    roomTyme = (int)Rooms.LRB;
                }
            }
            else
            {
                if (matrix[y, x + 1] != 0 && matrix[y, x - 1] != 0)
                {
                    roomTyme = (int)Rooms.LR;
                }
                else if (matrix[y, x + 1] != 0 || matrix[y, x - 1] != 0)
                {
                    if (matrix[y - 1, x] != 0 && matrix[y + 1, x] != 0)
                    {
                        roomTyme = (int)Rooms.LRBT;
                    }
                    else if (matrix[y - 1, x] != 0)
                    {
                        roomTyme = (int)Rooms.LRT;
                    }
                    else if (matrix[y + 1, x] != 0)
                    {
                        roomTyme = (int)Rooms.LRB;
                    }
                    else
                    {
                        roomTyme = (int)Rooms.LRBT;
                    }

                }
                else
                {
                    roomTyme = (int)Rooms.LRBT;
                }
            }
        }

        return roomTyme;

    }

    private void FillMatrix()
    {
        for(int y = 0; y < matrixHeight; y++)
        {
            for(int x = 0; x < matrixWidth; x++)
            {
                if(matrix[y,x] == 0)
                {
                    int chance = Random.Range(0,2);

                    if(chance == 1)
                    {
                        matrix[y, x] = Random.Range(1, 5);
                    }
                }
            }
        }
    }
    private void ReSizeMatrix()
    {
        int[,] matrixBackUp = matrix;

        matrix = new int[matrixHeight + 2, matrixWidth + 2];

        for(int i = 0; i < matrixHeight + 2; i++)
        {
            for(int k = 0; k < matrixWidth + 2; k++)
            {
                if(i > 0 && i < matrixHeight + 1 && k > 0 && k < matrixWidth + 1)
                {
                    matrix[i, k] = matrixBackUp[i - 1, k - 1];
                }
                else
                {
                    matrix[i, k] = 0;
                }

            }
        }

    }
}