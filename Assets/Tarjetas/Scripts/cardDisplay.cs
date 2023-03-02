using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.EventSystems;
using TMPro;

public class cardDisplay : MonoBehaviour
{

    deslizar d;
    public List<card> listaTarjetas;
    public Transform t_transform;
    public int rnd=-1;
    public bool fraudulenta;
    public card currentCard;

    GameObject NuevaTarjeta;

    public GameObject explanation;
    private ExpHandler expHandler;

    private int counter = 0;

    private void Awake()
    {
        d= GetComponentInChildren<deslizar>();
        expHandler = FindObjectOfType<ExpHandler>();
        declararVariables();

        counter = 0;
    }

    public void declararVariables()
    {
        if (counter < 6 && listaTarjetas.Count > 0)
        {
            rnd = Random.Range(0, listaTarjetas.Count);
            NuevaTarjeta = Instantiate(listaTarjetas[rnd].template, t_transform.position, Quaternion.identity, t_transform) as GameObject;
            currentCard = listaTarjetas[rnd];

            listaTarjetas[rnd].template.SetActive(true);
            fraudulenta = listaTarjetas[rnd].esFraudulenta;

            explanation.GetComponent<TextMeshProUGUI>().text = listaTarjetas[rnd].explicacionTarjeta;

            counter++;
        }

        else
        {
            expHandler.ShowSummary();
        }

    }

    public void DestroyCurrentCard()
    {
        listaTarjetas.RemoveAt(rnd);
        Destroy(NuevaTarjeta);
    }
   
    public void CheckAndPrepareNewCard()
    {
        declararVariables();
    }
}
