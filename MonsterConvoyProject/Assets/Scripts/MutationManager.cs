using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class MutationManager : MonoBehaviour {

    public static bool created = false;
    static MutationManager instance; 

    [System.Serializable]
    public class MutationData
    {

        public int nIdMonster;
        public CaractMonster._enumCaractMonster impossibleMutation;
        public CaractMonster._enumCaractMonster mutation;

        public MutationData(int id, CaractMonster mutationC, CaractMonster._enumCaractMonster impossibleMutationC)
        {
            nIdMonster = id;
            mutation = mutationC.enumCaract;
            impossibleMutation = impossibleMutationC;
        }
    }



    public List<MutationData> allMutations = new List<MutationData>(1);

    public static MutationManager Instance()
    {
        return instance; 
    }

    public void Awake()
    {
        if (instance == null)
            instance = this;

        if (!created)
        {
            DontDestroyOnLoad(transform.gameObject);
            created = true;
        }
        else
            Destroy(transform.gameObject);

        CaractMonster init =  CaractMonster.AILES;

        Initialise();
    }

    public void Start()
    {

    }

    private void Initialise()
    {
        CreaturePrefabManager prefabManager = GameObject.FindGameObjectWithTag("CreaturePrefabManager").GetComponent<CreaturePrefabManager>();

        foreach(CreaturePrefabManager.PrefabData prefabData in prefabManager.lMonsters)
        {
            ModelMonsterUI mm = prefabData.prefab.GetComponent<ModelMonsterUI>();
            allMutations.Add(new MutationData(prefabData.id, CaractMonster.NONE, mm.permanentMutation));
        }
    }

    public CaractMonster GetMutationWithId(int id)
    {
        foreach(MutationData mutationData in allMutations)
        {
            if(mutationData.nIdMonster ==id)
            {
                return CaractMonster.GetCaractMonsterOfEnum(mutationData.mutation);
            }

        }
  
        return CaractMonster.NONE;
    }

    public void AddMutationID( int id, CaractMonster mutation)
    {
        foreach (MutationData mutationData in allMutations)
        {
            if (mutationData.nIdMonster == id)
            {
                mutationData.mutation = mutation.enumCaract;
                return;
            }

        }
      //  
    }

    public List<MutationData> GetRandomMutation(int nbMutationRequested)
    {
        List<MutationData> list = new List<MutationData>(0);


        if (nbMutationRequested > 3)
            nbMutationRequested = 3;

        for(int i =0; i< nbMutationRequested; i++)
        {
            int randIndex = Random.Range(0, allMutations.Count);

            CaractMonster caract = CaractMonster.GetRandomCaracExept(allMutations[randIndex].impossibleMutation, allMutations[randIndex].mutation);
            MutationData data = new MutationData(allMutations[randIndex].nIdMonster, caract, allMutations[randIndex].impossibleMutation);

            while (list.Contains(data))
            {
                caract = CaractMonster.GetRandomCaracExept(allMutations[randIndex].impossibleMutation, allMutations[randIndex].mutation);
                data = new MutationData(allMutations[randIndex].nIdMonster, caract, allMutations[randIndex].impossibleMutation);
            }

            list.Add(data);
        }

        return list;

    }
}
