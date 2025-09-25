using UnityEngine;
using Photon.Pun;
using Photon.Realtime;

public class PlayerController : MonoBehaviourPunCallbacks
{
    float moveSpeed = 5.0f;

    // Start is called once before the first execution of Update after the MonoBehaviour is created
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        if (photonView.IsMine)
        {
            // WASDで移動する向きを取得
            float x = 0, y = 0;
            if (Input.GetKey(KeyCode.W)) y = 1.0f;
            if (Input.GetKey(KeyCode.S)) y = -1.0f;
            if (Input.GetKey(KeyCode.D)) x = 1.0f;
            if (Input.GetKey(KeyCode.A)) x = -1.0f;

            // 現在地から取得した向きにmoveSpeed分移動する
            Vector3 moveVelocity = new Vector3(x, y, 0) * moveSpeed * Time.deltaTime;
            transform.position += moveVelocity;   // 現在地を移動した座標に更新
        }
    }
}
