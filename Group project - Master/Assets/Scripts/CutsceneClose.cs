using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CutsceneClose : MonoBehaviour
{

    /* Update is called once per frame
    void Update()
    {
        if (StartCountdown.gameStarted == true)
        {
            this.gameObject.SetActive(false);
        }
        else
        {
            this.gameObject.SetActive(true);
        }
    }
    */

    public void Active(bool state)
    {
        if (state)
        {
            this.gameObject.SetActive(true);
        }
        else
        {
            this.gameObject.SetActive(false);
        }
    }

}
