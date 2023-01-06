using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using com.zibra.liquid.DataStructures;
using com.zibra.liquid.Manipulators;
using com.zibra.liquid.SDFObjects;
using com.zibra.liquid.Utilities;
using com.zibra.liquid.Analytics;
using System;
using System.Collections.ObjectModel;
using UnityEditor;

namespace com.zibra.liquid.Solver
{
    public class MapGeneratorEditor : EditorWindow
    {


        GameObject blockPrefab;
        GameObject blockParent;
        GameObject liquidPrefab;
        GameObject liquidParent;
        GameObject emitterPrefab;
        GameObject currentliquid;
        GameObject reflectionProbe;

        float inputZ = 0.0f, inputY = 0.0f, inputW = 0.0f, inputH = 0.0f;

        [MenuItem("MapUI/Map Generate")]
        public static void ShowWindow()
        {
            EditorWindow.GetWindow(typeof(MapGeneratorEditor));
        }

        void OnGUI()
        {
            GUILayout.Label("초기 세팅", EditorStyles.boldLabel);
            
            blockPrefab = (GameObject)EditorGUILayout.ObjectField("blockPrefab", blockPrefab, typeof(GameObject), true);
            blockParent = (GameObject)EditorGUILayout.ObjectField("blockParent", blockParent, typeof(GameObject), true);
            liquidPrefab = (GameObject)EditorGUILayout.ObjectField("liquidPrefab", liquidPrefab, typeof(GameObject), true);
            liquidParent = (GameObject)EditorGUILayout.ObjectField("liquidParent", liquidParent, typeof(GameObject), true);
            emitterPrefab = (GameObject)EditorGUILayout.ObjectField("emitterPrefab", emitterPrefab, typeof(GameObject), true);
            reflectionProbe = (GameObject)EditorGUILayout.ObjectField("reflectionProbe", reflectionProbe, typeof(GameObject), true);


            GUILayout.Label("블럭 생성하기", EditorStyles.boldLabel);
            inputZ = EditorGUILayout.FloatField("Z", inputZ);
            inputY = EditorGUILayout.FloatField("Y", inputY);
            inputW = EditorGUILayout.FloatField("Width", inputW);
            inputH = EditorGUILayout.FloatField("Height", inputH);
            if (GUILayout.Button("생성하기")) {
                GenerateBlock(inputZ, inputY, inputW, inputH);
            }


            GUILayout.Label("맵 초기화", EditorStyles.boldLabel);
            inputW = EditorGUILayout.FloatField("Width", inputW);
            inputH = EditorGUILayout.FloatField("Height", inputH);
            if (GUILayout.Button("초기화하기")) {
                foreach (Transform child in blockParent.transform) {
                    DestroyImmediate(child.gameObject);
                }
                foreach (Transform child in liquidParent.transform) {
                    DestroyImmediate(child.gameObject);
                }
                GenerateIdleMap(inputW, inputH);
            }



            
        }
        public GameObject GenerateBlock(float centerZ, float centerY, float width, float height)
        {
                var newCube = Instantiate(blockPrefab);
                newCube.transform.SetParent(blockParent.transform);
                newCube.transform.localPosition = new Vector3(0.0f, centerY, centerZ);
                newCube.transform.localScale = new Vector3(1.0f, height, width);
                newCube.transform.localRotation = Quaternion.identity;
                return newCube;
        }
        public GameObject GenerateLiquid(float width, float height)
        {
            var newLiquid = Instantiate(liquidPrefab);
            newLiquid.transform.SetParent(liquidParent.transform);
            newLiquid.transform.localPosition = new Vector3(0.0f, 0.0f, 0.0f);
            newLiquid.transform.localRotation = Quaternion.identity;
            double size = 2 * Mathf.Pow((width * width + height * height + width + height + 0.5f), 0.5f);
            newLiquid.GetComponent<ZibraLiquid>().ContainerSize = new Vector3(1, (float)size, (float)size);
            return newLiquid;
        }
        public GameObject GenerateEmit(float centerZ, float centerY, GameObject liquid)
        {
            var newEmit = Instantiate(emitterPrefab);
            newEmit.transform.SetParent(liquid.transform);
            newEmit.transform.localPosition = new Vector3(0.0f, centerY, centerZ);
            newEmit.transform.localRotation = Quaternion.identity;
            return newEmit;
        }
        public void GenerateIdleMap(float sizeZ, float sizeY)
        {
            GenerateBlock(0, sizeY + 0.5f, 2 * sizeZ + 2, 1);
            GenerateBlock(0, - sizeY - 0.5f, 2 * sizeZ + 2, 1);
            GenerateBlock(sizeZ + 0.5f, 0, 1, 2 * sizeY + 2);
            GenerateBlock(- sizeZ - 0.5f, 0, 1, 2 * sizeY + 2);
            GameObject liquid = GenerateLiquid(sizeZ, sizeY);
            ReadOnlyCollection<ZibraLiquidCollider> colliderList = liquid.GetComponent<ZibraLiquid>().GetColliderList();
        }
    }
}

