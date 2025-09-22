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
        PhotonNetwork.JoinLobby(); // �κ��
    }

    public override void OnJoinedLobby()
    {
        // ���� ��ġ: ������ �����, ������ ��
        var opts = new RoomOptions { MaxPlayers = 2, IsVisible = true, IsOpen = true };
        PhotonNetwork.JoinOrCreateRoom("QuickMatch", opts, TypedLobby.Default);
    }

    public override void OnJoinedRoom()
    {
        if (PhotonNetwork.CurrentRoom.PlayerCount == 2 && PhotonNetwork.IsMasterClient)
        {
            PhotonNetwork.LoadLevel("Game"); // �����Ͱ� �� ����ȭ ����
        }
    }
}
