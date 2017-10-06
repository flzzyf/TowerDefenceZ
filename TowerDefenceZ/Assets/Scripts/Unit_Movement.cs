using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Unit_Movement : MonoBehaviour {

    int currentWayPointIndex = 0;
    Vector3 targetWayPoint;
    Unit unit;

	void Start ()
    {
        unit = GetComponent<Unit>();
        ReachTarget();
	}
	
	void Update ()
    {
        //朝下个点移动
        Vector3 dir = GetHorizontalDirection(transform.position, targetWayPoint);

        transform.Translate(dir.normalized * unit.speed * Time.deltaTime, Space.World);

        FaceTarget(targetWayPoint);

        if(dir.magnitude <= unit.speed * Time.deltaTime)
        {
            ReachTarget();
        }

    }

    //获取下个路径点
    void GetNextWayPoint()
    {
        targetWayPoint = WayPointManager.wayPoints[currentWayPointIndex].position;
    }

    //到达目标点
    void ReachTarget()
    {
        currentWayPointIndex++;

        if (currentWayPointIndex == WayPointManager.wayPoints.Length)
        {
            //抵达终点
            Destroy(gameObject);
        }
        else
        {
            //继续走
            GetNextWayPoint();
        }
    }

    //面向目标方向
    void FaceTarget(Vector3 target)
    {
        Vector3 dir = target - transform.position;
        Quaternion lookRotation = Quaternion.LookRotation(dir);
        Vector3 rotation = Quaternion.Lerp(transform.rotation, lookRotation, Time.deltaTime * unit.rotSpeed).eulerAngles;
        transform.rotation = Quaternion.Euler(0f, rotation.y, 0f);
    }

    //返回水平方向向量
    Vector3 GetHorizontalDirection(Vector3 origin, Vector3 target)
    {
        Vector3 dir;
        dir = target - origin;
        dir.y = 0;

        return dir;
    }
}
