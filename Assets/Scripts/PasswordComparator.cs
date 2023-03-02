using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;

public class PasswordComparator : MonoBehaviour
{
    public int debugCounter = 0;

    public PasswordGameState state;

    private string lastPassword;

    [SerializeField] private TMP_InputField _IF;

    [SerializeField] private int
        basePoints,
        pointsPerUppercase,
        pointsPerLowercase,
        pointsPerDigit;

    [SerializeField] private float
        multiplierPerMetachar, 
        multiplierPerLength;

    ExpHandler expHandler;

    [SerializeField] private TextMeshProUGUI levelText;
    [SerializeField] private TextMeshProUGUI flavorText;
    [SerializeField] private TextMeshProUGUI explanationText;

    [SerializeField] private string[] levelTextString;
    [SerializeField] [TextArea(6, 10)] private string[] flavorTextString;
    [SerializeField] [TextArea(6, 10)] private string[] explanationTextString;

    [SerializeField] private GameObject explanation;
    [SerializeField] private GameObject headers;
    [SerializeField] private GameObject score;
    [SerializeField] private GameObject skip;



    // Start is called before the first frame update
    void Start()
    {
        
        if (ExpTracker.Instance.PasswordHighscore > 0)
        {
            state = PasswordGameState.Gameplay; //Si ya se ha jugado una vez...
            StartGameIfTutorialIsOver();
        
        }

        else
        {
            levelText.text = levelTextString[0].ToString();
            flavorText.text = flavorTextString[0].ToString();
        }


        expHandler = FindObjectOfType<ExpHandler>();            //Reference to the handler

    }


    private void Update()
    {
        if (debugCounter > 2)
        {
            expHandler.ShowSummary();
            headers.SetActive(false);
        }
    }

    public void AnalyzeString()
    {

        string password = _IF.text;                             //Get IF text

        if (password.Length < 4 || password == lastPassword)    //If the password has less than 4 chars or its the same pass, return.
        {
            return;
        }

        lastPassword = password;

        bool                                                    //Initialize bools to false
            hasUppercase = false,
            hasLowercase = false,
            hasDigit = false,
            hasMetachar = false,
            hasDesiredLength = false;


        foreach (char c in password)                            //check string characters
        {

            if ((int)c >= 65 && (int)c <= 90)
            {
                hasUppercase = true;
            }
            if ((int)c >= 97 && (int)c <= 122)
            {
                hasLowercase = true;
            }
            if ((int)c >= 48 && (int)c <= 57)
            {
                hasDigit = true;
            }
            if ((int)c >= 33 && (int)c <= 47 || (int)c >= 58 && (int)c <= 64 || (int)c >= 91 && (int)c <= 96 || (int)c >= 123 && (int)c <= 126)
            {
               hasMetachar = true;
            }
            if (password.Length > 10)
            {
                hasDesiredLength = true;
            }
        }

        switch (state)
        {
            case PasswordGameState.PIN:

                if (hasDigit && !hasLowercase && !hasUppercase && !hasMetachar)
                {
                    explanationText.text = explanationTextString[0].ToString();
                    levelText.text = levelTextString[1].ToString();
                    flavorText.text = flavorTextString[1].ToString();
                    state += 1;
                }
                
                else return;

                ShowExplanation();

                break;

            case PasswordGameState.Basic:

                if (hasLowercase && hasUppercase && password.Length >= 6)
                {
                    explanationText.text = explanationTextString[1].ToString();
                    levelText.text = levelTextString[2].ToString();
                    flavorText.text = flavorTextString[2].ToString();
                    state += 1;
                }

                else return;

                ShowExplanation();

                break;

            case PasswordGameState.Advanced:

                if (hasLowercase && hasUppercase && hasDigit && hasMetachar)
                {
                    explanationText.text = explanationTextString[2].ToString();
                    levelText.text = levelTextString[3].ToString();
                    flavorText.text = flavorTextString[3].ToString();
                    state += 1;
                }

                else return;

                ShowExplanation();

                break;

            case PasswordGameState.Gameplay:

                bool isSecure = CalculatePoints(hasUppercase, hasLowercase, hasDigit, hasMetachar, hasDesiredLength) > 300;
                expHandler.AddScore(CalculatePoints(hasUppercase, hasLowercase, hasDigit, hasMetachar, hasDesiredLength), isSecure);

                if (isSecure)
                {
                    expHandler.rightAmount += 1;
                }

                else
                {
                    expHandler.wrongAmount += 1;
                }

                debugCounter += 1;

                break;

        }
        
    }

    private int CalculatePoints(bool hasUppercase, bool hasLowercase, bool hasDigit, bool hasMetachar, bool hasDesiredLength)
    {
        int amount = basePoints;

        if (hasUppercase) amount += pointsPerUppercase;

        if (hasLowercase) amount += pointsPerLowercase;

        if (hasDigit) amount += pointsPerDigit;

        if (hasMetachar) amount = (int) (amount * multiplierPerMetachar);

        if (hasDesiredLength) amount = (int)(amount * multiplierPerLength);

        return amount;
    }

    private void ShowExplanation()
    {
        headers.SetActive(false);
        explanation.SetActive(true);
    }

    public void StartGameIfTutorialIsOver()
    {
        if (state != PasswordGameState.Gameplay) return;

        else
        {
            StartGameAlready();
        }
    }

    public void StartGameAlready()
    {
        state = PasswordGameState.Gameplay;
        levelText.text = levelTextString[3].ToString();
        flavorText.text = flavorTextString[3].ToString();
        skip.SetActive(false);
        score.SetActive(true);

    }
}
