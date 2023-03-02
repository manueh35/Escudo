using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;
using TMPro;
using UnityEngine.UI;

public class HubManager : MonoBehaviour
{
    [Header("RANK TEXTS")]
    [SerializeField] TextMeshProUGUI rankHeader;
    [SerializeField] TextMeshProUGUI rankText;
    [SerializeField] TextMeshProUGUI levelText;
    [SerializeField] string[] flavorText;

    [Header("SCORE TEXTS")]
    [SerializeField] TextMeshProUGUI PasswordScore;
    [SerializeField] TextMeshProUGUI SpamScore;
    [SerializeField] TextMeshProUGUI PopupScore;

    [Header("BUTTONS")]
    [SerializeField] GameObject[] gameButtons;
    [SerializeField] GameObject[] locked;

    private void Start()
    {
        UpdateHubText();                            //Actualizamos el texto
    }


    public void GoToScene(string scene) 
    {
        SceneManager.LoadScene(scene);
    }

    public void Quit()
    {
        Application.Quit();
    }

    public void UpdateHubText()
    {
        UpdateRankTexts();
        UpdateHighscores();
    }

    private void UpdateRankTexts()
    {
        switch (ExpTracker.Instance.currentLevel) //Segun el nivel almacenado en el tracker...
        {
            case PlayerLevel.Aprendiz:

                rankHeader.text = "APRENDIZ".ToString();
                levelText.text = "NIVEL 1".ToString();
                rankText.text = flavorText[0];
                gameButtons[0].GetComponent<Button>().interactable = true;

                break;

            case PlayerLevel.Principiante:

                rankHeader.text = "PRINCIPIANTE".ToString();
                levelText.text = "NIVEL 2".ToString();
                rankText.text = flavorText[1];
                gameButtons[0].GetComponent<Button>().interactable = true;
                gameButtons[1].GetComponent<Button>().interactable = true;
                locked[0].SetActive(false);

                break;

            case PlayerLevel.Avanzado:

                rankHeader.text = "AVANZADO".ToString();
                levelText.text = "NIVEL 3".ToString();
                rankText.text = flavorText[2];
                gameButtons[0].GetComponent<Button>().interactable = true;
                gameButtons[1].GetComponent<Button>().interactable = true;
                gameButtons[2].GetComponent<Button>().interactable = true;
                locked[0].SetActive(false);
                locked[1].SetActive(false);

                break;

            case PlayerLevel.Experto:

                rankHeader.text = "EXPERTO".ToString();
                levelText.text = "NIVEL 4".ToString();
                rankText.text = flavorText[3];
                gameButtons[0].GetComponent<Button>().interactable = true;
                gameButtons[1].GetComponent<Button>().interactable = true;
                gameButtons[2].GetComponent<Button>().interactable = true;
                locked[0].SetActive(false);
                locked[1].SetActive(false);

                break;

            case PlayerLevel.Maestro:

                rankHeader.text = "MAESTRO".ToString();
                levelText.text = "NIVEL 5".ToString();
                rankText.text = flavorText[4];
                gameButtons[0].GetComponent<Button>().interactable = true;
                gameButtons[1].GetComponent<Button>().interactable = true;
                gameButtons[2].GetComponent<Button>().interactable = true;
                locked[0].SetActive(false);
                locked[1].SetActive(false);

                break;
        }
    }

    private void UpdateHighscores()
    {
        PasswordScore.text = "PUNTUACIÓN MÁXIMA: " + ExpTracker.Instance.PasswordHighscore.ToString();
        SpamScore.text = "PUNTUACIÓN MÁXIMA: " + ExpTracker.Instance.SpamHighscore.ToString();
        PopupScore.text = "PUNTUACIÓN MÁXIMA: " + ExpTracker.Instance.PopupsHighscore.ToString();
    }
}
