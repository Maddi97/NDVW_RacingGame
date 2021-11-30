using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using System;


public class Countdown : MonoBehaviour
{
    public float countDownTime = 5;
    public Text textObject;

    // Start is called before the first frame update
    void Start()
    {
        textObject.text = countDownTime.ToString();
    }

    // Update is called once per frame
    void Update()
    {
        if (countDownTime > -2)
        {
            countDownTime -= Time.deltaTime;

            int roundedCountDownTime = (int)Math.Ceiling(countDownTime);
            if (roundedCountDownTime < 1) {
                textObject.text = "GO!";
            } else {
                textObject.text = roundedCountDownTime.ToString();
            }

        } else {
            textObject.text = "";
        }
    }

    public bool notFinished()
    {
        return countDownTime > 0;
    }
}
