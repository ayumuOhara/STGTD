using System;
using UnityEngine;
using System.Collections.Generic;

[Serializable]
public class EnemyStats
{
    public enum TargetType
    {
        Player,
        Tower,
    }

    public enum AttackType
    {
        Contact,
        Shot,
    }

    public int enemyID;             // エネミー番号
    public string enemyName;        // エネミー名
    public int maxHp;               // 最大HP
    public int atkPow;              // 攻撃力
    public int atkRate;             // 攻撃間隔
    public float shotRange;         // 射程距離
    public int moveSpeed;           // 移動速度

    public TargetType targetType;   // ターゲット傾向
    public AttackType attackType;   // 攻撃方法
}

[CreateAssetMenu(fileName = "EnemyData", menuName = "Scriptable Objects/EnemyData")]
public class EnemyData : ScriptableObject
{
    public List<EnemyStats> enemyStats;
}
