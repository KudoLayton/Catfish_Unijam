using System;
using UnityEngine;
using Random = UnityEngine.Random;

public class CatPopupManager : MonoBehaviour
{
    [SerializeField] private Sprite[] catSprites;
    [SerializeField] private GameObject catPopupPrefab;
    [SerializeField] private float popupDuration;
    private const float Epsilon = 0.00001f;

    private Sprite GetRandomImage()
    {
        return catSprites[Mathf.FloorToInt((Random.value - Epsilon) * catSprites.Length)];
    }
    
    public void ShowCatPopup(GameObject catObject)
    {
        var popup = Instantiate(catPopupPrefab, catObject.transform);
        var behavior = popup.GetComponent<CatPopupBehavior>();
        behavior.SetDuration(popupDuration);
        popup.GetComponent<Renderer>().material.mainTexture = GetRandomImage().texture;
    }

    private void Start()
    {
        // Test();
    }

    private void Test()
    {
        var catObjects = GameObject.FindGameObjectsWithTag("Cat");
        foreach (var catObject in catObjects)
        {
            ShowCatPopup(catObject);
        }
    }
}