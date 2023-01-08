using UnityEngine;

public class HighScoreManager : MonoBehaviour
{
    private static HighScoreManager _instance;
    public static HighScoreManager Instance => _instance;
    private int _highScore = -1;
    public int HighScore => _highScore;

    public void SetHighScore(int highScore)
    {
        _highScore = highScore;
        PlayerPrefs.SetInt("HighScore", _highScore);
        PlayerPrefs.Save();
    }

    void Start()
    {
        if (_instance != null)
        {
            Destroy(this);
            return;
        }

        _instance = this;
        DontDestroyOnLoad(this);
        _highScore = PlayerPrefs.GetInt("HighScore", -1);
    }
}