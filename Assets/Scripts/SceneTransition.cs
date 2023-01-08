using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class SceneTransition : MonoBehaviour
{
    [SerializeField] 
    private int inGameSceneIndex = 2;

    void Start()
    {
        SceneManager.LoadScene(inGameSceneIndex);
    }
}
