using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;

public class SpeedTextTest : MonoBehaviour
{
    [SerializeField] GameObject speedtext;
    [SerializeField] GameObject Player;
    Rigidbody rb;
    TextMeshProUGUI speed_text;

    // Start is called before the first frame update
    void Start()
    {
        rb = Player.GetComponent<Rigidbody>();
        speed_text = speedtext.GetComponent<TextMeshProUGUI>();
    }

    // Update is called once per frame
    void Update()
    {
        speed_text.text = "Speed: " + Mathf.Round(rb.velocity.magnitude).ToString();
    }
}
