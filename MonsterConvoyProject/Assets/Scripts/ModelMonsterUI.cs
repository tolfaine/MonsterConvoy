using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ModelMonsterUI : ModelUI
{
    public CaractMonster caractMonster;
    public GameObject Mutations;
    public CaractMonster._enumCaractMonster permanentMutation;
    public CaractMonster permanentCarMutation;

    public void Start()
    {
        if (permanentMutation != CaractMonster._enumCaractMonster.NONE)
            permanentCarMutation = CaractMonster.GetCaractMonsterOfEnum(permanentMutation);
    }

    public void SetCaract(CaractMonster caract)
    {
        caractMonster = caract;

        foreach (Transform child in Mutations.transform)
        {
            MonsterMutationUI mutationUI = child.gameObject.GetComponent<MonsterMutationUI>();

            if (mutationUI != null)
            {
                if (mutationUI._enumMutations == caract.enumCaract)
                    child.gameObject.SetActive(true);
                else
                    child.gameObject.SetActive(false);
            }
        }
    }
}
