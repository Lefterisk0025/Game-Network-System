using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using Photon.Pun;
using Photon.Realtime;

public class PlayerListing : MonoBehaviourPunCallbacks
{
    [SerializeField]
    private Text nameText;
    [SerializeField]
    private Button kickBtn;

    public Player Player { get; private set; }  

    public void SetPlayer(Player player)
    {
        Player = player;
        nameText.text = player.NickName;

        if (PhotonNetwork.IsMasterClient)
            kickBtn.gameObject.SetActive(true);
        else
            kickBtn.gameObject.SetActive(false);
    }
}
