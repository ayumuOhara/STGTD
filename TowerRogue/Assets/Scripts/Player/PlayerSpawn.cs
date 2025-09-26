using UnityEngine;
using Photon.Pun;
using Photon.Realtime;

public class PlayerSpawn : MonoBehaviourPunCallbacks
{
    // Start is called once before the first execution of Update after the MonoBehaviour is created
    void Start()
    {
        if (PhotonNetwork.IsMasterClient)
        {
            PhotonNetwork.Instantiate("Player", new Vector3(0, 0, 0), Quaternion.identity);
        }
        else
        {
            PhotonNetwork.Instantiate("Player", new Vector3(0, 0, 0), Quaternion.identity);
        }
    }

    // Update is called once per frame
    void Update()
    {
        
    }
}
