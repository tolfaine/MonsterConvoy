using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class Tooltip : MonoBehaviour {

    public GameObject tooltip;

    void OnMouseOver()
    {
        tooltip.SetActive(true);
        tooltip.GetComponent<Text>().text = gameObject.GetComponent<Image>().sprite.ToString();
        tooltip.GetComponent<Text>().text += "\n" + "30"+"/30";//TODO
        tooltip.GetComponent<Image>().sprite = Resources.Load<Sprite>("Sprites/HumandexIcons/AILES");
        tooltip.transform.position = new Vector3(Input.mousePosition.x - tooltip.transform.localScale.x, Input.mousePosition.y, 0);
    }

    void OnMouseExit() {
        tooltip.SetActive(false);
    }

}
