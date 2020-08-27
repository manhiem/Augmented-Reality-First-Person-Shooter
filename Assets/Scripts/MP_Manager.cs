using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Photon.Pun;
using UnityEngine.UI;
using Photon.Realtime;

public class MP_Manager : MonoBehaviourPunCallbacks
{
    public InputField enterroomID; 
    public Text errorDisplay;
    


    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void FixedUpdate()
    {
        
    }  

    public void connectToMaster()
    {
        PhotonNetwork.ConnectUsingSettings();
    }

    public override void OnConnectedToMaster()
    {
        PhotonNetwork.JoinLobby();
    }
    public void CreateRoom()
    {
        if(string.IsNullOrEmpty(enterroomID.text))
        {
            return;
        }
        
        PhotonNetwork.CreateRoom(enterroomID.text);
    }

    public void JoinRoom()
    {
        PhotonNetwork.JoinRoom(enterroomID.text);
    }

    public override void OnJoinedRoom()
    {
        PhotonNetwork.LoadLevel(1);        
    }
    public override void OnCreateRoomFailed(short returnCode, string message)
    {
        errorDisplay.text = "Room Creation Failed! [ " + message + " ] ";
    } 

    public override void OnJoinRoomFailed(short shortCode, string message)
    {
        errorDisplay.text = "Room Creation Failed! [ " + message +" ] ";
    }

    public void OnLeaveRoom()
    {
        PhotonNetwork.LeaveRoom();
    }

    public override void OnLeftRoom()
    {
    }
}