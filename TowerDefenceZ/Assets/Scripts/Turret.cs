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

    Turret_Weapon weapon;

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

    void Start ()
    {
        weapon = GetComponent<Turret_Weapon>();
	}
	
	void Update ()
    {
        //没有目标或者目标在范围外
        if(target == null || !targetList.Contains(target))
        {
            if(targetList.Count > 0)
            {
                //重新搜索目标
                UpdateTarget();
            }

        }
        else
        {
            //有可攻击目标
            FaceTarget(partToRotate, target.transform.position);

            weapon.Fire(target.transform.position);
            target.GetComponent<Unit>().TakeDamage(5f * Time.deltaTime);
        }
	}

    void UpdateTarget()
    {
        //从目标组获取最近目标
        float shortestDistance = Mathf.Infinity;
        GameObject nerestEnemy = null;

        //去除无效目标
        for (int i = 0; i < targetList.Count; i++)
        {
            if(targetList[i] == null)
            {
                targetList.Remove(targetList[i]);

            }
        }

        //选出目标组中最近的一个
        foreach (GameObject target in targetList)
        {

            float distance = Vector3.Distance(transform.position, target.transform.position);

            if (distance < shortestDistance)
            {
                shortestDistance = distance;
                nerestEnemy = target;
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
