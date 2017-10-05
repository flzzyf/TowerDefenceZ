using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Turret : MonoBehaviour {

    [Header("Attribute")]
    public float range = 15f;

    [Header("Attribute")]
    public Transform partToRotate;

    public const string enemyTag = "Enemy";

    GameObject target;


	void Start () {
		
	}
	
	void Update ()
    {
		if(target != null)
        {

        }
	}

    void UpdateTarget()
    {
        //根据Tag获取目标组
        GameObject[] enemies = GameObject.FindGameObjectsWithTag(enemyTag);

        //从目标组获取最近目标
        float shortestDistance = Mathf.Infinity;
        GameObject nerestEnemy = null;

        foreach (GameObject enemy in enemies)
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

    #region 用Collider获取可攻击目标列表
    List<GameObject> targetList;

    private void OnTriggerEnter(Collider other)
    {
        targetList.Add(other.gameObject);
    }

    private void OnTriggerExit(Collider other)
    {
        targetList.Remove(other.gameObject);
    }
#endregion
}
