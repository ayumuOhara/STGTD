using UnityEngine;
using Photon.Pun;
using Photon.Realtime;
using UnityEngine.InputSystem;

public class PlayerController : MonoBehaviourPunCallbacks
{
    [SerializeField] GameObject bulletPrefab;
    [SerializeField] GameObject bulletSpawn;
    Vector3 towerPos;

    Gamepad gamepad;

    int maxHp = 100;
    int hp = 0;
    float moveSpeed = 2.0f;    // 移動速度
    
    Vector3 direction = Vector3.zero;   // プレイヤーの向き
    
    float shotInterbal = 0; // 射撃後の経過時間
    float fireRate = 0.5f;  // 射撃のクールタイム

    float spawnInterbal = 0;    // 死亡後の経過時間
    float spawnTime = 3.0f;     // 復活する時間

    public bool isDead = false;

    // Start is called once before the first execution of Update after the MonoBehaviour is created
    void Start()
    {
        towerPos = GameObject.Find("Tower").transform.position;
        gamepad = Gamepad.current;
        hp = maxHp;
    }

    // Update is called once per frame
    void Update()
    {
        if (photonView.IsMine)
        {
            if (Input.GetMouseButtonDown(1))
            {
                TakeDamage(100);
            }

            if (isDead)
            {
                spawnInterbal += Time.deltaTime;
                if(spawnInterbal >= spawnTime)
                {
                    Respawn();
                    spawnInterbal = 0;
                }
            }
            else
            {
                Moving();
                Rotate();
                Attack();
            }
        }
    }

    // 移動処理
    void Moving()
    {
        float x = 0, y = 0;

        if(gamepad == null)
        {
            // WASDで移動する向きを取得
            if (Input.GetKey(KeyCode.W)) y = 1.0f;
            if (Input.GetKey(KeyCode.S)) y = -1.0f;
            if (Input.GetKey(KeyCode.D)) x = 1.0f;
            if (Input.GetKey(KeyCode.A)) x = -1.0f;
        }
        else
        {
            if (gamepad.leftStick.ReadValue().magnitude >= 0.01f)
            {
                // 左スティックの入力ベクトル
                Vector2 input = gamepad.leftStick.ReadValue();
                x = input.x;
                y = input.y;
            }
        }
        
        // 現在地から取得した向きにmoveSpeed分移動する
        Vector3 moveVelocity = new Vector3(x, y, 0) * moveSpeed * Time.deltaTime;
        transform.position += moveVelocity;   // 現在地を移動した座標に更新
    }

    // 回転処理
    void Rotate()
    {
        if(gamepad == null)
        {
            // マウスの座標をワールド座標に変換
            Vector3 mousePos = Camera.main.ScreenToWorldPoint(Input.mousePosition);
            mousePos.z = 0;

            // マウスの方向ベクトル
            direction = mousePos - transform.position;
        }
        else
        {
            if (gamepad.rightStick.ReadValue().magnitude >= 0.7f)
            {
                // 右スティックの方向ベクトル
                direction = gamepad.rightStick.ReadValue();
            }
        }

        float angle = Mathf.Atan2(direction.y, direction.x) * Mathf.Rad2Deg;
        // 回転を適用する
        transform.rotation = Quaternion.Euler(new Vector3(0, 0, angle));
    }

    // 攻撃処理
    void Attack()
    {
        shotInterbal += Time.deltaTime;
        
        if (gamepad == null)
        {
            if (Input.GetMouseButton(0))
            {
                if (shotInterbal > fireRate)
                {
                    PhotonNetwork.Instantiate("Bullet", bulletSpawn.transform.position, transform.rotation);
                    shotInterbal = 0;
                }
            }
        }
        else
        {
            if (gamepad.rightShoulder.isPressed)
            {
                if (shotInterbal > fireRate)
                {
                    PhotonNetwork.Instantiate("Bullet", bulletSpawn.transform.position, transform.rotation);
                    shotInterbal = 0;
                }
            }
        }
    }

    // ダメージ処理
    public void TakeDamage(int damage)
    {
        hp -= damage;

        if (hp <= 0)
        {
            Dead();
        }
    }

    // 死亡処理
    void Dead()
    {
        isDead = true;
        transform.localScale = new Vector3(0, 0, 0);
        transform.position = towerPos;
    }

    // 復活処理
    void Respawn()
    {
        isDead = false;
        transform.localScale = new Vector3(0.3f, 0.3f, 0.3f);
        hp = maxHp;
        shotInterbal = 0;
    }
}
