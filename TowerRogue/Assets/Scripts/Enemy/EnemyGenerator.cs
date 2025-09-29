using Photon.Pun;
using UnityEngine;

public class EnemyGenerator : MonoBehaviourPunCallbacks
{
    [SerializeField] EnemyData enemyData;

    float generateInterbal = 0;             // ¶¬Œã‚ÌŒo‰ßŽžŠÔ
    [SerializeField] float generateTime;    // ¶¬‚·‚éŽžŠÔ

    void Awake()
    {
        PhotonCustomTypes.Register();
    }

    // Start is called once before the first execution of Update after the MonoBehaviour is created
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        if (PhotonNetwork.IsMasterClient)
        {
            generateInterbal += Time.deltaTime;

            if (generateInterbal >= generateTime)
            {
                generateInterbal = 0;
                var rnd_idx = Random.Range(0, enemyData.enemyStats.Count);

                GameObject obj = PhotonNetwork.Instantiate("Enemy", transform.position, Quaternion.identity);
                PhotonView photonView = obj.GetComponent<PhotonView>();
                photonView.RPC("SetStats", RpcTarget.All, enemyData.enemyStats[rnd_idx]);
            }
        }        
    }
}
