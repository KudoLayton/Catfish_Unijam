using System;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.UI;

public class UIManager : MonoBehaviour
{
    [SerializeField] private Text[] scoreList;
    [SerializeField] private Text totalScore;

    private void Start()
    {
        string[] colors =
        {
            "White", "Green", "Blue", "Purple", "Gold", "Red"
        };
        Dictionary<string, int> fishTypes = new Dictionary<string, int>();
        foreach (var fishtype in GameManager.Instance.Settings.fishtypes)
        {
            fishTypes.Add(fishtype.color, fishtype.score);
        }

        int total = 0;
        for (int i = 0; i < 6; ++i)
        {
            var color = colors[i];
            int unit = fishTypes[color];
            int count = GameManager.Instance.GetScore(color);
            int prod = unit * count;
            scoreList[i].text = $"{count} x {unit} = {prod}";
            total += prod;
        }

        totalScore.text = $"Total: {total}";
    }

    private void Update()
    {
        if (Input.touchCount > 0)
        {
            // Debug.Log("clicked");
            SceneManager.LoadScene(0);
            GameManager.Instance.GameStart();
        }
    }
}