using UnityEngine;

public class ToTowerBehaviour : MoveBehaviour
{
    Vector3 towerPos;

    public void Move(GameObject myObj, float moveSpd)
    {
        if(towerPos == null) towerPos = GameObject.Find("Tower").transform.position;

        myObj.transform.position += (towerPos - myObj.transform.position).normalized * moveSpd * Time.deltaTime;
    }
}
