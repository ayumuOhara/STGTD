using UnityEngine;
using System.Collections.Generic;

public static class AttackBehaviourFactory
{
    private static Dictionary<EnemyStats.AttackType, AttackBehaviour> atkBehaviours = new()
    {
        { EnemyStats.AttackType.Contact, new ContactBehaviour() },
        { EnemyStats.AttackType.Shot,    new ShotBehaviour()    },
    };

    public static AttackBehaviour Get(EnemyStats.AttackType attackType) => atkBehaviours[attackType];
}
