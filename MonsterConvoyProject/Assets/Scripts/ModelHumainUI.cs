using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ModelHumainUI : ModelUI
{

    public CaractHumainStuff caractStuff;
    public CaractHumainCheveux caractCheveux;
    public GameObject Tete;


    public void Start()
    {

        Init();
    }

    public void Init()
    {
        foreach (Transform child in Tete.transform)
        {
            if (child.gameObject.activeSelf)
            {
                HumainHeadUI headUI = child.gameObject.GetComponent<HumainHeadUI>();
                if(headUI!= null)
                    caractCheveux = CaractHumainCheveux.GetCaractHumainOfEnum(headUI._enumcheveux);
            }
           
        }
    }
    public void RandomCheveux()
    {
        SetCaract(CaractHumainCheveux.GetRandomCarac());
    }

    public void SetCaract(CaractHumainCheveux caract)
    {
        caractCheveux = caract;

        foreach (Transform child in Tete.transform)
        {
            HumainHeadUI headUI = child.gameObject.GetComponent<HumainHeadUI>();
            if (headUI != null)
            {
                if (headUI._enumcheveux == caract.enumCaract)
                    child.gameObject.SetActive(true);
                else
                    child.gameObject.SetActive(false);
            }
        }
    }
}
