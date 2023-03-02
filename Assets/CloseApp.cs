using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CloseApp : MonoBehaviour
{
    public void CloseThisApp()
    {
        if (ExpTracker.Instance.currentLevel == PlayerLevel.Maestro)
        {
            Debug.Log("Bye bye!");
            Application.Quit();
        }

        else return;
    }
}
