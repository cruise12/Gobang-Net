using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GameManager : MonoBehaviour
{
    public static GameManager Instance;
    public int[,] chessBoard;
    public GameObject black;
    public GameObject white;
    public EPlayerTurn cureentTurn;
    private void Awake()
    {
        Instance = this;
        chessBoard = new int[15, 15];
    }


    public void CreateChess(Vector2 pos, EPlayerTurn playerTurn)
    {
        if (chessBoard[(int)pos.x, (int)pos.y] == 0 && playerTurn == cureentTurn)
        {
            if (playerTurn == EPlayerTurn.Black)
            {

             Instantiate(black, pos, Quaternion.identity);
            //    chessBoard[(int)pos.x, (int)pos.y] = (int)playerTurn;
            //    CheckLine(pos, new int[] { 1, 0 });
            //    CheckLine(pos, new int[] { 0, 1 });
            //    CheckLine(pos, new int[] { 1, 1 });
            //    CheckLine(pos, new int[] { 1, -1 });
              cureentTurn = EPlayerTurn.White;

            }
            else if (playerTurn == EPlayerTurn.White)
            {
                Instantiate(white, pos, Quaternion.identity);
                //chessBoard[(int)pos.x, (int)pos.y] = (int)playerTurn;
                //CheckLine(pos, new int[] { 1, 0 });
                //CheckLine(pos, new int[] { 0, 1 });
                //CheckLine(pos, new int[] { 1, 1 });
                //CheckLine(pos, new int[] { 1, -1 });
                cureentTurn = EPlayerTurn.Black;
            }
        }


    }

   // public void CheckLine(Vector2 pos, int[] offset)
    //{
    //    int Num = 1;
    //    for (int i = 0; i < 5; i++)
    //    {
    //        if (pos.x + offset[0] > 14 || pos.x + offset[0] < 0 || pos.y + offset[1] > 14 || pos.y + offset[1] < 0)
    //        {
    //            Num = 1;
    //            break;//退出循环
    //        }
    //        if (chessBoard[((int)pos.x + offset[0]), ((int)pos.y + offset[1])] == (float)cureentTurn)
    //        {
    //            Num += 1;
    //            pos.x += offset[0];
    //            pos.y += offset[1];
    //            if (Num == 5)
    //            {
    //                print("GameOver");
    //                return;
    //            }
    //        }
    //        else
    //        {
    //            Num = 1;
    //            break;
    //        }

    //    }

    //    for (int i = 0; i < 5; i++)
    //    {
    //        if (pos.x - offset[0] > 14 || pos.x - offset[0] < 0 || pos.y - offset[1] > 14 || pos.y - offset[1] < 0)
    //        {
    //            Num = 1;
    //            break;//退出循环
    //        }
    //        if (chessBoard[((int)pos.x - offset[0]), ((int)pos.y - offset[1])] == (float)cureentTurn)
    //        {
    //            Num += 1;
    //            pos.x -= offset[0];
    //            pos.y -= offset[1];
    //            if (Num == 5)
    //            {
    //                print("GameOver");
    //                return;
    //            }
    //        }
    //        else
    //        {
    //            Num = 1;
    //            break;
    //        }
    //    }

    //}

}
    
 

