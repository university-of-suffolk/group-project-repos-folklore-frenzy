using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CutsceneClose : MonoBehaviour
{
    // Update is called once per frame
    void Update()
    {
        if (StartCountdown.gameStarted == true)
        {
            this.gameObject.SetActive(false);

        }
    }
}
