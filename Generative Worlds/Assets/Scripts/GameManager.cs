using UnityEngine;
using UnityEngine.UI;
using System.Collections;

public class GameManager : MonoBehaviour
{
    public static GameManager Instance { get; private set; }

    [Header("References")]
    public CharacterControl player;

    [Header("UI Elements")]
    public GameObject pausePanel;    
    public Text messageText;         
    public Text countdownText;       
    public Button continueButton;    

    [Header("Gameplay Settings")]
    public float gracePeriod = 3f; 
    public float pauseDuration = 3f;      
    public string[] hitMessages;          

    private bool isPaused = false;
    private Coroutine graceCoroutine;

    private void Awake()
    {
        if (Instance != null && Instance != this)
        {
            Destroy(gameObject);
            return;
        }

        Instance = this;
        DontDestroyOnLoad(gameObject);

        if (pausePanel)
            pausePanel.SetActive(false);
    }

    public void DisablePlayer()
    {
        if (player != null)
            player.enabled = false;
    }

    public void EnablePlayer()
    {
        if (player != null)
            player.enabled = true;
    }

    public void RespawnPlayer(Vector3 position)
    {
        if (player != null)
        {
            player.enabled = false;
            player.transform.position = position;
            player.velocity = Vector3.zero;
            player.enabled = true;
        }
    }



    public void PauseGame()
    {
        if (isPaused) return;
        StartCoroutine(ShowMessageAndPause());
    }

    private IEnumerator ShowMessageAndPause()
    {
        isPaused = true;

        string message = hitMessages.Length > 0
            ? hitMessages[Random.Range(0, hitMessages.Length)]
            : "Watch out!";

        if (pausePanel)
            pausePanel.SetActive(true);

        if (messageText)
            messageText.text = message;

        DisablePlayer();
        Time.timeScale = 0f;

        // Countdown
        float countdown = pauseDuration;
        while (countdown > 0f)
        {
            if (countdownText)
                countdownText.text = Mathf.Ceil(countdown).ToString();

            yield return new WaitForSecondsRealtime(1f);
            countdown -= 1f;
        }

        // Hide panel, resume time, re-enable player
        if (pausePanel)
            pausePanel.SetActive(false);

        Time.timeScale = 1f;
        EnablePlayer();
        isPaused = false;
    }


    private void OnContinuePressed()
    {
        if (pausePanel)
        {
            pausePanel.SetActive(false);
            continueButton.onClick.RemoveListener(OnContinuePressed);
        }

        StartCoroutine(UnpauseWithGracePeriod());
    }

    private IEnumerator UnpauseWithGracePeriod()
    {
        Time.timeScale = 1f;
        EnablePlayer();
        isPaused = false;

        if (gracePeriod > 0f)
        {
            if (graceCoroutine != null)
                StopCoroutine(graceCoroutine);

            graceCoroutine = StartCoroutine(GracePeriodCoroutine());
        }

        yield break;
    }

    private IEnumerator GracePeriodCoroutine()
    {
        yield return new WaitForSeconds(gracePeriod);
        PauseGame();
    }
}
