using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using TMPro;

public class ExpBarScript : MonoBehaviour
{
    [Header("GRAPHIC REFERENCES")]
    [SerializeField] private Image mask;
    [SerializeField] private Image fill;
    [SerializeField] private Color color;
    [Space(5)]

    [Header("TEXTS")]
    [SerializeField] private TextMeshProUGUI expText;

    private HubManager hub;

    private void Start()
    {
        hub = FindObjectOfType<HubManager>();
        StartCoroutine(ExpTracker.Instance.AddStoredExp());
    }

    void Update()
    {
       if (ExpTracker.Instance.currentLevel != PlayerLevel.Maestro)
       {

            UpdateExpBar();                                     //Actualizar barra de experiencia


            if (ExpTracker.Instance.currentExp >= ExpTracker.Instance.levelMaxExp)
            {
                LevelUp();                                      //Subir de nivel
                hub.UpdateHubText();                            //Actualizar textos del HUB
            }
       }   
    }

    void UpdateExpBar()
    {
        float currentOffset = ExpTracker.Instance.currentExp - ExpTracker.Instance.levelMinExp;    
        float maxOffset = ExpTracker.Instance.levelMaxExp - ExpTracker.Instance.levelMinExp;    
        float fillAmount = currentOffset / maxOffset;   //Calcular % de la barra

        mask.fillAmount = fillAmount;                   //Rellenar barra

        fill.color = color;                             //Actualizar color

        expText.text = (ExpTracker.Instance.currentExp - ExpTracker.Instance.levelMinExp).ToString() + "/"
            + (ExpTracker.Instance.levelMaxExp - ExpTracker.Instance.levelMinExp).ToString();  //Actualizar texto

        //Uso de StringBuilder ToString() para evitar garbage collection
    }

    void LevelUp()
    {
        //Actualizar experiencia mínima y máxima
        ExpTracker.Instance.levelMinExp = ExpTracker.Instance.levelMaxExp;
        ExpTracker.Instance.levelMaxExp += Mathf.RoundToInt(ExpTracker.Instance.levelMaxExp * 1.2f);

        //Subir de nivel
        ExpTracker.Instance.currentLevel += 1;
    }
}
