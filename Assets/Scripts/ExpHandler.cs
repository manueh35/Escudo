using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;

public class ExpHandler : MonoBehaviour
{

    public int currentScore = 0;

    [Header("SCORE TEXT")]
    [SerializeField] private TextMeshProUGUI scoreText;

    [Space(5)]
    [Header("STREAK")]
    public int streakThreshold;
    public float streakMultiplier;
    public int highestStreak = 0;
    public int currentStreak;
    [HideInInspector] public int rightAmount;
    [HideInInspector] public int wrongAmount;

    [Space(5)]
    [Header("FEEDBACK")]
    [SerializeField] private TextMeshProUGUI feedbackText;
    [SerializeField] private Animator feedbackAnim;
    public string[] feedbackStrings;
    public string[] negativeStrings;
    public string streakString;

    [Space(5)]
    [Range(0.5f, 5)]
    public float maxTimeToFill = 0f;

    [Space(5)]
    [Header("SUMMARY")]
    [SerializeField] private GameObject summary;
    [HideInInspector] public bool hasFinishedAdding = true;

    private void Update()
    {
        UpdateScoreText();                                  
    }

    private void UpdateScoreText()
    {
        scoreText.text = "PUNTUACIÓN: " + currentScore;
    }

    public void AddScore(int score, bool isPositive)
    {
        if (currentStreak >= streakThreshold)            //Si el jugador está en una racha >= al threshold, aumentar su ganancia
        {
            score = (int)(score * streakMultiplier);
        }

        if (isPositive)
        {
            currentStreak += 1;

            if (currentStreak > highestStreak)              //Trackear la racha más alta
            {
                highestStreak = currentStreak;
            }
        }
        
        ShowFeedbackText(isPositive);

        StartCoroutine(AddScoreSmoothly(score));
    }

    private IEnumerator AddScoreSmoothly(int score)
    {
        hasFinishedAdding = false;

        int pointsPerFrame =
            Mathf.RoundToInt((score / maxTimeToFill) * 0.016f);

        if (pointsPerFrame < 1) pointsPerFrame = 1;

        while (score > 0)
        {
            if (pointsPerFrame > score)
            {
                currentScore += score;
                score -= score;
            }

            else
            {
                score -= pointsPerFrame;
                currentScore += pointsPerFrame;
            }

            yield return new WaitForSeconds(0.016f); //un frame, 1/60 = 0.016.
        }

        hasFinishedAdding = true;
    }

    void ShowFeedbackText(bool isPositive)
    {
        if (isPositive)
        {
            if (currentStreak == streakThreshold)
            {
                feedbackText.text = streakString;
            }

            else
            {
                int rnd = Random.Range(0, feedbackStrings.Length);
                feedbackText.text = feedbackStrings[rnd];
            }
        }

        else
        {
            int rnd = Random.Range(0, negativeStrings.Length);
            feedbackText.text = negativeStrings[rnd];
        }

        feedbackAnim.Play("feedbackText", -1, 0);

    }

    public void ShowSummary()
    {
        summary.SetActive(true);
    }
}
