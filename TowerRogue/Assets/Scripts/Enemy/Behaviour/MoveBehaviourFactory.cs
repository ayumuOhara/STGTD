using System.Collections.Generic;
using UnityEngine;

public static class MoveBehaviourFactory
{
    private static Dictionary<EnemyStats.TargetType, MoveBehaviour> moveBehaviours = new()
    {
        { EnemyStats.TargetType.Player, new ToPlayerBehaviour() },
        { EnemyStats.TargetType.Tower, new ToTowerBehaviour() },
    };

    public static MoveBehaviour Get(EnemyStats.TargetType targetType) => moveBehaviours[targetType];
}
