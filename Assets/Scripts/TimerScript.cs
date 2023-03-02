using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using TMPro;

public class TimerScript : MonoBehaviour
{

    public float timer = 60;
    [SerializeField] private TextMeshProUGUI textTimer;
    [SerializeField] private GameObject summary;

    private ExpHandler handler;

    // Start is called before the first frame update
    void Start()
    {
        handler = FindObjectOfType<ExpHandler>();
        timer = 30;
    }

    // Update is called once per frame
    void Update()
    {
        if (timer > 0)
        {
            timer -= Time.deltaTime;
            textTimer.text = "" + timer.ToString("f0");
        }

        if (timer <= 0 && !summary.activeInHierarchy)
        {
            handler.ShowSummary();
        }

    }
}
