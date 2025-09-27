using UnityEngine;

public class EnemyController : MonoBehaviour
{
    [SerializeField] EnemyStats stats;
    AttackBehaviour attackBehaviour;
    MoveBehaviour moveBehaviour;

    float atkInterbal = 0;

    // エネミーのスタッツを設定
    public void SetStats(EnemyStats stats)
    {
        this.stats = stats;
        attackBehaviour = AttackBehaviourFactory.Get(stats.attackType);
        moveBehaviour = MoveBehaviourFactory.Get(stats.targetType);
    }

    // Start is called once before the first execution of Update after the MonoBehaviour is created
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        if (AttackTimer())
        {
            attackBehaviour.Attack(stats.atk, stats.targetType);
        }

        moveBehaviour.Move(gameObject, stats.moveSpeed);
    }

    // 攻撃クールタイム
    public bool AttackTimer()
    {
        atkInterbal += Time.deltaTime;

        if (atkInterbal > stats.atkSpeed)
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
