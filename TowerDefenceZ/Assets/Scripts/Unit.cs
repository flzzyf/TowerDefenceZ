using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Unit : MonoBehaviour {

    public float maxHP = 10;
    public float speed = 3;
    public float rotSpeed = 30;

    float currentHP;
    [HideInInspector]
    public bool isDead = false;

    void Start ()
    {
        currentHP = maxHP;
	}
	
	void Update () {
		
	}

    public void TakeDamage(float _amount)
    {
        if (isDead)
            return;

        currentHP -= _amount;

        if (currentHP > 0)
        {
            //还没死
        }
        else
        {
            //死了
            //Debug.Log("Dead");
            isDead = true;
            Destroy(gameObject);
        }
    }
}
