using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class TitleScreen : MonoBehaviour
{
    [SerializeField] int inGameSceneIndex = 1;
    // Update is called once per frame
    void Update()
    {
#if UNITY_EDITOR
        if (Input.GetMouseButtonDown(0))
        {
            SceneManager.LoadScene(inGameSceneIndex);
        }
#else
        if (Input.touchCount > 0)
        {
            SceneManager.LoadScene(inGameSceneIndex);
        }
#endif
    }
}
