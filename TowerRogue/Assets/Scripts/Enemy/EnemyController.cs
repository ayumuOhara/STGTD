using Photon.Pun;
using UnityEngine;

public class EnemyController : MonoBehaviourPunCallbacks
{
    public Character enemy;

    EnemyStats stats;
    AttackBehaviour attackBehaviour;
    MoveBehaviour moveBehaviour;

    float atkInterbal = 0;

    // エネミーのスタッツを設定
    [PunRPC]
    public void SetStats(EnemyStats stats)
    {
        this.stats = stats;
        attackBehaviour = AttackBehaviourFactory.Get(stats.attackType);
        moveBehaviour = MoveBehaviourFactory.Get(stats.targetType);

        enemy = new Character(stats.maxHp, stats.atkPow, stats.atkRate, stats.moveSpeed);
    }

    // Start is called once before the first execution of Update after the MonoBehaviour is created
    void Start()
    {

    }

    // Update is called once per frame
    void Update()
    {
        if(enemy.GetIsDead())
        {
            Destroy(gameObject);
        }

        if (AttackTimer())
        {
            attackBehaviour.Attack(stats.atkPow, stats.targetType);
        }

        moveBehaviour.Move(gameObject, stats.moveSpeed);
    }

    // 攻撃クールタイム
    public bool AttackTimer()
    {
        atkInterbal += Time.deltaTime;

        if (atkInterbal > stats.atkRate)
        {
            atkInterbal = 0;
            return true;
        }
        else
        {
            return false;
        }
    }
}
