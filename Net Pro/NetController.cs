using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Photon.Pun;
using Photon.Realtime;
using UnityEngine.UI;
using UnityEditor;

public class NetController : MonoBehaviourPunCallbacks
{
    public string gameVersion = "1";

    [SerializeField]
    private Button onlineBtn;
    [SerializeField]
    private Text netStatus;

    [SerializeField]
    private InputField usernameInput;
    [SerializeField]
    private Text errorText;

    private void Start()
    {
        errorText.enabled = false;
    }

    public void Connect()
    {
        if (!SetName())
            return;

        netStatus.color = Color.white;
        netStatus.text = "Connecting...";       

        PhotonNetwork.AutomaticallySyncScene = true;
        PhotonNetwork.ConnectUsingSettings();
        PhotonNetwork.GameVersion = gameVersion;       
    }  

    public override void OnConnectedToMaster()
    {        
        Debug.Log("Connected to Master!");       
        PhotonNetwork.JoinLobby();            
    }   

    public override void OnJoinedLobby()
    {
        netStatus.color = new Color(0f, 1f, 0.09604788f);
        netStatus.text = "Connected";
        MenusManager.hasError = false;

        Debug.Log("Connected to Lobby!");
    }

    public void RefreshRoomList()
    {       
        PhotonNetwork.Disconnect();        
        PhotonNetwork.ConnectUsingSettings();
        PhotonNetwork.GameVersion = gameVersion;            
    }     

    public void Disconnect()
    {
        PhotonNetwork.Disconnect();        
    }

    private bool SetName()
    {      
        if (usernameInput.text.Length < 5)
        {
            MenusManager.hasError = true;

            errorText.enabled = true;
            errorText.rectTransform.anchoredPosition = new Vector2(0f, 178f);
            errorText.text = "Invalid username";
            return false;
        }
        
        MenusManager.hasError = false;
        errorText.enabled = false;
        PhotonNetwork.NickName = usernameInput.text;
        return true;


    }
}
