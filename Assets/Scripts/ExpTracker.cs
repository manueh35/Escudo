using System.Collections;
using System.Collections.Generic;
using UnityEngine;


public enum PlayerLevel { Aprendiz, Principiante, Avanzado, Experto, Maestro } //Niveles del jugador

public class ExpTracker : MonoBehaviour
{
    public static ExpTracker Instance { get; private set; }

    [Header("MIN/MAX LEVEL EXPERIENCE")]
    public int levelMaxExp;             //Experiencia para subir al siguiente nivel
    public int levelMinExp;             //Experiencia de partida del nivel

    [Space(5)]
    [Header("TOTAL EXP EARNED")]
    public int currentExp;              //Experiencia obtenida por el jugador
    public int storedExp;               //La experiencia del minijuego se acumula en esta variable

    public PlayerLevel currentLevel;    //Nivel actual

    [Range(0.5f,5)]
    public float maxTimeToFill = 0f;    //Tiempo máximo de llenado de barra


    [Space(5)]
    [Header("GAMES HIGHSCORES")]
    public int SpamHighscore;
    public int PasswordHighscore;
    public int PopupsHighscore;

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
            WorstSaveSystemEver.Instance.LoadData();
        }
    }

    public IEnumerator AddStoredExp()
    {


        int pointsPerFrame =
            Mathf.RoundToInt((storedExp / maxTimeToFill) * 0.016f); //Calcular puntos por frame

        if (pointsPerFrame < 1) pointsPerFrame = 1;                 //Si el valor es inferior a 1, redondear a 1.

        while (storedExp > 0)
        {
            if (pointsPerFrame > storedExp)                         //Si la cantidad restante de puntos es menor que
            {                                                       //Los puntos que se añaden por frame, añadir el resto.
                currentExp += storedExp;
                storedExp -= storedExp;
            }

            else
            {
                storedExp -= pointsPerFrame;                       
                currentExp += pointsPerFrame;                       //Añadir puntos al score final
            }
            
            yield return new WaitForSeconds(0.016f);                //un frame, 1/60 = 0.016.
        }



    }
}
