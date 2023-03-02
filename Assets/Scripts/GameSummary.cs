using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;
using UnityEngine.SceneManagement;
using UnityEngine.UI;

public class GameSummary : MonoBehaviour
{
    [Header("SUMMARY")]
    [SerializeField] private GameObject summary;
    [SerializeField] private TextMeshProUGUI rights;
    [SerializeField] private TextMeshProUGUI wrongs;
    [SerializeField] private TextMeshProUGUI streak;
    [SerializeField] private TextMeshProUGUI finalScore;
    public Button menuButton;

    [SerializeField] bool
        isSpam,
        isPassword,
        isPopup;

    private ExpHandler expHandler;

    // Start is called before the first frame update
    void Start()
    {
        expHandler = FindObjectOfType<ExpHandler>();
        rights.text = expHandler.rightAmount.ToString();
        wrongs.text = expHandler.wrongAmount.ToString();
        streak.text = expHandler.highestStreak.ToString();
    }

    // Update is called once per frame
    void Update()
    {
        finalScore.text = expHandler.currentScore.ToString();

        if (expHandler.hasFinishedAdding) ActivateMenuButton();
    }

    void ActivateMenuButton()
    {
        menuButton.interactable = true;
    }

    public void AddExpToTracker()
    {
        ExpTracker.Instance.storedExp += expHandler.currentScore;

        if (ExpTracker.Instance.SpamHighscore < expHandler.currentScore && isSpam) //Actualizar highscore
        {
            ExpTracker.Instance.SpamHighscore = expHandler.currentScore;
        }

        if (ExpTracker.Instance.PasswordHighscore < expHandler.currentScore && isPassword) //Actualizar highscore
        {
            ExpTracker.Instance.PasswordHighscore = expHandler.currentScore;
        }

        if (ExpTracker.Instance.PopupsHighscore < expHandler.currentScore && isPopup) //Actualizar highscore
        {
            ExpTracker.Instance.PopupsHighscore = expHandler.currentScore;
        }

        SceneManager.LoadScene("MainMenu");
    }
}
