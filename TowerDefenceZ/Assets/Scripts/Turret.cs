using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Turret : MonoBehaviour
{
    public float rotSpeed = 10;

    [Header("Setup")]
    public Transform partToRotate;

    public const string enemyTag = "Enemy";

    GameObject target;

    public Animator animator;

    public float range = 40;

    Turret_Weapon weapon;

    void Start ()
    {
        weapon = GetComponent<Turret_Weapon>();

        //解除隐藏
        animator.SetBool("hidden", false);
	}
	
	void Update ()
    {
        //没有目标或者在范围外，搜索目标
        if(target == null || Vector3.Distance(transform.position, target.transform.position) > range)
        {
            UpdateTarget();
        }
        else
        {
            //没有朝向目标则转动，否则攻击
            FaceTarget(partToRotate, target.transform.position);

            weapon.Attack(target);
        }
	}

    void UpdateTarget()
    {
        //从目标组获取最近目标
        float shortestDistance = Mathf.Infinity;
        GameObject nerestEnemy = null;

        float distance;
        foreach (var item in Physics.OverlapSphere(transform.position, range))
        {
            //过滤非敌人单位
            if (item.gameObject.tag != "Unit")
                continue;

            distance = Vector3.Distance(transform.position, item.transform.position);

            if (distance < shortestDistance)
            {
                shortestDistance = distance;
                nerestEnemy = item.gameObject;
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

    private void OnDrawGizmos()
    {
        Gizmos.DrawWireSphere(transform.position, range);
    }
}
