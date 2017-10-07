using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Turret_Weapon : MonoBehaviour {

    public Transform firePoint;

    public GameObject launchEffect;

	void Start () {
		
	}
	
	void Update () {
		
	}

    public void Fire(Vector3 _targetPoint)
    {
        GameObject particle = Instantiate(launchEffect, firePoint.position, firePoint.rotation);
        Destroy(particle, particle.GetComponent<ParticleSystem>().main.duration);
    }
}
