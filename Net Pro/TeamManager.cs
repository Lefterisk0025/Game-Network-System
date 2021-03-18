using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using Photon.Pun;
using Photon.Realtime;
using System;

public class TeamManager : MonoBehaviourPunCallbacks
{
    [SerializeField]
    private Text nameText;
    [SerializeField]
    private Text sizeText;
    [SerializeField]
    private Button BeginBtn;

    [SerializeField]
    private Transform playersContainer;
    [SerializeField]
    private PlayerListing playerListingPrefab;

    private List<PlayerListing> listings = new List<PlayerListing>();

    public override void OnJoinedRoom()
    {
        ClearPlayerListing();
        ListPlayers();
        UpdateRoomUI();
    }

    public override void OnLeftRoom()
    {                
        UpdateRoomUI();
    }

    public override void OnPlayerEnteredRoom(Player newPlayer)
    {
        ClearPlayerListing();
        ListPlayers();
        UpdateRoomUI();
    }

    public override void OnPlayerLeftRoom(Player otherPlayer)
    {
        ClearPlayerListing();
        ListPlayers();
        UpdateRoomUI();
    }

    private void UpdateRoomUI()
    {       
        nameText.text = PhotonNetwork.CurrentRoom.Name;
        sizeText.text = PhotonNetwork.CurrentRoom.PlayerCount.ToString() + "/" + PhotonNetwork.CurrentRoom.MaxPlayers.ToString();      

        if (PhotonNetwork.IsMasterClient)
            BeginBtn.gameObject.SetActive(true);
        else
            BeginBtn.gameObject.SetActive(false);
    }

    void ClearPlayerListing()
    {
        for (int i = playersContainer.childCount - 1; i >= 0; i--)
            Destroy(playersContainer.GetChild(i).gameObject);
    }

    void ListPlayers()
    {
        foreach (Player player in PhotonNetwork.PlayerList)
        {
            PlayerListing tempListing = Instantiate(playerListingPrefab, playersContainer);
            tempListing.SetPlayer(player);
        }
    }

    public void StartGame()
    {
        if(PhotonNetwork.IsMasterClient)
        {
            PhotonNetwork.CurrentRoom.IsOpen = false;
            PhotonNetwork.LoadLevel("Level1");
        }
    }

    IEnumerator reJoinLobby()
    {
        yield return new WaitForSeconds(1);
        PhotonNetwork.JoinLobby();
    }

    public void LeaveRoom()
    {
        PhotonNetwork.LeaveRoom();
        PhotonNetwork.LeaveLobby();
        StartCoroutine(reJoinLobby());
    }


}
