using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class WorstSaveSystemEver : MonoBehaviour
{
    public static WorstSaveSystemEver Instance { get; private set; }

    [SerializeField]
    private int exp, passwordHS, spamHS, popupHS, tutorial, charSize, audioOn, darkOn;


    private void Awake()                //Singleton, ("si existe una instancia, y no soy  yo, me destruyo")
    {
        if (Instance != null && Instance != this)
        {
            Destroy(this);
        }

        else
        {
            Instance = this;
            DontDestroyOnLoad(gameObject);
        }
    }

    private int BoolToInt(bool b)
    {
        if (b) return 1;
        else return 0;
    }

    private bool IntToBool(int i)
    {
        if (i == 1) return true;
        else return false;
    }

    private void OnApplicationQuit()
    {
        SaveData();
    }

    private void OnApplicationPause(bool pause)
    {
        if (pause)
        {
            SaveData();
        }     
    }

    [ContextMenu("Save")]
    public void SaveData()
    {

        exp = ExpTracker.Instance.currentExp + ExpTracker.Instance.storedExp;
        passwordHS = ExpTracker.Instance.PasswordHighscore;
        spamHS = ExpTracker.Instance.SpamHighscore;
        popupHS = ExpTracker.Instance.PopupsHighscore;

        tutorial = BoolToInt(ConversationManager.tutorial_completed);
        charSize = SettingsManager.charSize;
        darkOn = BoolToInt(SettingsManager.dark);
        audioOn = BoolToInt(SettingsManager.audioOn);

        PlayerPrefs.SetInt("CurrentExp", exp);
        PlayerPrefs.SetInt("Password", passwordHS);
        PlayerPrefs.SetInt("Spam", spamHS);
        PlayerPrefs.SetInt("Popups", popupHS);

        PlayerPrefs.SetInt("Tutorial", tutorial);
        PlayerPrefs.SetInt("CharSize", charSize);
        PlayerPrefs.SetInt("DarkOn", darkOn);
        PlayerPrefs.SetInt("AudioOn", audioOn);

        PlayerPrefs.Save();

        Debug.Log("Saved data");
    }

    [ContextMenu("Load")]
    public void LoadData()
    {
        exp = PlayerPrefs.GetInt("CurrentExp");
        passwordHS = PlayerPrefs.GetInt("Password");
        spamHS = PlayerPrefs.GetInt("Spam");
        popupHS = PlayerPrefs.GetInt("Popups");

        tutorial = PlayerPrefs.GetInt("Tutorial");
        charSize = PlayerPrefs.GetInt("CharSize");
        darkOn = PlayerPrefs.GetInt("DarkOn");
        audioOn = PlayerPrefs.GetInt("AudioOn");

        ExpTracker.Instance.storedExp = exp;           
        ExpTracker.Instance.PasswordHighscore = passwordHS;
        ExpTracker.Instance.SpamHighscore = spamHS;
        ExpTracker.Instance.PopupsHighscore = popupHS;

        ConversationManager.tutorial_completed = IntToBool(tutorial);
        SettingsManager.charSize = charSize;
        SettingsManager.dark = IntToBool(darkOn);
        SettingsManager.audioOn = IntToBool(audioOn);

        Debug.Log("Loaded data");
    }

    [ContextMenu("Clear")]
    public void ClearData()
    {

        PlayerPrefs.SetInt("CurrentExp", 0);
        PlayerPrefs.SetInt("Password", 0);
        PlayerPrefs.SetInt("Spam", 0);
        PlayerPrefs.SetInt("Popups", 0);

        PlayerPrefs.SetInt("Tutorial", 0);
        PlayerPrefs.SetInt("CharSize", 1);
        PlayerPrefs.SetInt("DarkOn", 0);
        PlayerPrefs.SetInt("AudioOn", 1);

        Debug.Log("Cleared data");
    }
}
