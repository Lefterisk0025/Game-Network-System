using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using Photon.Pun;
using Photon.Realtime;

public class RoomsListManager : MonoBehaviourPunCallbacks
{
    //Create room form
    [SerializeField]
    private Dropdown sizeDp;
    [SerializeField]
    private Toggle isPrivate;

    //Room List
    [SerializeField]
    private Transform roomsContainerPanel;
    [SerializeField]
    private RoomListing roomListingPrefab;
   
    [SerializeField]
    private InputField codeIput;    

    private List<RoomInfo> listings = new List<RoomInfo>(); 

    public void CreateRoom()
    {
        RoomOptions roomOptions = new RoomOptions();

        roomOptions.MaxPlayers = (byte)(sizeDp.value + 2);
        string roomName = RandomString();
        if (isPrivate.isOn)
            roomOptions.IsVisible = false;
        else
            roomOptions.IsVisible = true;

        PhotonNetwork.CreateRoom(roomName, roomOptions);
    }

    public override void OnCreatedRoom()
    {
        Debug.Log("Room created");
    }

    public override void OnRoomListUpdate(List<RoomInfo> roomList)
    {
        int tempIndex;
        foreach (RoomInfo room in roomList)
        {
            if(listings != null)
            {
                tempIndex = listings.FindIndex(ByName(room.Name));
            }
            else
            {
                tempIndex = -1;
            }
            if(tempIndex != -1)
            {
                listings.RemoveAt(tempIndex);
                Destroy(roomsContainerPanel.GetChild(tempIndex).gameObject);
            }
            if(room.PlayerCount > 0)
            { 
                if(room.IsOpen && room.IsVisible)
                {
                    RoomListing tempRoom = Instantiate(roomListingPrefab, roomsContainerPanel);
                    tempRoom.SetRoom(room);
                    listings.Add(room);
                }               
            }
        }
    } 
    
    static System.Predicate<RoomInfo> ByName(string name)
    {
        return delegate (RoomInfo room)
        {
            return room.Name == name;
        };
    }
      
    public void JoinPrivateRoom()
    {
        if(codeIput.text != null)
            PhotonNetwork.JoinRoom(codeIput.text);
    }   

    private string RandomString()
    {
        int _stringLength = 7;
        string randomString = "";
        string[] characters = new string[] { "a", "b", "c", "d", "e", "f", "g", "h", "i", "j", "k", "l", "m", "n", "o", "p", "q", "r", "s", "t", "u", "v", "w", "x", "y", "z" };
        for (int i = 0; i <= _stringLength; i++)
        {
            randomString = randomString + characters[UnityEngine.Random.Range(0, characters.Length)];
        }
        return randomString;
    }   
}
