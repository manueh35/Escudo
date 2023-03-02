using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.EventSystems;
using TMPro;

public class deslizar : MonoBehaviour, IDragHandler ,IEndDragHandler
{
    cardDisplay cd;
    [SerializeField]
    private float _dampSpeed = .05f;
    
    public Image imagen;

    public TextMeshProUGUI textFraudeSeguro;
    

    private Vector3 originalPosition;
    private RectTransform _objetoRectTransform;
    private Vector3 _velocity = Vector3.zero;
    
    private bool _moving = false;

    ExpHandler expHandler; //GRG


    private void Awake()
    {
        textFraudeSeguro.enabled = false;

        _objetoRectTransform = transform as RectTransform;
        if(_objetoRectTransform!=null)
            originalPosition = _objetoRectTransform.position;

        cd = this.transform.parent.gameObject.GetComponent<cardDisplay>();

        expHandler = FindObjectOfType<ExpHandler>(); //GRG

    }
    public void OnDrag(PointerEventData eventData)
    {

        if (RectTransformUtility.ScreenPointToWorldPointInRectangle(_objetoRectTransform, eventData.position, eventData.pressEventCamera, out var globalMousePosition))
        {
            _objetoRectTransform.position = Vector3.SmoothDamp(_objetoRectTransform.position, globalMousePosition, ref _velocity, _dampSpeed);
            
        }
        _moving = true;
        
    }
    public void OnEndDrag(PointerEventData eventData)
    {
        
        _moving = false;

        if (textFraudeSeguro.enabled)
        {
            if ((textFraudeSeguro.text == "FRAUDULENTO" && cd.fraudulenta) || (textFraudeSeguro.text == "SEGURO" && !cd.fraudulenta))
            {
                expHandler.AddScore(300, true); //GRG
                expHandler.rightAmount++;
                cd.DestroyCurrentCard();
                cd.CheckAndPrepareNewCard();

            }

            else
            {
                //expHandler.AddScore(-1, false);
                expHandler.currentStreak = 0; //GRG
                cd.DestroyCurrentCard();
                cd.explanation.SetActive(true);
                expHandler.wrongAmount++;
            }
        }

        else
        {
            RecolocarCarta();
            return;
        }

        
    }
    
    public void RecolocarCarta()
    {
        _objetoRectTransform.position = originalPosition;
        _objetoRectTransform.rotation = Quaternion.Euler(0f, 0f, 0f);
    }


    // Update is called once per frame
    void Update()
    {

        
        
        if (_moving)
        {
            _objetoRectTransform.rotation = Quaternion.Euler(0f, 0f, (float)(-0.05 * (_objetoRectTransform.position.x - originalPosition.x)));
        }
        

    }
    private void OnTriggerEnter2D(Collider2D collision)
    {
        if (collision.name == "FraudeCollider")
        {
            textFraudeSeguro.text = "FRAUDULENTO";
            textFraudeSeguro.color = new Color32(255, 0, 0, 150);
            textFraudeSeguro.enabled = true;
            imagen.color = new Color32(255, 0, 0, 150);
        }
        else
        {
            textFraudeSeguro.text = "SEGURO";
            textFraudeSeguro.color = new Color32(0, 255, 0, 150);
            textFraudeSeguro.enabled = true;
            imagen.color = new Color32(0, 255, 0, 150);
        }
    }
    private void OnTriggerExit2D(Collider2D collision)
    {
        imagen.color = new Color32(255, 255, 255, 255);
        textFraudeSeguro.enabled = false;
    }
    
    
}
