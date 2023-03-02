using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.EventSystems;

public class actualizarDatos : MonoBehaviour
{
    cardDisplay cD;
    private cardWhatsapp wCard;
    private cardGmail gCard;


    public Image profilePic;
    public Text remitente;
    public Image container_inbox;
    public Text mensaje;
    public Text asunto;

    public RectTransform dialogTransform;


    // Start is called before the first frame update
    void Start()
    {
        cD = this.transform.parent.gameObject.GetComponent<cardDisplay>();
        if (cD != null)
        {
            if(cD.currentCard is cardWhatsapp)
            {
                wCard = (cardWhatsapp)cD.currentCard;
                profilePic.sprite = wCard.imagenPerfil;

            }
            else if (cD.currentCard is cardGmail)
            {
                gCard = (cardGmail)cD.currentCard;
                profilePic.sprite = gCard.imagenPerfil;
                asunto.text = gCard.asuntoMail;
                container_inbox.sprite = gCard.gmailInbox;
            }



            remitente.text = cD.currentCard.remitente_url;
            mensaje.text = cD.currentCard.contenido;
        }
        //profilePic.sprite = (cD.arrayTarjetas[cD.rnd]).
    }


}
