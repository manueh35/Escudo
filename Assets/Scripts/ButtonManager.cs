using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.UI;
using TMPro;

public class ButtonManager : MonoBehaviour
{
    public bool menuOn = false;
    public Animator ButtonAnim;
    public Animator CortinaAnim;

    public GameObject cortina;
    public Button menuButton;

    [SerializeField]
    private DialogScript dialog;

    [SerializeField]
    private Transform speakerT;
    [SerializeField]
    private Image small;
    [SerializeField]
    private Image medium;
    [SerializeField]
    private Image big;

    [SerializeField]
    private TextMeshProUGUI textDisplay;

    private bool theme_day = true;
    [SerializeField]
    private Transform themeT;

    private void Start()
    {
        cortina.SetActive(false);
    }

    public void ShowMenu()
    {
        cortina.SetActive(true);
        
        if (menuOn)
        {
            // Hide Menu
            menuOn = false;
            ButtonAnim.SetBool("menuOn", menuOn);
            if (!dialog.dialogOn)
            {
                CortinaAnim.SetBool("cortinaOn", menuOn);
                Invoke("UnActiveCortina", 1.2f);
            }
        }
        else
        {
            // Show Menu
            menuOn = true;
            ButtonAnim.SetBool("menuOn", menuOn);
            CortinaAnim.SetBool("cortinaOn", menuOn);
        }
        
        ButtonAnim.Play("Desplegable");
        CortinaAnim.Play("Cortina_alpha");

        StartCoroutine(ExampleCoroutine(1));
    }

    private void UnActiveCortina()
    {
        cortina.SetActive(false);
    }

    IEnumerator ExampleCoroutine(int seconds)
    {
        menuButton.interactable = false;
        yield return new WaitForSeconds(seconds);
        menuButton.interactable = true;
    }

   

    public void ChangeTheme()
    {
        if (theme_day)
        {
            theme_day = false;
            themeT.GetChild(0).gameObject.SetActive(false);
            themeT.GetChild(1).gameObject.SetActive(true);
        }
        else
        {
            theme_day = true;
            themeT.GetChild(0).gameObject.SetActive(true);
            themeT.GetChild(1).gameObject.SetActive(false);
        }
    }

    public void ChangeSizeSmall()
    {
        textDisplay.fontSize = 40;
        ChangeAlpha(small, 1f);
        ChangeAlpha(medium, 0.4f);
        ChangeAlpha(big, 0.4f);
    }

    public void ChangeSizeMedium()
    {
        textDisplay.fontSize = 48;
        ChangeAlpha(small, 0.4f);
        ChangeAlpha(medium, 1f);
        ChangeAlpha(big, 0.4f);
    }
    public void ChangeSizeBig()
    {
        textDisplay.fontSize = 56;
        ChangeAlpha(small, 0.4f);
        ChangeAlpha(medium, 0.4f);
        ChangeAlpha(big, 1f);
    }

    public void ChangeAlpha(Image image, float alpha)
    {
        var tempColor = image.color;
        tempColor.a = alpha;
        image.color = tempColor;
    }

    public void GoToScene(string scene)
    {
        SceneManager.LoadScene(scene);
    }
}

