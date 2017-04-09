using UnityEngine;
using UnityEditor;
public class NodeWeight : MonoBehaviour {
    public int weight = 0;
    
    //Write the weight in the sceen view.
    void OnDrawGizmos() 
    {
        GUIStyle gS = new GUIStyle();
        gS.normal.textColor = Color.cyan;
        gS.fontSize = 18;
        gS.fontStyle = FontStyle.Bold;
        Handles.Label(transform.position + Vector3.up, weight.ToString(),gS);
    }
}
