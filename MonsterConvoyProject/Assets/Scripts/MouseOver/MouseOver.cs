using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class MouseOver : MonoBehaviour {

    protected Color mouseOverColor = Color.green;
    protected Color mouseClickedColor = Color.yellow;
    protected Color baseColor;

    public bool bMouseOver = false;
    public bool bMouseClicking = false;

    protected virtual void Start()
    {
        baseColor = gameObject.GetComponent<Renderer>().material.color;
    }
    protected virtual void Update()
    {
        ProcessInput();
        ProcessStates();
    }

    protected virtual void ProcessInput()
    {
        if (bMouseOver)
        {
            if (Input.GetMouseButton(0))
                bMouseClicking = true;
            else if (Input.GetMouseButtonUp(0))
                bMouseClicking = false;
        }
    }

    protected virtual void ProcessStates()
    {
        if (bMouseClicking)
        {
            gameObject.GetComponent<Renderer>().material.color = mouseClickedColor;
        }
        else if (bMouseOver)
        {
            gameObject.GetComponent<Renderer>().material.color = mouseOverColor;
        }
        else
        {
            gameObject.GetComponent<Renderer>().material.color = baseColor;
        }
    }

    protected virtual void OnMouseOver()
    {
        bMouseOver = true;
    }

    protected virtual void OnMouseExit()
    {
        bMouseOver = false;
    }



}
