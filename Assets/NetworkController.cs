using UnityEngine;
using UnityEngine.UI;
using TMPro;
using System.Collections;
using System.Collections.Generic;
using Photon.Realtime;
using Photon.Pun;

public class NetworkController : MonoBehaviourPunCallbacks
{
    public bool connect = false;
    string playernameTemp;
    [SerializeField] TMP_InputField playerNameInput;
    [SerializeField] GameObject telaLogin;
    [SerializeField] GameObject telaSala;
    public TextMeshProUGUI textoBotao;

    private void Awake()
    {
        telaLogin.gameObject.SetActive(true); 
        telaSala.gameObject.SetActive(false);
    }

    public void ConectServerUI()
    {
        PhotonNetwork.ConnectUsingSettings();
    }
    void Start()
    {
        playernameTemp = "Player" + Random.Range(1, 1000);
        playerNameInput.text = playernameTemp;
    }


    private void Login()
    {
        if(playerNameInput.text == "")
        {
            PhotonNetwork.NickName = playerNameInput.text;
        }
        else
        {
            PhotonNetwork.NickName = playernameTemp;
        }

        telaLogin.gameObject.SetActive(false);
        telaSala.gameObject.SetActive(true);
    }
    
    void Update()
    {
        if(connect == false)
        {
            connect = true;
            PhotonNetwork.ConnectUsingSettings();
        }
    }

    public override void OnConnected()
    {
        print("OnConnected");
    }

    public override void OnConnectedToMaster()
    {
        base.OnConnectedToMaster();
        print("OnConnectMaster");
        print("Server: " + PhotonNetwork.CloudRegion + "Ping: " + PhotonNetwork.GetPing());
        Login();
        PhotonNetwork.JoinLobby();
    }

    public override void OnJoinedLobby()
    {
        base.OnJoinedLobby();
        PhotonNetwork.JoinRandomRoom();
    }

    public override void OnJoinRandomFailed(short returnCode, string message)
    {
        base.OnJoinRandomFailed(returnCode, message);
        string roomTemp = "Room" + Random.Range(1, 1000);
        PhotonNetwork.CreateRoom(roomTemp);
    }

    public override void OnDisconnected(DisconnectCause cause)
    {
        base.OnDisconnected(cause);
        print("OnDisconectd: " + cause);
        PhotonNetwork.ConnectToRegion("EU");
        connect = false;
        textoBotao.text = "Reconectar";
    }


}
