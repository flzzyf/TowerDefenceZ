using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Turret : MonoBehaviour {

    [Header("Attribute")]
    [Header("范围为Collider的Radius")]
    public float rotSpeed = 10;

    [Header("Setup")]
    public Transform partToRotate;

    public const string enemyTag = "Enemy";

    GameObject target;

    #region 用Collider获取可攻击目标列表
    List<GameObject> targetList = new List<GameObject>();

    private void OnTriggerEnter(Collider other)
    {
        targetList.Add(other.gameObject);

    }

    private void OnTriggerExit(Collider other)
    {
        targetList.Remove(other.gameObject);
    }
    #endregion

    void Start () {
		
	}
	
	void Update ()
    {
        if(targetList.Count > 0)
        {
            UpdateTarget();
        }

		if(target != null)
        {
            FaceTarget(partToRotate, target.transform.position);
        }
	}

    void UpdateTarget()
    {
        //从目标组获取最近目标
        float shortestDistance = Mathf.Infinity;
        GameObject nerestEnemy = null;

        foreach (GameObject enemy in targetList)
        {
            float distance = Vector3.Distance(transform.position, enemy.transform.position);

            if (distance < shortestDistance)
            {
                shortestDistance = distance;
                nerestEnemy = enemy;
            }
        }

        target = nerestEnemy;

    }

    //面向目标方向
    void FaceTarget(Transform _partToRotate, Vector3 target)
    {
        Vector3 dir = target - _partToRotate.position;
        Quaternion lookRotation = Quaternion.LookRotation(dir);
        Vector3 rotation = Quaternion.Lerp(_partToRotate.rotation, lookRotation, Time.deltaTime * rotSpeed).eulerAngles;
        _partToRotate.rotation = Quaternion.Euler(0f, rotation.y, 0f);
    }
}
