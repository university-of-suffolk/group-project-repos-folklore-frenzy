using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;

public class LeaderBoardController : MonoBehaviour
{

    //[SerializeField] GameObject HighScore;
    //[SerializeField] GameObject SecondScore;
    //[SerializeField] GameObject ThirdScore;
    //[SerializeField] GameObject ForthScore;
    //[SerializeField] GameObject FithScore;

    [SerializeField] TextMeshProUGUI high_text;
    [SerializeField] TextMeshProUGUI second_text;
    [SerializeField] TextMeshProUGUI third_text;
    [SerializeField] TextMeshProUGUI fourth_text;
    [SerializeField] TextMeshProUGUI fith_text;

    // Update is called once per frame
    void Update()
    {
        if (PlayerPrefs.HasKey("Highscore"))
        {
            high_text.text = PlayerPrefs.GetFloat("Highscore").ToString();
        }
        else
        {
            high_text.gameObject.SetActive(false);
        }

        if (PlayerPrefs.HasKey("Secondscore"))
        {
            second_text.text = PlayerPrefs.GetFloat("Secondscore").ToString();
        }
        else
        {
            second_text.gameObject.SetActive(false);
        }

        if (PlayerPrefs.HasKey("Thirdscore"))
        {
            third_text.text = PlayerPrefs.GetFloat("Thirdscore").ToString();
        }
        else
        {
            third_text.gameObject.SetActive(false);
        }

        if (PlayerPrefs.HasKey("Fourthscore"))
        {
            fourth_text.text = PlayerPrefs.GetFloat("Fourthscore").ToString();
        }
        else
        {
            fourth_text.gameObject.SetActive(false);
        }

        if (PlayerPrefs.HasKey("Fithscore"))
        {
            fith_text.text = PlayerPrefs.GetFloat("Fithscore").ToString();
        }
        else
        {
            fith_text.gameObject.SetActive(false);
        }
    }
}
