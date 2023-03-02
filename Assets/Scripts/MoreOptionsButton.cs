using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class MoreOptionsButton : MonoBehaviour
{
    [SerializeField] private GameObject desplegable;
    [SerializeField] private Animator desplegableAnim;

    public void ShowOptions()
    {
        if (DialogScript.menuOn){ MenuOff(); }
        else if (DialogScript.canPressMenu) { MenuOn(); } 
    }

    private void PlayAnim(bool status)
    {
        desplegableAnim.SetBool("menuOn", status);
        desplegableAnim.Play("Desplegable");
    }
    private void MenuOn()
    {
        DialogScript.menuOn = true;
        PlayAnim(DialogScript.menuOn);
    }

    public void MenuOff()
    {
        DialogScript.menuOn = false;
        PlayAnim(DialogScript.menuOn);
    }
}
