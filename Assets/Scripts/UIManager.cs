using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.UI;

public class UIManager : MonoBehaviour
{
    [SerializeField] private Text scoreText;
    [SerializeField] private Text highScoreText;
    private void Start()
    {
        var score = GameManager.Instance.Score;
        var highScore = HighScoreManager.Instance.HighScore;
        scoreText.text = "Score: " + score;
        if (highScore < score)
        {
            HighScoreManager.Instance.SetHighScore(score);
            highScoreText.text = "New High Score!";
        }
        else
        {
            highScoreText.text = "High Score: " + highScore;
        }
    }

    public void OnPlayAgainButtonClicked()
    {
        SceneManager.LoadScene("Scenes/TitleScene");
        GameManager.Instance.GameStart();
    }
}
