using Photon.Pun;
using Photon.Realtime;
using UnityEngine;

public class NetBooting : MonoBehaviourPunCallbacks
{
    void Start()
    {
        PhotonNetwork.AutomaticallySyncScene = true;
        PhotonNetwork.NickName = "Player" + Random.Range(1000, 9999);
        PhotonNetwork.ConnectUsingSettings();
    }

    public override void OnConnectedToMaster()
    {
        PhotonNetwork.JoinLobby(); // 로비로
    }

    public override void OnJoinedLobby()
    {
        // 빠른 매치: 없으면 만들고, 있으면 들어감
        var opts = new RoomOptions { MaxPlayers = 2, IsVisible = true, IsOpen = true };
        PhotonNetwork.JoinOrCreateRoom("QuickMatch", opts, TypedLobby.Default);
    }

    public override void OnJoinedRoom()
    {
        if (PhotonNetwork.CurrentRoom.PlayerCount == 2 && PhotonNetwork.IsMasterClient)
        {
            PhotonNetwork.LoadLevel("Game"); // 마스터가 씬 동기화 시작
        }
    }
}
