using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Turret_Weapon : MonoBehaviour {

    public float damage = 5f;
    public float fireRate = 4f;
    float currentCountdown = 0;

    public Transform firePoint;

    public GameObject launchEffect;
    public GameObject impactEffect;

    Turret turret;
    GameObject target;

    AudioSource audioSource;

    void Start ()
    {
        turret = GetComponent<Turret>();

        audioSource = GetComponent<AudioSource>();
	}
	
	void Update ()
    {
        if(currentCountdown > 0)
        {
            currentCountdown -= Time.deltaTime;
        }

        target = turret.ReturnTarget();

        if (target != null)
        {
            if (currentCountdown <= 0)
            {
                //冷却为0可开火

                if (TargetIsForward(target.transform.position))
                {
                    currentCountdown = 1 / fireRate;
                    Fire(target.transform.position);
                }
            }
        }


    }

    public void Fire(Vector3 _targetPoint)
    {
        GameObject particle = Instantiate(launchEffect, firePoint.position, firePoint.rotation);
        Destroy(particle, particle.GetComponent<ParticleSystem>().main.duration);

        audioSource.Play();

        RaycastHit hit;

        if(Physics.Raycast(firePoint.position, _targetPoint - firePoint.position, out hit, 500))
        {
            Debug.DrawLine(firePoint.position, hit.point);

            //Debug.Log(gameObject.name);
            //Debug.Log(hit.transform.gameObject.name);

            particle = Instantiate(impactEffect, hit.point, Quaternion.LookRotation(hit.normal));
            Destroy(particle, particle.GetComponent<ParticleSystem>().main.duration);

            if(hit.transform.tag == "Unit")
            {
                hit.transform.gameObject.GetComponent<Unit>().TakeDamage(damage);

            }

        }

    }

    bool TargetIsForward(Vector3 _target)
    {
        Vector3 dir = _target - turret.partToRotate.position;

        float angle = Vector3.Angle(dir, turret.partToRotate.forward);

        if(angle < 30)
        {
            return true;
        }
        return false;
    }
}
