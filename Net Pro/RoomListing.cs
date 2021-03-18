using Photon.Realtime;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using Photon.Pun;

public class RoomListing : MonoBehaviour
{
    [SerializeField]
    private Text nameText;
    [SerializeField]
    private Text sizeText;
        
    GameObject menusManager;
    
    public RoomInfo roomInfo { get; private set; }

    private void Start()
    {
        menusManager = GameObject.Find("UIManager");
    }

    public void SetRoom(RoomInfo room)
    {
        roomInfo = room;
              
        nameText.text = room.Name;
        sizeText.text = room.PlayerCount.ToString() + "/" + room.MaxPlayers.ToString();
    }    

    public void JoinRoom()
    {
        PhotonNetwork.JoinRoom(roomInfo.Name);

        MenusManager menusScript = menusManager.GetComponent<MenusManager>();
        menusScript.ToggleMenu(menusManager.transform.GetChild(2).gameObject);           
    }
}
