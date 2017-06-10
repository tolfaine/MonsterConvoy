using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.EventSystems;

public class MouseOver : MonoBehaviour {

    protected Color mouseOverColor = Color.red;
    protected Color mouseClickedColor = Color.red;
    protected Color baseColor;

    public bool bMouseOver = false;
    public bool bMouseClicking = false;
    public bool bClickProcessed = false;
    public bool bLastFrameClicking = false;

    protected virtual void Start()
    {
        baseColor = gameObject.GetComponentInChildren<Renderer>().material.color;
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
            {
                bMouseClicking = true;

                if (!bLastFrameClicking)
                {
                    bClickProcessed = false;
                }

                bLastFrameClicking = true;
            }
            else
            {
                bMouseClicking = false;
                if (bLastFrameClicking)
                {
                    bLastFrameClicking = false;
                }
            }
        }
    }

    protected virtual void ProcessStates()
    {
        if (bMouseClicking && !bClickProcessed)
        {
            bClickProcessed = true;
            Renderer[] allRenders = gameObject.GetComponentsInChildren<Renderer>();
            foreach(Renderer render in allRenders)
            {
                render.material.color = mouseClickedColor;    
            }
           // gameObject.GetComponent<Renderer>().material.color = mouseClickedColor;
        }
        else if (bMouseOver)
        {
            Renderer[] allRenders = gameObject.GetComponentsInChildren<Renderer>();
            foreach (Renderer render in allRenders)
            {
                render.material.color = mouseOverColor;
            }
         //   gameObject.GetComponent<Renderer>().material.color = mouseOverColor;
        }
        else
        {
            Renderer[] allRenders = gameObject.GetComponentsInChildren<Renderer>();
            foreach (Renderer render in allRenders)
            {
                render.material.color = baseColor;
            }
           // gameObject.GetComponent<Renderer>().material.color = baseColor;
        }
    }

    protected virtual void OnMouseOver()
    {
        if (!EventSystem.current.IsPointerOverGameObject())
        {
            bMouseOver = true;
        }
    }

    protected virtual void OnMouseExit()
    {
        if (!EventSystem.current.IsPointerOverGameObject())
        {
            bMouseOver = false;
        }
    }



}
