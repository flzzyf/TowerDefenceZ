using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BuildManager : MonoBehaviour {

#region Singleton
    public static BuildManager instance;

    private void Awake()
    {
        if (instance == null)
            instance = this;
    }
#endregion

    public GameObject turretToBuild;

	void Start () {
		
	}

    public GameObject GetTurretToBuild()
    {
        return turretToBuild;
    }

}
