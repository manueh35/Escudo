using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;
using UnityEngine.UI;

public class ChangeColor : MonoBehaviour
{
    [SerializeField] private ScriptableColor lightColor;
    [SerializeField] private ScriptableColor darkColor;

    Image _image;
    TextMeshProUGUI _text;
    Camera _camera;

    public void Start()
    {
        UpdateTheme();
    }

    public void UpdateTheme()
    {
        //Si encuentra un componente Image cambia su color
        if (TryGetComponent(out Image _image))
        {
            if (SettingsManager.dark)
            {
                _image.color = darkColor.myColor;
            }

            else _image.color = lightColor.myColor;

        }

        //Si encuentra un component TextMeshProUGUI cambia su color
        if (TryGetComponent(out TextMeshProUGUI _text))
        {
            if (SettingsManager.dark)
            {
                _text.color = darkColor.myColor;
            }

            else _text.color = lightColor.myColor;

        }

        if (TryGetComponent(out Camera _camera))
        {
            if (SettingsManager.dark)
            {
                _camera.backgroundColor = darkColor.myColor;
            }

            else _camera.backgroundColor = lightColor.myColor;

        }
    }

}
