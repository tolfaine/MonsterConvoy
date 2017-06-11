using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class Tooltip : MonoBehaviour {

    Caravane caravane;
    public GameObject mutationImage;
    public Text monsterName; 
    public int caravaneIndex = 0;

    List<GameObject> countedMutations = new List<GameObject>();
     
    void Awake()
    {
        caravane = GameObject.FindGameObjectWithTag("Caravane").GetComponent<Caravane>();
    }

    void OnEnable()
    {
        Monster monster = caravane.lFighters[caravaneIndex];
        monsterName.text = monster.sName;

        List<CaractMonster> muts = new List<CaractMonster>();

        if (MutationManager.Instance() != null)
            muts = MutationManager.Instance().GetMutationsWithId(monster.nID);

        for (int i = 0; i < countedMutations.Count; ++i)
        {
            Destroy(countedMutations[i]);
        }
        countedMutations.Clear();

        for (int i = 0; i <  muts.Count; i++)
        {
            mutationImage.SetActive(true);
            mutationImage.GetComponent<Image>().sprite = Resources.Load<Sprite>("Sprites/HumandexIcons/" + muts[i].sName);
            countedMutations.Add(Instantiate(mutationImage, transform));
            mutationImage.SetActive(false);
        }
        //xxx mutationImage.GetComponent<Image>().sprite = Resources.Load<Sprite>("Sprites/HumandexIcons/" + MutationManager.Instance().GetMutationWithId(monster.nID).sName); 
    }

}
