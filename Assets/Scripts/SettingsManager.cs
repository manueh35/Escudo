using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using TMPro;

public enum CharSize { small, medium, large };

public class SettingsManager : MonoBehaviour
{
    [Header("Theme")]
    [SerializeField] private Image lightTheme;
    [SerializeField] private Image darkTheme;
    [Space(10)]

    [Header("CharSize")]
    [SerializeField] private Image small;
    [SerializeField] private Image medium;
    [SerializeField] private Image large;

    [Space(10)]

    [Header("Speaker")]
    [SerializeField] private Image speakerOn;
    [SerializeField] private Image speakerOff;
    [Space(10)]

    public static int charSize = 1;
    public static bool audioOn = true, dark = false;

    private void Awake()
    {
        //Update icons based on loaded data

        UpdateVolume(audioOn);
        UpdateTheme(dark);
    }

    private void Update()
    {
        UpdateCharSize(charSize);
    }

    public void UpdateCharSize(int size)
    {
        charSize = size;

        switch (size)
        {
            case (int)CharSize.small:

                FetchAvaliableTexts(40);
                ChangeAlpha(small, 1f);
                ChangeAlpha(medium, 0.4f);
                ChangeAlpha(large, 0.4f);

                break;

            case (int)CharSize.medium:

                FetchAvaliableTexts(48);
                ChangeAlpha(small, 0.4f);
                ChangeAlpha(medium, 1f);
                ChangeAlpha(large, 0.4f);

                break;

            case (int)CharSize.large:

                FetchAvaliableTexts(56);
                ChangeAlpha(small, 0.4f);
                ChangeAlpha(medium, 0.4f);
                ChangeAlpha(large, 1f);

                break;
        }
    }

    private void FetchAvaliableTexts(int amount)
    {
        GameObject[] textAvaliable = GameObject.FindGameObjectsWithTag("AdjustableText");

        foreach (GameObject text in textAvaliable)
        {
            text.GetComponent<TextMeshProUGUI>().fontSize = amount;
        }
    }

    public void UpdateVolume(bool audio)
    {
        audioOn = audio;

        if (audio)
        {
            speakerOn.gameObject.SetActive(true);
            speakerOff.gameObject.SetActive(false);
        }
        else 
        {
            speakerOn.gameObject.SetActive(false);
            speakerOff.gameObject.SetActive(true);
        }
    }

    public void UpdateTheme(bool darkT)
    {
        if (darkT)
        {
            lightTheme.gameObject.SetActive(false);
            darkTheme.gameObject.SetActive(true);
            dark = true;

            ChangeColor[] objectsToChange = FindObjectsOfType<ChangeColor>();
            foreach (ChangeColor objectToChange in objectsToChange)
            {
                objectToChange.UpdateTheme();
            }
        }

        else
        {
            lightTheme.gameObject.SetActive(true);
            darkTheme.gameObject.SetActive(false);
            dark = false;

            ChangeColor[] objectsToChange = FindObjectsOfType<ChangeColor>();
            foreach (ChangeColor objectToChange in objectsToChange)
            {
                objectToChange.UpdateTheme();
            }
        }
    }

    public void ChangeAlpha(Image image, float alpha)
    {
        var tempColor = image.color;
        tempColor.a = alpha;
        image.color = tempColor;
    }
}
