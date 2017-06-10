using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.SceneManagement;

public class MutationSelection : MonoBehaviour {

    public int idMonster;
    public CaractMonster mutation;
    public GameObject monsterIcon;
    public GameObject mutationIcon;
    public Text mutationName;
    Scene currentScene;

    private void Start()
    {
        MutationManager mutationManager = GameObject.FindGameObjectWithTag("MutationManager").GetComponent<MutationManager>();
        CreaturesData creatureData = GameObject.FindGameObjectWithTag("CreaturesData").GetComponent<CreaturesData>();

        if (mutationManager.allMutations.Count > 0)
        {
            MutationManager.MutationData data = mutationManager.GetRandomMutation(1)[0];

            idMonster = data.nIdMonster;
            mutation = CaractMonster.GetCaractMonsterOfEnum(data.mutation);

            monsterIcon.GetComponent<Image>().sprite = Resources.Load<Sprite>("Sprites/MonsterIcons/" + creatureData.GetFighterOfID<Monster>(CreatureType.Monster, idMonster).sName);
            mutationIcon.GetComponent<Image>().sprite = Resources.Load<Sprite>("Sprites/HumandexIcons/" + mutation.sName);
            char[] correctCapitalisation = mutation.sName.ToLowerInvariant().ToCharArray();
            correctCapitalisation[0] = correctCapitalisation[0].ToString().ToUpperInvariant()[0];
            mutationName.text = "";
            for (int i = 0; i < correctCapitalisation.Length; ++i)
                mutationName.text += correctCapitalisation[i];
        }
        else
            Destroy(gameObject);
     }

    void OnEnable()
    {
        SceneManager.sceneLoaded += OnSceneLoaded;
    }

    private void OnSceneLoaded(Scene scene, LoadSceneMode mode)
    {
        currentScene = scene;
    }

    public void SelectMutation() {
        MutationManager mutationManager = GameObject.FindGameObjectWithTag("MutationManager").GetComponent<MutationManager>();

        mutationManager.AddMutationID(idMonster, mutation);

        string s = "CAPITAL";
        SceneManager.LoadScene(s);

        // SceneManager.UnloadScene(currentScene);
        /*
         SceneManager.SetActiveScene(SceneManager.GetSceneByName(s));
         for (int i = 0; i < SceneManager.GetActiveScene().GetRootGameObjects().Length; i++)
         {
             GameObject go = SceneManager.GetActiveScene().GetRootGameObjects()[i];
             if (go.tag != "Spotligth")
                 SceneManager.GetActiveScene().GetRootGameObjects()[i].SetActive(true);
         }
         */

        GameObject g = GameObject.FindGameObjectWithTag("ProtoManager");
        ProtoScript ps = null;

        if (g != null)
        {
            ps = g.GetComponent<ProtoScript>();
            ps.map.EnterMap();
            ps.currentIndex++;
        }
    }
}
