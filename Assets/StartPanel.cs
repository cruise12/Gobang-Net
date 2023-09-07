using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class StartPanel : MonoBehaviour
{
    public CanvasGroup canvasGroup;
    public GameObject room;
    public Text id;
    public Text roomID;
    public Button EnterButton;
    void Start()
    {
        EnterButton.onClick.AddListener(() =>
        {
            
            EnterRoomMsg enterRoomMsg = new EnterRoomMsg();
            enterRoomMsg.id = int.Parse(id.text);
            enterRoomMsg.RoomId = int.Parse(roomID.text);
            NetMgr.Instance.Send(enterRoomMsg);
            canvasGroup.alpha = 0;
            room.SetActive(true);
        });
    }

    // Update is called once per frame
    void Update()
    {
        
    }
}
