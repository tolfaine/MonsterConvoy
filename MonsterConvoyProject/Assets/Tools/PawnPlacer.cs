using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEditor;

public class PawnPlacer : EditorWindow {

    public GameObject pawnPrefab;

    PlaceType.Place nextPawn = PlaceType.Place.TERRAIN;

    bool enabled = false;
    bool createPaths = false;

    [MenuItem("Window/Pawn Placer")]
    static void CreateWindow()
    {
        PawnPlacer window = (PawnPlacer)EditorWindow.GetWindow<PawnPlacer>("Pawn Placer", true, new System.Type[] { System.Type.GetType("UnityEditor.InspectorWindow,UnityEditor.dll") });
        window.Show();
    }

    private void OnEnable()
    {
        SceneView.onSceneGUIDelegate += this.OnSceneGUI;
    }
    private void OnDisable()
    {
        SceneView.onSceneGUIDelegate -= this.OnSceneGUI;
    }
    private void OnGUI()
    {
        enabled = EditorGUILayout.Toggle("Enabled", enabled) ;
        nextPawn = (PlaceType.Place)EditorGUILayout.EnumPopup("Pawn Type", nextPawn);

        createPaths = EditorGUILayout.Toggle("Create Paths", createPaths);
    }

    GameObject activeSelection;
    private void OnSceneGUI(SceneView sceneview)
    {
        Event e = Event.current;
        
        Ray camRay = HandleUtility.GUIPointToWorldRay(e.mousePosition);
        RaycastHit mapHit;

        if (e.type == EventType.mouseDown && enabled && e.button == 0)
        {
            Physics.Raycast(camRay, out mapHit, float.MaxValue, LayerMask.GetMask("Map"));
            PawnManager.Instance().CreatePawn(nextPawn, mapHit.point, Quaternion.Euler(0, 0, 0));
        }

        if (e.type == EventType.mouseDown && createPaths && e.button == 1)
        {
            if (activeSelection == null)
                activeSelection = Selection.activeGameObject;
            else if (activeSelection != Selection.activeGameObject && Selection.activeGameObject.tag.Equals("MapNode"))
            {
                activeSelection.GetComponent<NodeConnections>().AddNeighbour(Selection.activeGameObject);
                activeSelection = Selection.activeGameObject;
            }
            else
                activeSelection = null;
        }
    }
}
