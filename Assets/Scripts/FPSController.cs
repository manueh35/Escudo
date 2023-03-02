using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class FPSController : MonoBehaviour
{
    public static FPSController Instance { get; private set; }

    private void Awake()                //Singleton, ("si existe una instancia, y no soy yo, me destruyo")
    {
        if (Instance != null && Instance != this)
        {
            Destroy(this);
        }

        else
        {
            Instance = this;
            SetFrameRate();
            DontDestroyOnLoad(gameObject);
        }
    }

    private void SetFrameRate()
    {
        QualitySettings.vSyncCount = 0;
        Application.targetFrameRate = 60;
    }

}
