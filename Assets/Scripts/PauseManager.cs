//using System.Collections;
//using UnityEngine;
//using TMPro;

//public class PauseManager : MonoBehaviour
//{
//    [Header("Pause UI")]
//    [SerializeField] private GameObject pausePanel;
//    [SerializeField] private TMP_Text scoreText;

//    [Header("Countdown UI")]
//    [SerializeField] private GameObject countdownPanel;
//    [SerializeField] private TMP_Text countdownText;

//    [Header("Pause Key")]
//    [SerializeField] private KeyCode pauseKey = KeyCode.Escape;

//    [Header("Countdown Settings")]
//    [SerializeField] private float countdownTime = 1f;

//    private bool isPaused;
//    private bool isCountingDown;
//    private void Start()
//    {
//        isPaused = false;
//        isCountingDown = false;

//        pausePanel.SetActive(false);
//        countdownPanel.SetActive(false);

//        Time.timeScale = 1f;
//    }

//    private void Update()
//    {
//        if (Input.GetKeyDown(pauseKey))
//        {
//            TogglePause();
//        }
//    }

//    private void TogglePause()
//    {
//        if (isCountingDown)
//        {
//            return;
//        }

//        if (isPaused)
//        {
//            StartCoroutine(ResumeCountdown());
//        }
//        else
//        {
//            PauseGame();
//        }
//    }

//    private void PauseGame()
//    {
//        isPaused = true;

//        pausePanel.SetActive(true);

//        Time.timeScale = 0f;
//    }

//    private void ResumeGame()
//    {
//        isPaused = false;

//        pausePanel.SetActive(false);

//        Time.timeScale = 1f;
//    }

//    private IEnumerator ResumeCountdown()
//    {
//        isCountingDown = true;

//        pausePanel.SetActive(false);
//        countdownPanel.SetActive(true);

//        countdownText.text = "3";
//        yield return new WaitForSecondsRealtime(countdownTime);

//        countdownText.text = "2";
//        yield return new WaitForSecondsRealtime(countdownTime);

//        countdownText.text = "1";
//        yield return new WaitForSecondsRealtime(countdownTime);

//        countdownPanel.SetActive(false);

//        Time.timeScale = 1f;

//        isPaused = false;
//        isCountingDown = false;
//    }
//}

using System.Collections;
using UnityEngine;
using UnityEngine.SceneManagement;
using TMPro;

public class PauseManager : MonoBehaviour
{
    [Header("Pause UI")]
    [SerializeField] private GameObject pausePanel;
    [SerializeField] private TMP_Text scoreText;

    [Header("Countdown UI")]
    [SerializeField] private GameObject countdownPanel;
    [SerializeField] private TMP_Text countdownText;

    [Header("Pause Key")]
    [SerializeField] private KeyCode pauseKey = KeyCode.Escape;

    [Header("Countdown Settings")]
    [SerializeField] private float countdownTime = 1f;

    [Header("Main Menu")]
    [SerializeField] private string mainMenuSceneName = "MainMenu";

    private bool isPaused;
    private bool isCountingDown;

    private void Start()
    {
        isPaused = false;
        isCountingDown = false;

        pausePanel.SetActive(false);
        countdownPanel.SetActive(false);

        Time.timeScale = 1f;
    }

    private void Update()
    {
        if (Input.GetKeyDown(pauseKey))
        {
            TogglePause();
        }
    }

    private void TogglePause()
    {
        if (isCountingDown)
        {
            return;
        }

        if (isPaused)
        {
            StartCoroutine(ResumeCountdown());
        }
        else
        {
            PauseGame();
        }
    }

    private void PauseGame()
    {
        isPaused = true;

        pausePanel.SetActive(true);

        Time.timeScale = 0f;
    }

    public void ContinueGame()
    {
        if (!isPaused || isCountingDown)
        {
            return;
        }

        StartCoroutine(ResumeCountdown());
    }

    private IEnumerator ResumeCountdown()
    {
        isCountingDown = true;

        pausePanel.SetActive(false);
        countdownPanel.SetActive(true);

        countdownText.text = "3";
        yield return new WaitForSecondsRealtime(countdownTime);

        countdownText.text = "2";
        yield return new WaitForSecondsRealtime(countdownTime);

        countdownText.text = "1";
        yield return new WaitForSecondsRealtime(countdownTime);

        countdownPanel.SetActive(false);

        Time.timeScale = 1f;

        isPaused = false;
        isCountingDown = false;
    }

    public void RestartGame()
    {
        Time.timeScale = 1f;

        SceneManager.LoadScene(SceneManager.GetActiveScene().buildIndex);
    }

    public void MainMenu()
    {
        Time.timeScale = 1f;

        SceneManager.LoadScene(mainMenuSceneName);
    }
}