using UnityEngine;
using static UnityEngine.RuleTile.TilingRuleOutput;
using UnityEngine.InputSystem;

public class ToPlayerBehaviour : MoveBehaviour
{
    Vector3 playerPos;

    public void Move(GameObject myObj, float moveSpd)
    {
        playerPos = GetTarget.NearestTargetPos(myObj.transform.position, "Player");

        myObj.transform.position += (playerPos - myObj.transform.position).normalized * moveSpd * Time.deltaTime;
    }
}
