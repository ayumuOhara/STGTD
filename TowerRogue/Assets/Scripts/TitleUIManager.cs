using Photon.Pun;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.InputSystem;

public class TitleUIManager : MonoBehaviourPunCallbacks
{
    [SerializeField] RoomMatching match;

    [SerializeField] List<GameObject> titleUIList;
    GameObject currentWindow;
    Gamepad gamepad;

    private void Start()
    {
        gamepad = Gamepad.current;
        WindowTransition(titleUIList[0]);
    }

    private void Update()
    {
        if (match.isConnecting) return;

        if(gamepad == null)
        {
            if (Input.GetKeyDown(KeyCode.Escape))
            {
                TitleUI();
            }
        }
        else
        {
            if(gamepad.aButton.isPressed)
            {
                TitleUI();
            }
        }
    }

    // タイトル&モード選択
    void TitleUI()
    {
        if (currentWindow == titleUIList[0]) return;

        if (PhotonNetwork.InRoom)
        {
            PhotonNetwork.LeaveRoom();
        }

        if (PhotonNetwork.IsConnected)
        {
            PhotonNetwork.Disconnect();
        }

        WindowTransition(titleUIList[0]);
    }

    // ソロモードUI表示
    public void SoloUI()
    {
        WindowTransition(titleUIList[1]);
    }

    // マルチモードUI表示
    public void MultiUI()
    {
        WindowTransition(titleUIList[2]);
    }

    // ウィンドウ切り替え
    public void WindowTransition(GameObject nextWindow)
    {
        if (currentWindow == nextWindow) return;

        currentWindow?.SetActive(false);
        currentWindow = nextWindow;
        currentWindow?.SetActive(true);
    }
}
