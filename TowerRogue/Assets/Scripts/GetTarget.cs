using UnityEngine;
using static UnityEngine.RuleTile.TilingRuleOutput;

public static class GetTarget
{
    public static Vector3 NearestTargetPos(Vector3 myPos, string targetTag)
    {
        GameObject[] targets = GameObject.FindGameObjectsWithTag(targetTag);

        if (targets.Length == 1)
        {
            return targets[0].transform.position;
        }
        else
        {
            GameObject nearestObject = null;
            float nearestDistance = Mathf.Infinity;

            foreach (GameObject target in targets)
            {
                Vector3 direction = target.transform.position - myPos;
                float distance = direction.magnitude;

                // Raycastを発射して障害物がないか確認
                Ray ray = new Ray(myPos, direction.normalized);
                if (Physics.Raycast(ray, out RaycastHit hit))
                {
                    if (hit.collider.gameObject == target)
                    {
                        // 最短距離を更新
                        if (distance < nearestDistance)
                        {
                            nearestDistance = distance;
                            nearestObject = target;
                        }
                    }
                }
            }

            return nearestObject.transform.position;
        }
    }
}
