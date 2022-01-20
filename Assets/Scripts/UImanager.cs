using System.Collections;
using System.Collections.Generic;
using UnityEngine.UI;
using UnityEngine;

public class UImanager : MonoBehaviour
{
    [SerializeField] private Text timer; 
    private float time;

    public void raceTimer(bool isRunning) 
    {
        if(isRunning == true)
        {
            time = time + Time.deltaTime;
        }

        timer.text = "Time: " + time;

    }

}
