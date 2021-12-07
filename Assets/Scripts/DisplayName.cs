using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using System;

public class DisplayName : MonoBehaviour
{
    public TextMesh text;
    public string car_name;
    public Countdown countDown;

    // Start is called before the first frame update
    void Start()
    {
        text.text = ""; 
    }
    // Update is called once per frame
    void Update()
    {
        if (!countDown.notFinished())
            text.text = car_name;
    }
}
