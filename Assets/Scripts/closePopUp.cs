using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class closePopUp : MonoBehaviour
{
    
    [SerializeField]
    private Animator close;
    [SerializeField]
    private ParticleSystem closeParticles;

    private float myTimer;

    private Canvas canvas;
    GeneratePopUp genPopUp;

    ExpHandler handler;

    bool onlyCallCloseOnce = true;
    
    // Start is called before the first frame update
    void Start()
    {
        myTimer = 0;

        handler = FindObjectOfType<ExpHandler>();




        GameObject g = GameObject.Find("PopUpGenerator");
        if (g != null)
        {
            genPopUp = g.GetComponent<GeneratePopUp>();
        }
        
    }

    private void Update()
    {
        if (myTimer > 3)
        {
            if (onlyCallCloseOnce)
            {
                CloseThisPopUp();
            }        
        }

        else myTimer += Time.deltaTime;
    }

    public void CloseThisPopUp()
    {
        onlyCallCloseOnce = false;

        close.SetBool("closeButtonPressed",true);

        if (myTimer < 3)
        {
            handler.AddScore((int)(200 / myTimer), true);
            handler.rightAmount++;
        }

        else
        {
            handler.AddScore(-1, false);
            handler.wrongAmount++;
        }

    }
    public void playParticleEffect()
    {
        //Nota de goragar: creo que esto no va a furular por la UI, me informaré luego
        ParticleSystem particles = Instantiate(closeParticles, this.transform) as ParticleSystem;
        particles.Play();
    }

    public void destroyPopUp()
    {
        //genPopUp.numberPopUpsClosed++;
        genPopUp.popUpActive = false;

        Destroy(this.gameObject);
    }
}
