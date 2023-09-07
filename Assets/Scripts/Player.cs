using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public enum EPlayerTurn
{
    None, //¿ÕÆå
    Black,//ºÚÆå
    White,//°×Æå

    

}
public class Player : MonoBehaviour
{
    public EPlayerTurn MyTurn;

    private void Update()
    {
        PlayChess();
    }

    public void PlayChess()
    {
        if (Input.GetMouseButtonDown(0)&&MyTurn==GameManager.Instance.cureentTurn)
        {
            Vector3 mousePosition = Input.mousePosition;
            mousePosition.z = -Camera.main.transform.position.z;
            Vector2 pos = Camera.main.ScreenToWorldPoint(mousePosition);
            if (pos.x < 0 || pos.y < 0 || pos.x > 14 || pos.y > 14)
            {
                return;
            }
            pos.x = Mathf.Round(pos.x);
            pos.y = Mathf.Round(pos.y);
            PlayChessMsg playChessMsg = new PlayChessMsg();
            playChessMsg.color = (int)MyTurn;
            playChessMsg.x =(int) pos.x;
            playChessMsg.y =(int) pos.y;
            NetMgr.Instance.Send(playChessMsg);
            print(pos);
        }
       
    }

  


}


