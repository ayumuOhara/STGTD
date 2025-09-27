using UnityEngine;

public interface AttackBehaviour
{
    public void Attack(int damage, EnemyStats.TargetType target);
}
