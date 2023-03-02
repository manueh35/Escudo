using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;
using UnityEngine.UI;

public class DialogScript : MonoBehaviour
{
    private bool cortinaOn = false;
    public static bool menuOn = false;
    public static bool canPressMenu = true;

    [SerializeField] private TextMeshProUGUI textDisplay;
    [SerializeField] private float typingSpeed;
    [SerializeField] private GameObject continueButton;
    [SerializeField] private GameObject skipButton;
    [SerializeField] private AudioSource audioManager;
    [SerializeField] private Transform candidoT;
    [SerializeField] private GameObject dialogDisplay;
    [SerializeField] private GameObject button_si;
    [SerializeField] private GameObject button_no;
    [SerializeField] private Animator cortinaAnim;
    [SerializeField] private GameObject cortina;

    public Conversation[] conversations;
    public Conversation currentConversation;
    public Conversation intro;
    public Conversation tutorial;

    private int index;
    public bool dialogOn = false;

    private void Start()
    {
        button_si.SetActive(false);
        button_no.SetActive(false);
        canPressMenu = true;
    }

    private void Update()
    {
        if (dialogOn)
        {
            if (textDisplay.text == currentConversation.lines[index].text)
            {
                if (currentConversation.lines[index].Question)
                {
                    button_si.SetActive(true);
                    button_no.SetActive(true);
                }
                else
                {
                    continueButton.SetActive(true);
                    skipButton.SetActive(false);
                }
            }
        }

        // dialogoOn || menuOn --> cortinaOn
        // dialogoOff && menuOff --> cortinaOff

        //Debug.Log(cortinaOn && !dialogOn && !menuOn);
        if (!(!cortinaOn && !(dialogOn || menuOn)))
        {
            cortinaOn = true;

            cortina.SetActive(cortinaOn);
            cortinaAnim.SetBool("cortinaOn", cortinaOn);
            cortinaAnim.Play("Cortina_alpha");
        }
        if (cortinaOn && !dialogOn && !menuOn)
        {
            canPressMenu = false;

            cortinaOn = false;
            cortinaAnim.SetBool("cortinaOn", cortinaOn);
            cortinaAnim.Play("Cortina_alpha");

            Invoke("UnActiveCortina", 1.2f);
        }
    }
    private void UnActiveCortina()
    {
        cortina.SetActive(false);
        canPressMenu = true;
    }

    IEnumerator Type()
    {
        ChangeAudioAndPlay();
        ChangeEmotionSprite();

        foreach (char letter in currentConversation.lines[index].text.ToCharArray())
        {
            textDisplay.text += letter;
            yield return new WaitForSeconds(typingSpeed);
        }
    }

    public void NextSentence()
    {
        button_si.SetActive(false);
        button_no.SetActive(false);

        continueButton.SetActive(false);
        skipButton.SetActive(true);

        if (index < currentConversation.lines.Length - 1)
        {
            index++;
            textDisplay.text = "";
            StartCoroutine(Type());

        }
        else
        {
            textDisplay.text = "";
            continueButton.SetActive(false);
            skipButton.SetActive(true);
            ShowAfterDialog();
            NextConversation();
        }
    }

    private void NextConversation()
    {
        if (currentConversation.nextConversation != null)
        {
            ShowBeforeDialog();
            currentConversation = currentConversation.nextConversation;
            index = 0;
            StartCoroutine(Type());
        }
        else
        {
            index = 0;
            dialogOn = false;
            SetActiveAllChildren(candidoT, false);
            candidoT.GetChild(0).gameObject.SetActive(true);
        }
    }
    private void ChangeAudioAndPlay()
    {
        audioManager.clip = currentConversation.lines[index].audioClip;
        audioManager.Play();
    }
    private void ChangeEmotionSprite()
    {
        SetActiveAllChildren(candidoT.transform, false);

        if (currentConversation.lines[index].Emotion == Line.Mood.Normal)
        {
            candidoT.GetChild(0).gameObject.SetActive(true);
        }
        else if (currentConversation.lines[index].Emotion == Line.Mood.Alegre)
        {
            candidoT.GetChild(1).gameObject.SetActive(true);
        }
        else if (currentConversation.lines[index].Emotion == Line.Mood.Confuso)
        {
            candidoT.GetChild(2).gameObject.SetActive(true);
        }
        else if (currentConversation.lines[index].Emotion == Line.Mood.Preocupado)
        {
            candidoT.GetChild(3).gameObject.SetActive(true);
        }
        else if (currentConversation.lines[index].Emotion == Line.Mood.Afk)
        {
            candidoT.GetChild(4).gameObject.SetActive(true);
        }
        else
        {
            candidoT.GetChild(5).gameObject.SetActive(true);
        }
    }
    private void SetActiveAllChildren(Transform transform, bool value)
    {
        foreach (Transform child in transform)
        {
            child.gameObject.SetActive(value);
            SetActiveAllChildren(child, value);
        }
    }
    

    private void ShowBeforeDialog()
    {
        dialogDisplay.SetActive(true);
    }
    private void ShowAfterDialog()
    {
        dialogDisplay.SetActive(false); 
    }

    public void StartConversation(Conversation conversation)
    {
        dialogOn = true;
        ShowBeforeDialog();
        currentConversation = conversation;
        StartCoroutine(Type());
    }

    public void Skip()
    {
        StopAllCoroutines();
        textDisplay.text = currentConversation.lines[index].text;
        audioManager.Stop();
    }
}
