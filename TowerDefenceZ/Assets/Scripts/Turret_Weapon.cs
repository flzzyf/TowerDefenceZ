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

    public float range = 30;

    Turret turret;
    AudioSource audioSource;

    public TrailRenderer prefab_trail;

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
    }

    public void Attack(GameObject _target)
    {
        //冷却为0可开火
        if (currentCountdown <= 0)
        {
            if (Vector3.Distance(transform.position, _target.transform.position) <= range && TargetIsForward(_target.transform.position))
            {
                currentCountdown = 1 / fireRate;
                Fire(_target.transform.position);
            }
        }
    }

    public void Fire(Vector3 _targetPoint)
    {
        GameObject particle = Instantiate(launchEffect, firePoint.position, firePoint.rotation);
        Destroy(particle, particle.GetComponent<ParticleSystem>().main.duration);

        audioSource.Play();

        RaycastHit hit;

        Vector3 random = new Vector3(Random.Range(-1, 1), Random.Range(-1, 1), Random.Range(-1, 1));
        if(Physics.Raycast(firePoint.position, _targetPoint - firePoint.position + random, out hit, 100))
        {
            Debug.DrawLine(firePoint.position, hit.point);

            StartCoroutine(LaunchTrail(hit.point));

            particle = Instantiate(impactEffect, hit.point, Quaternion.LookRotation(hit.normal));
            Destroy(particle, particle.GetComponent<ParticleSystem>().main.duration);

            if(hit.transform.tag == "Unit")
            {
                hit.transform.gameObject.GetComponent<Unit>().TakeDamage(damage);
            }

        }
    }

    IEnumerator LaunchTrail(Vector3 _target)
    {
        float random = Random.Range(1, 2f);
        TrailRenderer trail = Instantiate(prefab_trail, new Ray(firePoint.position, _target - firePoint.position).GetPoint(random), Quaternion.identity);

        yield return null;
        trail.transform.Translate(_target - firePoint.position);

        Destroy(trail, trail.time);
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
