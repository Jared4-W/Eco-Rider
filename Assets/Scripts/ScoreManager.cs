using UnityEngine;
using TMPro;

public class ScoreManager : MonoBehaviour
{
    public static ScoreManager Instance;

    [Header("UI")]
    public TextMeshProUGUI scoreText;

    private int score = 0;

    private void Awake()
    {
        Instance = this;
    }

    private void Start()
    {
        UpdateUI();
    }

    public void AddPoints(int amount)
    {
        score += amount;

        UpdateUI();
    }

    private void UpdateUI()
    {
        scoreText.text = "Puntos: " + score.ToString();
    }
}
