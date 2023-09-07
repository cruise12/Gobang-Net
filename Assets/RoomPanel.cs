using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class RoomPanel : MonoBehaviour
{
    public GameObject room;
    public GameObject startPanel;
    public Button quit;
    public Button preparation;
    void Start()
    {
        quit.onClick.AddListener(() =>
        {
            room.SetActive(false);
            startPanel.SetActive(false);
        });
        preparation.onClick.AddListener(() =>
        {
            PreMsg preMsg = new PreMsg();
            NetMgr.Instance.Send(preMsg);
        });

    }

    // Update is called once per frame
    void Update()
    {
        
    }
}
