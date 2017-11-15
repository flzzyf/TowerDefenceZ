using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Node : MonoBehaviour {

    public Color hoverColor;

    [SerializeField]
    Transform placeToBuild;

    Renderer renderer;
    Color startColor;

    void Start ()
    {
        renderer = GetComponent<Renderer>();
        startColor = renderer.material.color;
	}

#region 鼠标悬浮时变色
    private void OnMouseEnter()
    {
        Debug.Log("qwe");
        renderer.material.color = hoverColor;
    }

    private void OnMouseExit()
    {
        renderer.material.color = startColor;

    }
    #endregion

    private void OnMouseDown()
    {
        //Debug.Log("Click");
        GameObject turretPrefab = BuildManager.instance.GetTurretToBuild();
        Instantiate(turretPrefab, placeToBuild.position, placeToBuild.rotation);
    }
}
