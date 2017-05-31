using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using System.IO;
using LitJson;
using UnityEngine.UI;

public class HumandexEntry
{   
    public int id;
    public bool discovered;
    public string name;
    public string tip;

   public HumandexEntry(JsonData data)
    {
        id = int.Parse(data["id"].ToString());
        discovered = bool.Parse(data["discovered"].ToString());
        name = data["name"].ToString();
        tip = data["tip"].ToString();
    }
}

public class HumandexDataHandler : MonoBehaviour
{

    public GameObject humandexIcon;
    public GameObject[] arrHumandexIcon;
    public int pageNumber = 0;
    private int numMonstersPerPage = 4;
    List<HumandexEntry> humandexEntries = new List<HumandexEntry>(); 

    void Start()
    {
        arrHumandexIcon = new GameObject[numMonstersPerPage];
        
        //TODO initilaise human dex icons in the correct position. 
        arrHumandexIcon[0] = GameObject.Instantiate(humandexIcon);
        arrHumandexIcon[0].transform.position = new Vector3(50, 50);

        arrHumandexIcon[1] = GameObject.Instantiate(humandexIcon);
        arrHumandexIcon[1].transform.position = new Vector3(50, 250);

        arrHumandexIcon[2] = GameObject.Instantiate(humandexIcon);
        arrHumandexIcon[2].transform.position = new Vector3(250, 50);

        arrHumandexIcon[3] = GameObject.Instantiate(humandexIcon);
        arrHumandexIcon[3].transform.position = new Vector3(250, 250);

        string path = File.ReadAllText(Application.dataPath + "/Ressources/HumandexInfo.json");
        JsonData jsHumanData = JsonMapper.ToObject(path);

        string type = "HumandexEntry";

        for (int i = 0; i < jsHumanData[type].Count; ++i)
        {
            JsonData humanInfo = jsHumanData[type][i];
            humandexEntries.Add(new HumandexEntry(humanInfo));
        }
        //Todo display 4 humandex entries per page. 
    }

    public void TurnPageRight()
    {
        if (pageNumber < (int)(humandexEntries.Count / numMonstersPerPage))
        {
            pageNumber++;
            UpdatePage();
        }
    }

    public void TurnPageLeft()
    {
        if (pageNumber > 0)
        {
            pageNumber--;
            UpdatePage();
        }
    }

    private void UpdatePage()
    {


        for (int i = 0; i < arrHumandexIcon.Length; i++)
        {

            if (humandexEntries[i + (numMonstersPerPage * pageNumber)].discovered)
            {
                string imagePath = (Application.dataPath + "/Sprites/HumandexIcons/" + humandexEntries[i + (numMonstersPerPage * pageNumber)].name);
                byte[] data = File.ReadAllBytes(imagePath);
                Texture2D texture = new Texture2D(64, 64, TextureFormat.ARGB32, false);
                texture.LoadImage(data);
                texture.name = Path.GetFileNameWithoutExtension(imagePath);

                Sprite newSprite = Resources.Load(Application.dataPath + "/Sprites/HumandexIcons/" + humandexEntries[i + (numMonstersPerPage * pageNumber)].name + ".png") as Sprite;
                arrHumandexIcon[i].GetComponent<Image>().sprite = newSprite;
               // arrHumandexIcon[i].GetComponent<Image>().sprite = Path.GetFileName(Application.dataPath + "/Sprites/HumandexIcons/" + humandexEntries[i + (numMonstersPerPage * pageNumber)].name + ".png");
            }
            else
            {
                //Display a question mark 
            }
        }
    }
}


