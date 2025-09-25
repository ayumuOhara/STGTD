using Photon.Pun;
using Photon.Realtime;
using TMPro;
using UnityEditor.Profiling.Memory.Experimental;
using UnityEngine;

public class RoomMatch : MonoBehaviourPunCallbacks
{
    const int MAX_PLAYER_NUM = 2;   // 参加可能人数

    [SerializeField] TMP_InputField nameField;

    void Awake()
    {
        // シーン遷移を親に同期
        PhotonNetwork.AutomaticallySyncScene = true;
    }

    // Start is called once before the first execution of Update after the MonoBehaviour is created
    void Start()
    {
        PhotonNetwork.ConnectUsingSettings();
    }

    // Update is called once per frame
    void Update()
    {
        
    }

    // ルーム作成
    public void CreateRoom()
    {
        var name = nameField.text;

        // ルーム参加人数を設定
        RoomOptions roomOptions = new RoomOptions();
        roomOptions.MaxPlayers = MAX_PLAYER_NUM;

        PhotonNetwork.CreateRoom(name, roomOptions);
    }

    // ルーム参加
    public void JoinRoom()
    {
        var name = nameField.text;

        // ルーム参加
        PhotonNetwork.JoinRoom(name);
    }

    public override void OnCreateRoomFailed(short returnCode, string message)
    {
        if (returnCode == ErrorCode.GameIdAlreadyExists)    // 同名のルームを作成しようとした際のログ
        {
            Debug.Log("その名前のルームは既に存在しています");
        }
        else
        {
            Debug.Log($"ルーム作成に失敗: {message} (Code: {returnCode})");
        }
    }

    public override void OnJoinRoomFailed(short returnCode, string message)
    {
        Debug.Log("ルーム参加に失敗");
        Debug.Log("ルームが存在しないか、人数に空きが無い可能性があります");
    }

    // ルームに参加した直後に呼ばれる
    public override void OnJoinedRoom()
    {
        Debug.Log("ルームに参加しました");
        Debug.Log($"現在のルームの人数: {PhotonNetwork.CurrentRoom.PlayerCount} / {MAX_PLAYER_NUM}");
    }

    // 新しくルームにプレイヤーが参加した時に呼ばれる
    public override void OnPlayerEnteredRoom(Player newPlayer)
    {
        Debug.Log("他プレイヤーが新しくルームに参加しました");
        Debug.Log($"現在のルームの人数: {PhotonNetwork.CurrentRoom.PlayerCount} / {MAX_PLAYER_NUM}");

        if(PhotonNetwork.IsMasterClient)
        {
            if (PhotonNetwork.CurrentRoom.PlayerCount == MAX_PLAYER_NUM)
            {
                Debug.Log("参加可能人数に達しました");

                // ルームを非公開にする
                PhotonNetwork.CurrentRoom.IsOpen = false;
                // シーン遷移
                PhotonNetwork.LoadLevel("Main");
            }
        }        
    }
}
