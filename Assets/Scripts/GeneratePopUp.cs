using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class GeneratePopUp : MonoBehaviour
{
    [SerializeField]
    private List<Image> _popUps;
    public bool popUpActive = true;
    [SerializeField]
    private RectTransform _rtSpawner;
    [SerializeField]
    private RectTransform _rtCanvas;
    private Image _createPopUp;

    private TimerScript _timerScript;

    public int numberPopUpsClosed;

    private void Awake()
    {
        _timerScript = GetComponent<TimerScript>();
    }
    // Start is called before the first frame update
    void Start()
    {
        changeGenPosition();
        _createPopUp = Instantiate(_popUps[0], _rtSpawner.transform);
    }

    // Update is called once per frame
    void Update()
    {
        if (!popUpActive && _timerScript.timer>0)
        {
            changeGenPosition();
            _createPopUp = Instantiate(_popUps[0], _rtSpawner.transform);
            popUpActive = true;
        }
    }

    public void changeGenPosition()
    {
        float maxX = (_rtCanvas.sizeDelta.x / 2 - _rtSpawner.sizeDelta.x);
        float maxY = (_rtCanvas.sizeDelta.y / 2 - _rtSpawner.sizeDelta.y);
        _rtSpawner.anchoredPosition = new Vector3(Random.Range(-maxX, maxX), Random.Range(-maxY, maxY), 0);
    }
}
