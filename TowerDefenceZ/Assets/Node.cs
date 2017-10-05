using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Node : MonoBehaviour {

    public Color hoverColor;

    private Renderer renderer;
    private Color startColor;

    void Start ()
    {
        renderer = GetComponent<Renderer>();
        startColor = renderer.material.color;
	}
	
	void Update () {
		
	}

    private void OnMouseEnter()
    {
        renderer.material.color = hoverColor;
    }

    private void OnMouseExit()
    {
        renderer.material.color = startColor;

    }
}
