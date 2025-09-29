using UnityEngine;
using Photon.Pun;
using System;

public class Character
{
    int maxHp;         // 最大HP
    int currentHp;     // 現在HP
    int atkPow;        // 攻撃力
    float atkRate;       // 攻撃間隔
    float moveSpeed;   // 移動速度
    float spawnTime;   // 復活までの時間

    bool isDead;       // 死亡したか

    public Character(int maxHp = 0, int atkPow = 0, float atkRate = 0, float moveSpeed = 0, float spawnTime = 0)
    {
        this.maxHp = maxHp;
        currentHp = maxHp;
        this.atkPow = atkPow;
        this.atkRate = atkRate;
        this.moveSpeed = moveSpeed;
        this.spawnTime = spawnTime;
    }

    // ダメージ
    [PunRPC]
    public void TakeDamage(int damage)
    {
        currentHp -= damage;

        if(currentHp <= 0)
        {
            SetIsDead(true);
        }
    }

    // HP回復
    [PunRPC]
    public void Heal(int heal)
    {
        currentHp = Mathf.Clamp(currentHp + heal, 0, maxHp);
    }

    // 死亡フラグ設定
    [PunRPC]
    public void SetIsDead(bool isDead)
    {
        this.isDead = isDead;
    }

    // 死亡確認
    public bool GetIsDead()
    {
        return isDead;
    }

    // 移動速度上昇
    [PunRPC]
    public void SpeedBoost()
    {

    }

    // 移動速度低下
    [PunRPC]
    public void SpeedDown()
    {

    }

    // 復活時間増加
    [PunRPC]
    public void RespawnTimeInc()
    {

    }

    // 復活時間減少
    [PunRPC]
    public void RespawnTimeDec()
    {

    }
}
