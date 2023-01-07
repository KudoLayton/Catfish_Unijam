using System;
using System.Collections;
using UnityEngine;
using Random = UnityEngine.Random;

public class MarimoGenerator : MonoBehaviour
{
    [SerializeField] private float spawnRangeX;
    [SerializeField] private float spawnRangeZ;
    [SerializeField] private float spawnDelay;
    [SerializeField] private GameObject marimoPrefab;
    [SerializeField] private GameObject map;
    
    public void SpawnMarimo()
    {
        StartCoroutine(WaitAndCreateMarimo());
    }
    
    private IEnumerator WaitAndCreateMarimo()
    {
        yield return new WaitForSeconds(spawnDelay);
        var x = Random.Range(-spawnRangeX, spawnRangeX);
        var z = Random.Range(-spawnRangeZ, spawnRangeZ);
        var positionOnPlane = new Vector3(x, 0, z);
        var marimo = Instantiate(marimoPrefab, positionOnPlane, Quaternion.identity);
        marimo.transform.parent = map.transform;
    }
}