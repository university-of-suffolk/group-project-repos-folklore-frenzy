using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CameraFOV : MonoBehaviour
{

    // CREDIT TO XERONID, KURT-DEKKER & GROZZLER FOR THE CODE!
    // READ THE FULL FORUM DISCUSSUION HERE: https://forum.unity.com/threads/dynamically-change-camera-field-of-view-based-on-player-speed.1240015/

    float player_speed;
    float last_speed;
    float currentFov; //currentQuantity
    float desiredFov; //desiredQuantity

    public float maxFOV;
    public float minFOV;

    public ParticleSystem speedlineParticles;

    const float zoomStep = 3.0f;

    // Start is called before the first frame update
    void Start()
    {
        currentFov = minFOV;
        desiredFov = currentFov;
    }

    void CheckSpeed()
    {
        if (player_speed < last_speed)
        {
            print("Player Speed Decreasing - Zoom IN!");
            last_speed = player_speed;
            desiredFov = minFOV;
            //currentFOV to minFOV
        }
        else if (player_speed > last_speed)
        {
            print("Player Speed Increasing - Zoom OUT!");
            last_speed = player_speed;
            desiredFov = maxFOV;
            //current FOV to maxFOV
        }
    }
    void ProcessFOV()
    {
        currentFov = Mathf.MoveTowards(currentFov, desiredFov, zoomStep * Time.deltaTime);
    }
    void SetFOV()
    {
        Camera.main.fieldOfView = currentFov;
    }

    void Speedlines()
    {
        if (GetComponent<PlayerMove>().Speed >= 20)
        {
            speedlineParticles.Play();
        }
        else
        {
            speedlineParticles.Stop();
        }
    }

    // Update is called once per frame
    void Update()
    {
        player_speed = GetComponent<PlayerMove>().Speed;

        CheckSpeed();
        ProcessFOV();
        SetFOV();
        Speedlines();
    }
}
