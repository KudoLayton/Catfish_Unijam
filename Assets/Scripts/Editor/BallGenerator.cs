using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEditor;


public class BallGenerator : Editor
{
    int numStone = 10, numTree = 6;
    GameObject map, tree, stone, marimo, jellyfish, ball;
    [MenuItem ("MapGen/Object Generator")]
    public static void ShowWindow()
    {
        EditorWindow.GetWindow(typeof(BallGenerator));
    }

    void OnGUI()
    {
        GUILayout.Label("Prefabs");
        map = (GameObject)EditorGUILayout.ObjectField("Map Object (Plane)", map, typeof(GameObject), true);
        tree = (GameObject)EditorGUILayout.ObjectField("Tree", tree, typeof(GameObject), true);
        stone = (GameObject)EditorGUILayout.ObjectField("Stone", stone, typeof(GameObject), true);
        marimo = (GameObject)EditorGUILayout.ObjectField("Marimo", marimo, typeof(GameObject), true);
        jellyfish = (GameObject)EditorGUILayout.ObjectField("Jellyfish", jellyfish, typeof(GameObject), true);
        ball = (GameObject)EditorGUILayout.ObjectField("Ball", ball, typeof(GameObject), true);

        GUILayout.Label("Params");
        numStone = EditorGUILayout.IntField("number of stone", numStone);
        numTree = EditorGUILayout.IntField("number of Trees", numTree);

        if (GUILayout.Button("Generate"))
        {
            float width = 20.0f; //to be changed
            float height = 20.0f; //to be changed

        }

    }
}
