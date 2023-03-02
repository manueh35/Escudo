using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.EventSystems;


public class card : ScriptableObject
{
    public bool esFraudulenta;
    public string remitente_url;
    [TextArea]
    public string contenido;
    [TextArea(10,10)]
    public string explicacionTarjeta;
    public GameObject template;


    
}
